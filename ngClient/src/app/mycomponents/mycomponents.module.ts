import { NgModule } from '@angular/core';
import { LoginComponent } from './login.component';
import { FormsModule, ReactiveFormsModule} from '@angular/forms';
import { MyservicesModule } from '../myservices/myservices.module';

@NgModule({
  declarations: [LoginComponent],
  imports: [
     FormsModule,
     ReactiveFormsModule,
     MyservicesModule
  ],
})
export class MycomponentsModule { }
