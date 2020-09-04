import { Subject } from 'rxjs';

export class MySignalrConnection{
    public connectionStatus:MySignalrConnectionStatus;
    public onConnectedSubject:Subject<MySignalrConnectionStatus>;
    public onSendTextMessageSubject:Subject<string>;
    public onSendChatMessageSubject:Subject<MyUserMessageModel>;

    constructor(public connection:signalR.HubConnection) {
        this.onConnectedSubject = new Subject<MySignalrConnectionStatus>();
        this.onSendTextMessageSubject = new Subject<string>();
        this.onSendChatMessageSubject = new Subject<MyUserMessageModel>();
    }

    public startConnection(handler:IMySignalrConnectionHandler):void{
        this.connection.onclose((error: Error | undefined) => {
            console.log('Connection has been closed');
            this.updateConnectionStatus(MySignalrConnectionStatus.disconnected);
            
            setTimeout(()=>{
                this.attemptConnectionStart();
            }, 1000);
        });

        this.connection.onreconnecting(() => {
            console.log('Connection attempting reconnect');
            this.updateConnectionStatus(MySignalrConnectionStatus.reconnecting);
        });

        this.connection.onreconnected((connectionId: string | undefined) => {
            console.log(`Reconnection Success: ${connectionId}`);
            this.updateConnectionStatus(MySignalrConnectionStatus.connected);
        });

        this.connection.on("SendTextMessage", (message: string, connectionId: string) => {
            console.log(`Recieved Message from ${connectionId}: ${message}`);
            this.onSendTextMessageSubject.next(message);
        });

        this.connection.on("SendChatMessage", (userMessage: MyUserMessageModel) => {
            //console.log(`Recieved Message from ${connectionId}: ${message}`);
            this.onSendChatMessageSubject.next(userMessage);
        });

        this.attemptConnectionStart();
    }

    public sendChatMessage(message:string):Promise<any>{
        //return this.connection.invoke('EchoTextMessage', message, this.connection.connectionId);
        return this.connection.invoke('EchoChatMessage', message);
    }

    private attemptConnectionStart():void{
        this.connection.start().then(
            () => {
                this.updateConnectionStatus(MySignalrConnectionStatus.connected);
            }
        ).catch(() => {
            this.onConnectedSubject.error("connection fail");
        });
    }

    private updateConnectionStatus(status:MySignalrConnectionStatus){
        this.connectionStatus = status;
        this.onConnectedSubject.next(status);
    }
}

export enum MySignalrConnectionStatus {
    disconnected,
    connected,
    reconnecting
}

export interface IMySignalrConnectionHandler{
}

export class MyUserMessageModel
{
    public user:string;
    public connectionId:string;
    public message:string;
}