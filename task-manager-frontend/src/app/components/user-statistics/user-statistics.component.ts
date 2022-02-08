import { Component, OnInit } from '@angular/core';
import * as moment from 'moment';
import { getTasks } from 'src/app/store/tasks.actions';
import {Task, tasksStore} from 'src/app/store/tasks.store';

export interface UserStatistics
{
  Username:string;
  tasksDone:number;
}

@Component({
  selector: 'app-user-statistics',
  templateUrl: './user-statistics.component.html',
  styleUrls: ['./user-statistics.component.css']
})
export class UserStatisticsComponent implements OnInit {
  tasks: Task[];
  usersStatistics: UserStatistics[];

  constructor() {
      this.usersStatistics=[];
      this.tasks=[];
      tasksStore.subscribe(x => this.setTasks(x));
   }

  setTasks(tasks:Task[]): void {
    this.tasks = tasks.filter(task => task.taskStatus == "Done");
    this.tasks.map(task => this.addTaskToUser(task.assignee));
  }

  addTaskToUser(username:string)
  {
    var user = this.usersStatistics.find(user => user.Username == username);
    if(user){
      var newarray:UserStatistics[] = this.usersStatistics.map(user=>{
        if(user.Username==username){
          return{...user,tasksDone: user.tasksDone+1}
        }
        return user;
      })
      this.usersStatistics=newarray;
    }
    else 
    {
      this.usersStatistics.push({Username: username,tasksDone:1});
    }
  }

  dateFilter(dateRangeStart: HTMLInputElement, dateRangeEnd: HTMLInputElement) {
    this.usersStatistics=[];
    var dateStart:string = moment(dateRangeStart.value).format("yyyy-MM-DD");
    var dateEnd:string = moment(dateRangeEnd.value).format("yyyy-MM-DD");
    this.tasks = getTasks().filter(task => task.taskStatus == "Done" && dateStart<= moment(task.taskCreatedDate).format("yyyy-MM-DD") && moment(task.taskCreatedDate).format("yyyy-MM-DD")<=dateEnd);
    this.tasks.map(task => this.addTaskToUser(task.assignee));
  }

  resetFilter(){
    window.location.reload();
  }

  sortUsersByTaskDone() {
    return this.usersStatistics.sort((a, b) => a.tasksDone < b.tasksDone ? 1 : a.tasksDone === b.tasksDone ? 0 : -1);
  }

  ngOnInit(): void {
  }

}
