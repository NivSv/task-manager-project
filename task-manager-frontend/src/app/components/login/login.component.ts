import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
import * as apiUsers from 'src/api/api-users';
import * as apiTasks from 'src/api/api-tasks';
import * as usersActions from 'src/app/store/users.actions';
import * as tasksActions from 'src/app/store/tasks.actions';

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

  setUsers(data:any)
  {
    usersActions.setUsers(data);
  }

  setTasks(data:any)
  {
    tasksActions.setTasks(data);
  }

  error(error:any)
  {
    if(error.status === 401 || error.status === 400)
    {
      this.cookieService.set('Username',"");
      this.cookieService.set('AccessKey',"");
      this.router.navigate(['login']);
    }
  }

  success(accessKey:any, username:string)
  {
    this.cookieService.set('AccessKey', accessKey);
    this.cookieService.set('Username', username);
    apiUsers.GetUsers(this.cookieService.get('Username'),this.cookieService.get('AccessKey')).subscribe({
      next: data => this.setUsers(data),
      error: (e) => this.error(e),
    })

    apiTasks.GetTasks(this.cookieService.get('Username'),this.cookieService.get('AccessKey')).subscribe({
      next: data => this.setTasks(data),
      error: (e) => this.error(e),
    })
    this.router.navigate(['']);
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
