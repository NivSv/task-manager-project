import { Component, OnInit } from '@angular/core';
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

  ngOnInit(): void {
  }

}
