import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
import * as apiUsers from 'src/api/api-users';
import * as apiTasks from 'src/api/api-tasks';
import * as usersActions from 'src/app/store/users.actions';
import * as tsersActions from 'src/app/store/tasks.actions';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent implements OnInit {
  title = 'task-manager-frontend';

  constructor(private cookieService: CookieService, public router: Router) { 
  }

  setUsers(data:any)
  {
    usersActions.setUsers(data);
  }

  setTasks(data:any)
  {
    tsersActions.setTasks(data);
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

  ngOnInit(): void {
      apiUsers.GetUsers(this.cookieService.get('Username'),this.cookieService.get('AccessKey')).subscribe({
        next: data => this.setUsers(data),
        error: (e) => this.error(e),
      })

      apiTasks.GetTasks(this.cookieService.get('Username'),this.cookieService.get('AccessKey')).subscribe({
        next: data => this.setTasks(data),
        error: (e) => this.error(e),
      })
  }
}