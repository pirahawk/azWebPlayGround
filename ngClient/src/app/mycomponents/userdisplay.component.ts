import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { UserService } from '../myservices/user-service';

@Component({
  selector: 'app-userdisplay',
  templateUrl: './userdisplay.component.html',
})
export class UserdisplayComponent implements OnInit {

  public get userName():string{
    return this.userService.userName;
  }

  constructor(public activatedRoute:ActivatedRoute, public userService:UserService) { }

  ngOnInit(): void {
  }

}
