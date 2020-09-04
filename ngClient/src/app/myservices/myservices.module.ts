import { NgModule } from '@angular/core';
import { MyHttpService } from './my-http-service';
import { UserService } from './user-service';
import { SignalrFactory } from './signalr-factory';

let userServiceSingleton = new UserService();

@NgModule({
  declarations: [],
  imports: [],
  providers:[
    MyHttpService,
    SignalrFactory,
    {provide: UserService, useValue: userServiceSingleton}
  ]
})
export class MyservicesModule { }
