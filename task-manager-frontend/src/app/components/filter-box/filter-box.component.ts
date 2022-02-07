import { Component, Input, OnInit, Output, EventEmitter} from '@angular/core';
import { Filter } from '../home/home.component';

@Component({
  selector: 'app-filter-box',
  templateUrl: './filter-box.component.html',
  styleUrls: ['./filter-box.component.css']
})
export class FilterBoxComponent implements OnInit {
  filterID:string;
  inputValue:string;
  @Output() filterEmitter=new EventEmitter<Filter>();

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
}
