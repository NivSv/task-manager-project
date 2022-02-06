import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  constructor(private http: HttpClient,) { }
  list= []

  ngOnInit(): void {
    this.http.get('https://localhost:7259/api/WowcharactersTable').subscribe(data => console.log(data));
  }

}
