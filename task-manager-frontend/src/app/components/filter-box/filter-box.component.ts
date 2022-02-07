import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-filter-box',
  templateUrl: './filter-box.component.html',
  styleUrls: ['./filter-box.component.css']
})
export class FilterBoxComponent implements OnInit {
  filterID:number;

  constructor() { 
    this.filterID=1;
  }

  ngOnInit(): void {
  }

  dropDownClick(event:any)
  {
    var target = event.target || event.srcElement || event.currentTarget;
    var idAttr = target.attributes.id;
    var value = idAttr.nodeValue;
    this.filterID=value;
  }
}
