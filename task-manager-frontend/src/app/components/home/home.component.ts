import { Component, OnInit} from '@angular/core';
import {User, usersStore} from 'src/app/store/users.store';
import {Task, tasksStore} from 'src/app/store/tasks.store';
import * as moment from 'moment';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})

export class HomeComponent implements OnInit {
  selected?: string;
  usernames: string[];
  tasks: Task[];

  constructor() { 
    this.usernames = [];
    this.tasks=[];
    usersStore.subscribe(x => this.setUsers(x.map((user:User) => user.username)));

    tasksStore.subscribe(x => this.setTasks(x));
  }

  toDate(date:string)
  {
    return moment(date).format("yyyy-MM-DD");
  }

  setUsers(usernames:string[]): void {
    this.usernames = usernames;
  }

  setTasks(tasks:Task[]): void {
    this.tasks = tasks;
    console.log(tasks)
  }

  ngOnInit(): void {
  }
}
