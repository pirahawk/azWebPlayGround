import { Injectable } from '@angular/core';
import { Myuser } from '../domain/myuser';

@Injectable()
export class UserService {
    private myUser: Myuser;

    public get userName():string{
        return this.myUser?.userName;
    }
    
    public setUser(myUser: Myuser):void{
        this.myUser = myUser;
    }
}
