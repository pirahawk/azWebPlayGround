import { Component, OnInit, OnChanges, SimpleChanges, OnDestroy, Injectable } from '@angular/core';
import { FormControl, Validators, FormGroup, AbstractControl } from '@angular/forms';
import { Subscription } from 'rxjs';
import { MyHttpService } from '../myservices/my-http-service';
import { HttpClient, HttpResponse, HttpErrorResponse } from '@angular/common/http';
import { Myuser } from '../domain/myuser';

@Component({
  selector: "app-login",
  templateUrl: "./login.component.html"
})
export class LoginComponent implements OnInit, OnChanges, OnDestroy {

  public isItTrue:boolean;
  public nameForm:FormGroup;

  public get nameFormVal():string{
    return JSON.stringify(this.nameForm.value);
  }

  public get nameFormErrors():string{
    return JSON.stringify(this.nameForm.errors);
  }

  public get userNameErrors():string{
    let userName:AbstractControl = this.nameForm.controls.userName;
    if(!userName.touched || userName.valid){
      return;
    }

    return JSON.stringify(userName.errors);
  }

  public get userName():string{
    let userName:AbstractControl = this.nameForm.controls.userName;
    return userName.value;
  }

  private nameFormControlSub: Subscription;

  constructor(public myHttpService:MyHttpService) {
    this.nameForm = new FormGroup({
      userName: new FormControl('',Validators.required)
    });
  }

  ngOnInit(): void {
    this.isItTrue = true;
    this.nameFormControlSub = this.nameForm.valueChanges.subscribe(
      (val:any)=>{
        console.log(`${val} Name changed: ${this.nameFormVal}`);
      },
      (err:any)=>{}
    );
  }

  ngOnChanges(changes: SimpleChanges): void {
    console.log(`changes detected: ${changes}`);
  }

  ngOnDestroy(): void {
    this.nameFormControlSub?.unsubscribe();
  }

  onSubmit():void {
    this.myHttpService.login({
      userName: this.userName
    }).subscribe(
      (response:HttpResponse<null>)=>{
        console.log("success");
      },
      (error:HttpErrorResponse)=>{
        console.log("fail");
      }
    );
  }
}
