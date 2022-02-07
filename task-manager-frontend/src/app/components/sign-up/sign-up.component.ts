import { Component, OnInit} from '@angular/core';
import { Observable } from 'rxjs';
import * as apiUsers from 'src/api/api-users';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css']
})
export class SignUpComponent implements OnInit {
  errorMessage:string
  errorActive:boolean

  constructor() {
    this.errorActive=false;
    this.errorMessage="";
  }

  ngOnInit(): void {
  }

  error(error:any)
  {
    this.errorMessage=error.error;
    this.errorActive=true;
  }
  
  async submit()
  {
    var username:string = (<HTMLInputElement>document.getElementById("username")).value;
    var password:string = (<HTMLInputElement>document.getElementById("password")).value;
    var confirmPassword:string = (<HTMLInputElement>document.getElementById("confirm-password")).value;
    if(password!==confirmPassword) 
    {
       this.errorMessage="Please make sure your passwords match.";
       this.errorActive=true;
    }
    else
    {
      await apiUsers.Register(username,password).catch(e => this.error(e));
    }
  }
}
