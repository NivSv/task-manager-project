import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  hamActive:boolean;
  navActive:number;

  constructor() { 
    this.hamActive=false;
    this.navActive=0;
  }

  ngOnInit(): void {
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
