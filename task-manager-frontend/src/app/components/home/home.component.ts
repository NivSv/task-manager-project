import { Component, ElementRef, OnInit, ViewChild} from '@angular/core';
import {User, usersStore} from 'src/app/store/users.store';
import {Task, tasksStore} from 'src/app/store/tasks.store';
import {getTasks} from 'src/app/store/tasks.actions';
import * as moment from 'moment';
import { CookieService } from 'ngx-cookie-service';
import { Router } from '@angular/router';
import * as apiTasks from 'src/api/api-tasks';

export interface Filter
{
  key:string;
  data:string;
}

export interface DateRange
{
  startDate:string;
  endDate:string;
}

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  @ViewChild("editTaskTitle") editTaskTitle!: ElementRef;
  @ViewChild("editTaskDesc") editTaskDesc!: ElementRef;
  @ViewChild("editTaskPriority") editTaskPriority!: ElementRef;
  @ViewChild("editTaskStatus") editTaskStatus!: ElementRef;
  @ViewChild("editTaskDeadline") editTaskDeadline!: ElementRef;
  @ViewChild("editTaskAssignee") editTaskAssignee!: ElementRef;
  editTaskID:number;
  selected?: string;
  usernames: string[];
  tasks: Task[];
  filterBy:Filter;
  errorMessage;


  constructor(private cookieService: CookieService, public router: Router) { 
    this.filterBy = {key:"taskTitle",data:""};
    this.editTaskID=0;
    this.usernames = [];
    this.tasks=[];
    this.errorMessage="";
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
    this.tasks = getTasks()
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

  dateDeadlineFilter(date:DateRange){
    this.tasks = getTasks();
    var startDate = moment(date.startDate).format("yyyy-MM-DD");
    var endDate = moment(date.endDate).format("yyyy-MM-DD");
    this.tasks = getTasks().filter(task => startDate <= moment(task.taskDeadline).format("yyyy-MM-DD") && moment(task.taskDeadline).format("yyyy-MM-DD") <= endDate);
  }

  dateCreationFilter(date:DateRange){
    this.tasks = getTasks();
    var startDate = moment(date.startDate).format("yyyy-MM-DD");
    var endDate = moment(date.endDate).format("yyyy-MM-DD");
    this.tasks = getTasks().filter(task => startDate <= moment(task.taskCreatedDate).format("yyyy-MM-DD") && moment(task.taskCreatedDate).format("yyyy-MM-DD") <= endDate);
  }

  createTask()
  {
    var taskTitle:string = (<HTMLInputElement>document.getElementById("addTaskTitle")).value;
    if(!taskTitle) {
      this.errorMessage="Title is empty!";
      return;
    }
    var taskDesc:string = (<HTMLInputElement>document.getElementById("addTaskDesc")).value;
    if(!taskDesc) {
      this.errorMessage="Desc is empty!";
      return;
    }
    var taskPriority:string = (<HTMLSelectElement>document.getElementById("addTaskPriority")).value;
    var taskDeadline:string = (<HTMLInputElement>document.getElementById("addTaskDeadline")).value;
    if(!taskDeadline || taskDeadline == "Invalid date") {
      this.errorMessage="Deadline is empty!";
      return;
    }
    if(moment(taskDeadline).format("yyyy-MM-DD") < moment(Date.now()).format("yyyy-MM-DD")) {
      this.errorMessage="Deadline cannot be earlier than today!";
      return;
    }
    var taskAssignee:string = (<HTMLInputElement>document.getElementById("addTaskAssignee")).value;
    if(!this.usernames.find(user => user === taskAssignee)) {
      this.errorMessage="Uses Assignee is not exist!";
      return;
    }
    apiTasks.CreateTask(this.cookieService.get('Username'),this.cookieService.get('AccessKey'),taskTitle,taskDesc,taskPriority,taskDeadline,taskAssignee).subscribe({
      error: (e) => console.log(e),
    })
    window.location.reload();
  }

  prepareEditForm(targetedTaskID:number)
  {
    this.editTaskID = targetedTaskID;
    var foundTask =this.tasks.find(task => task.taskId === this.editTaskID)
    this.editTaskTitle.nativeElement.value = foundTask?.taskTitle;
    this.editTaskDesc.nativeElement.value = foundTask?.taskDescription;
    this.editTaskPriority.nativeElement.value = foundTask?.taskPriority;
    this.editTaskStatus.nativeElement.value = foundTask?.taskStatus;
    this.editTaskDeadline.nativeElement.value = moment(foundTask?.taskDeadline).format("MM/DD/yyyy");
    this.editTaskAssignee.nativeElement.value = foundTask?.assignee;
  }

  editTask()
  {
    var taskTitle:string = this.editTaskTitle.nativeElement.value;
    if(!taskTitle) {
      this.errorMessage="Title is empty!";
      return;
    }
    var taskDesc:string = this.editTaskDesc.nativeElement.value;
    if(!taskDesc) {
      this.errorMessage="Desc is empty!";
      return;
    }
    var taskPriority:string =  this.editTaskPriority.nativeElement.value;
    var taskStatus:string =  this.editTaskStatus.nativeElement.value;
    var taskDeadline:string = this.editTaskDeadline.nativeElement.value;
    if(!taskDeadline || taskDeadline == "Invalid date") {
      this.errorMessage="Deadline is empty!";
      return;
    }
    if(moment(taskDeadline).format("yyyy-MM-DD") < moment(Date.now()).format("yyyy-MM-DD")) {
      this.errorMessage="Deadline cannot be earlier than today!";
      return;
    }
    var taskAssignee:string = this.editTaskAssignee.nativeElement.value;
    if(!this.usernames.find(user => user === taskAssignee)) {
      this.errorMessage="Uses Assignee is not exist!";
      return;
    }
    apiTasks.EditTask(this.cookieService.get('Username'),this.cookieService.get('AccessKey'),this.editTaskID,taskTitle,taskDesc,taskPriority,taskDeadline,taskStatus,taskAssignee).subscribe({
      error: (e) => console.log(e),
    })
    window.location.reload();
  }

  deleteTask(taskID:number){
    apiTasks.DeleteTask(this.cookieService.get('Username'),this.cookieService.get('AccessKey'),taskID).subscribe({
      error: (e) => console.log(e),
    })
    window.location.reload();
  }

  ngOnInit(): void {
  }
}
