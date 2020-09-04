import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { UserService } from '../myservices/user-service';
import { SignalrFactory} from '../myservices/signalr-factory';
import { MySignalrConnection, IMySignalrConnectionHandler, MySignalrConnectionStatus, MyUserMessageModel } from '../myservices/my-signalr-connection';
import { Subscription } from 'rxjs';
import { FormGroup, Validators, FormControl } from '@angular/forms';

@Component({
  selector: 'app-userdisplay',
  templateUrl: './userdisplay.component.html',
})
export class UserdisplayComponent implements OnInit, OnDestroy, IMySignalrConnectionHandler {
  public myConnection: MySignalrConnection;
  public isConnected: boolean;
  public connectionStatus: string;
  public onConnectedSubscriber: Subscription;
  public onSendTextMessageSubject: Subscription;
  public onSendChatMessageSubject: Subscription;
  public messageFormGroup: FormGroup;

  public get userName(): string {
    return this.userService.userName;
  }

  constructor(public activatedRoute: ActivatedRoute, public userService: UserService, public signalFactory: SignalrFactory) {
    this.messageFormGroup = new FormGroup({
      chatMessage: new FormControl('', Validators.required)
    });
  }

  ngOnInit(): void {
    this.updateConnectionStatus(MySignalrConnectionStatus.disconnected);
    this.myConnection = this.signalFactory.createConnection();
    this.onConnectedSubscriber = this.myConnection.onConnectedSubject.subscribe(
      (status: MySignalrConnectionStatus) => {
        this.updateConnectionStatus(status);
      },
      () => {
        this.updateConnectionStatus(MySignalrConnectionStatus.disconnected);
      },
    );
    this.onSendTextMessageSubject = this.myConnection.onSendTextMessageSubject.subscribe(
      (message: string) => { this.recieveMessage(message); },
      () => { }
    );
    this.onSendChatMessageSubject = this.myConnection.onSendChatMessageSubject.subscribe(
      (message: MyUserMessageModel) => { 
        this.recieveMessage(`${message.user}: ${message.message}`);
      },
      () => { }
    );
    this.myConnection.startConnection(this);
  }

  recieveMessage(message: string): void {
    let msgDisplay: HTMLElement = document.getElementById('msgDisplay') as HTMLElement;
    let newMsgP = document.createElement('p');
    newMsgP.innerText = message;
    msgDisplay.appendChild(newMsgP);
  }

  updateConnectionStatus(connectionStatus: MySignalrConnectionStatus): void {
    switch (connectionStatus) {

      case MySignalrConnectionStatus.disconnected:
        this.connectionStatus = 'Disconnected';
        this.isConnected = false;
        break;

      case MySignalrConnectionStatus.connected:
        this.connectionStatus = 'Connected';
        this.isConnected = true;
        break;

      case MySignalrConnectionStatus.reconnecting:
        this.connectionStatus = 'Reconnecting..';
        this.isConnected = false;
        break;
    }
  }

  ngOnDestroy(): void {
    this.onConnectedSubscriber?.unsubscribe();
    this.onSendTextMessageSubject.unsubscribe();
  }

  async onSubmit(): Promise<void> {
    let message: string = this.messageFormGroup.controls.chatMessage.value;
    if (message) {
      await this.myConnection.sendChatMessage(message);
      this.messageFormGroup.controls.chatMessage.reset('');
    }
  }
}
