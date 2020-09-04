import { NgModule } from '@angular/core';
import { LoginComponent } from './login.component';
import { FormsModule, ReactiveFormsModule} from '@angular/forms';
import { MyservicesModule } from '../myservices/myservices.module';
import { UserdisplayComponent } from './userdisplay.component';
import { AppRoutingModule } from '../app-routing.module';

@NgModule({
  declarations: [LoginComponent, UserdisplayComponent],
  imports: [
     FormsModule,
     ReactiveFormsModule,
     MyservicesModule,
     AppRoutingModule
  ],
})
export class MycomponentsModule { }
