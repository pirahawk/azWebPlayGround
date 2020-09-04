import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, ParamMap } from '@angular/router';

@Component({
  selector: 'app-userdisplay',
  templateUrl: './userdisplay.component.html',
})
export class UserdisplayComponent implements OnInit {

  constructor(public activatedRoute:ActivatedRoute) { }

  ngOnInit(): void {
  }

}
