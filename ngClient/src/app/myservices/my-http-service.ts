import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { Myuser } from '../domain/myuser';
import { Observable } from 'rxjs';

@Injectable()
export class MyHttpService {
    constructor(public httpClient:HttpClient) {
    }

    public login(user:Myuser):Observable<HttpResponse<null>>{
        return this.httpClient.post<null>("/api/login",user, { observe: 'response' } );
    }
}
