import { NgModule } from '@angular/core';
import { LoginComponent } from './login.component';
import { FormsModule, ReactiveFormsModule} from '@angular/forms';
import { CommonModule } from '@angular/common';
import { BrowserModule } from '@angular/platform-browser';
import { MyservicesModule } from '../myservices/myservices.module';

@NgModule({
  declarations: [LoginComponent],
  imports: [
    CommonModule,
     FormsModule,
     ReactiveFormsModule,
     MyservicesModule
  ],
})
export class MycomponentsModule { }
