import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
import * as apiUsers from 'src/api/api-users';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  errorMessage:string
  errorActive:boolean
  successMessage:string
  successActive:boolean

  constructor(private cookieService: CookieService, public router: Router) { 
    this.errorActive=false;
    this.errorMessage="";
    this.successActive=false;
    this.successMessage="";
  }

  ngOnInit(): void {
  }

  errorSet(error:any)
  {
    this.errorMessage=error.error;
    this.errorActive=true;
    this.successActive=false;
  }

  success(accessKey:any, username:string)
  {
    this.cookieService.set('AccessKey', accessKey);
    this.cookieService.set('Username', username);
    this.router.navigate(['home']);
  }

  submit()
  {
    var username:string = (<HTMLInputElement>document.getElementById("username")).value;
    var password:string = (<HTMLInputElement>document.getElementById("password")).value;
    apiUsers.Login(username,password).subscribe({
      next: (data) => this.success(data,username),
      error: (e) => this.errorSet(e),
    })
  }
}
