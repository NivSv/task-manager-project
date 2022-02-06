import { Component, OnInit} from '@angular/core';
import { HttpClient } from '@angular/common/http';
import * as apiUsers from 'src/api/api-users';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})

export class HomeComponent implements OnInit {
  list= [];
  selected?: string;
  users: string[] = [
    'Alabama',
    'Wisconsin',
    'Wyoming'
  ];

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
  }
}
