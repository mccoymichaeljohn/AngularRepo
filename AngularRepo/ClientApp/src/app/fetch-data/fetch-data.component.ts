import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  public notes: Note[] = [];
  public forecasts: WeatherForecast[] = [];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<Note[]>(baseUrl + 'api/notes').subscribe(result => {
      this.notes = result;
    }, error => console.error(error));
  }
}

interface Note {
  dateCreated: string;
  dateDue: string;
  text: string;
}

interface WeatherForecast {
  date: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}