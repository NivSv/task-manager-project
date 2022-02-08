import { Component, Input, OnInit, Output, EventEmitter, ViewChild, ElementRef} from '@angular/core';
import * as moment from 'moment';
import { Filter,DateRange } from '../home/home.component';

@Component({
  selector: 'app-filter-box',
  templateUrl: './filter-box.component.html',
  styleUrls: ['./filter-box.component.css']
})
export class FilterBoxComponent implements OnInit {
  @ViewChild("inputText") inputText!: ElementRef;
  filterID:string;
  inputValue:string;
  @Output() filterEmitter=new EventEmitter<Filter>();
  @Output() creationDateFilterEmitter=new EventEmitter<DateRange>();
  @Output() deadlineDateFilterEmitter=new EventEmitter<DateRange>();

  constructor() { 
    this.filterID="taskTitle";
    this.inputValue="";
  }

  ngOnInit(): void {
  }

  dropDownClick(event:any)
  {
    var target = event.target || event.srcElement || event.currentTarget;
    var idAttr = target.attributes.id;
    var value = idAttr.nodeValue;
    this.filterID=value;
    this.filterEmitter.emit({key:this.filterID,data:this.inputValue});
  }

  inputChange(value:string)
  {
    this.inputValue =value;
    this.filterEmitter.emit({key:this.filterID,data:this.inputValue});
  }

  creationDateFilter(dateRangeStart: HTMLInputElement, dateRangeEnd: HTMLInputElement) {
    this.creationDateFilterEmitter.emit({startDate:dateRangeStart.value,endDate:dateRangeEnd.value});
  }

  deadlineFilter(deadline: HTMLInputElement)
  {
    var dateToday:string = moment(Date.now()).format("MM-DD-yyyy");
    this.deadlineDateFilterEmitter.emit({startDate:dateToday,endDate:deadline.value});
  }

  resetFilter(){
    window.location.reload();
  }
}
