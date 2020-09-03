import { Component, OnInit, OnChanges, SimpleChanges, OnDestroy } from '@angular/core';
import { FormControl, Validators, FormGroup, AbstractControl } from '@angular/forms';
import { Subscription } from 'rxjs';

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

  public get firstNameErrors():string{
    let firstName:AbstractControl = this.nameForm.controls.firstName;
    if(!firstName.touched || firstName.valid){
      return;
    }

    return JSON.stringify(firstName.errors);
  }

  public get lastNameErrors():string{
    let lastName:AbstractControl = this.nameForm.controls.lastName;
    if(!lastName.touched || lastName.valid){
      return;
    }

    return JSON.stringify(lastName.errors);
  }

  nameFormControlSub: Subscription;

  constructor() {
    this.nameForm = new FormGroup({
      firstName: new FormControl('',Validators.required),
      lastName: new FormControl('',Validators.required)
    });
  }
  ngOnDestroy(): void {
    this.nameFormControlSub?.unsubscribe();
  }

  ngOnChanges(changes: SimpleChanges): void {
    console.log(`changes detected: ${changes}`);
  }

  ngOnInit(): void {
    this.isItTrue = true;
    //this.nameFormControl.
    this.nameFormControlSub = this.nameForm.valueChanges.subscribe(
      (val:any)=>{
        console.log(`${val} Name changed: ${this.nameForm.value}`);
      },
      (err:any)=>{}
    );
  }

  onSubmit():void{
  }
}
