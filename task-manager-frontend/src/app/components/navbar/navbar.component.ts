import { Component, OnInit } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  hamActive:boolean;
  navActive:number;

  constructor(public cookieService: CookieService) { 
    this.hamActive=false;
    this.navActive=0;
  }

  ngOnInit(): void {
  }

  Logout()
  {
    this.cookieService.set('Username', "");
    this.cookieService.set('AccessKey', "");
  }

  hamClick()
  {
    this.hamActive = !this.hamActive;
  }

  checkValue(value:number)
  {
    return this.navActive == value;
  }

  navClick(event:any)
  {
    var target = event.target || event.srcElement || event.currentTarget;
    var idAttr = target.attributes.id;
    var value = idAttr.nodeValue;
    this.navActive = value;
  }
}
