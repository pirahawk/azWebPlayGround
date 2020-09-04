import { NgModule } from '@angular/core';
import { MyHttpService } from './my-http-service';
import { UserService } from './user-service';

let userServiceSingleton = new UserService();

@NgModule({
  declarations: [],
  imports: [],
  providers:[
    MyHttpService,
    {provide: UserService, useValue: userServiceSingleton}
  ]
})
export class MyservicesModule { }
