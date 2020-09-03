import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class MyHttpService {
    constructor(httpClient:HttpClient) {
    }

    public doSomething():void{
        console.log('I work');
    }
}
