import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  hamActive:boolean;

  constructor() { 
    this.hamActive=false;
  }

  ngOnInit(): void {
  }

  hamClick()
  {
    this.hamActive = !this.hamActive;
  }
}
