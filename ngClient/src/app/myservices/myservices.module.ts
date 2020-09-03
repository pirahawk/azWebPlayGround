import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MyHttpService } from './my-http-service';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
  declarations: [],
  imports: [
    BrowserModule,
    CommonModule,
    HttpClientModule,
  ],
  providers:[
    MyHttpService
  ]
})
export class MyservicesModule { }
