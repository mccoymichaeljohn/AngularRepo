import { Component, Inject } from '@angular/core';
import { FetchDataService } from './fetch-data.service';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  public notes: Note[] = [];
  public forecasts: WeatherForecast[] = [];
  public fetchDataService: FetchDataService;
  public note: Note = { dateDue: new Date(), dateCreated: new Date(), text: "" };

  constructor(fetchDataService: FetchDataService) {
    fetchDataService.getNotes().subscribe(result => {
      this.notes = result;
    }, error => console.error(error));
    this.fetchDataService = fetchDataService;
  }

  public async postNote() {
    this.fetchDataService.postNote(this.note.text, this.note.dateDue)
      .subscribe(result => this.notes = this.notes.concat(result));
  }
}

interface Note {
  dateCreated: Date | undefined;
  dateDue: Date | undefined;
  text: string;
}

interface WeatherForecast {
  date: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}
