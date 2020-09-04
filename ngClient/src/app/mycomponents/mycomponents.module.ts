import { NgModule } from '@angular/core';
import { LoginComponent } from './login.component';
import { FormsModule, ReactiveFormsModule} from '@angular/forms';
import { MyservicesModule } from '../myservices/myservices.module';
import { UserdisplayComponent } from './userdisplay.component';
import { AppRoutingModule } from '../app-routing.module';
import { CommonModule } from '@angular/common';

@NgModule({
  declarations: [LoginComponent, UserdisplayComponent],
  imports: [
    CommonModule,
     FormsModule,
     ReactiveFormsModule,
     MyservicesModule,
     AppRoutingModule
  ],
})
export class MycomponentsModule { }
