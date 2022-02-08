import { Component, OnInit} from '@angular/core';
import { Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
import * as apiUsers from 'src/api/api-users';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css']
})
export class SignUpComponent implements OnInit {
  errorMessage:string
  errorActive:boolean
  successMessage:string
  successActive:boolean

  constructor(private cookieService: CookieService, public router: Router) {
    this.errorActive=false;
    this.errorMessage="";
    this.successActive=false;
    this.successMessage="";
    if(this.cookieService.get('Username') !== ""){
      this.router.navigate(['']);
     }
  }

  ngOnInit(): void {
  }

  errorSet(error:any)
  {
    this.errorMessage=error.error;
    this.errorActive=true;
    this.successActive=false;
  }

  success(username:string)
  {
    this.successMessage="Username "+username+" is now signed up.";
    this.successActive=true;
    this.errorActive=false;
  }
  
  submit()
  {
    var username:string = (<HTMLInputElement>document.getElementById("username")).value;
    var password:string = (<HTMLInputElement>document.getElementById("password")).value;
    var confirmPassword:string = (<HTMLInputElement>document.getElementById("confirm-password")).value;
    if(password!==confirmPassword) 
    {
       this.errorMessage="Please make sure your passwords match.";
       this.errorActive=true;
       this.successActive=false;
    }
    else
    {
      apiUsers.Register(username,password).subscribe({
        next: () => this.success(username),
        error: (e) => this.errorSet(e),
      })
    }
  }
}
