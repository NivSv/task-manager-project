import { Component, OnInit} from '@angular/core';
import {User, usersStore} from 'src/app/store/users.store';
import {Task, tasksStore} from 'src/app/store/tasks.store';
import {getTasks} from 'src/app/store/tasks.actions';
import * as moment from 'moment';

export interface Filter
{
  key:string;
  data:string;
}

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  selected?: string;
  usernames: string[];
  tasks: Task[];
  filterBy:Filter;


  constructor() { 
    this.filterBy = {key:"taskTitle",data:""};
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
    const filterKey = this.filterBy.key as keyof Task;
    this.tasks = tasks.filter(task => String(task[filterKey]).includes(this.filterBy.data));
  }

  setFilter(filter:Filter){
    this.filterBy=filter;
    if(this.filterBy.data)
    {
      const filterKey = this.filterBy.key as keyof Task;
      this.tasks = getTasks().filter(task => String(task[filterKey]).includes(this.filterBy.data));
    }
    else
    {
      this.tasks = getTasks();
    }
  }

  ngOnInit(): void {

  }
}
