import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable, Inject } from '@angular/core';
import { Observable, throwError as observableThrowError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';

import { Note } from './note';

@Injectable({
  providedIn: 'root'
})
export class FetchDataService {
  private url = ""
  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) { this.url = baseUrl + "api/notes" }

  getNotes() {
    return this.http.get<Note[]>(this.url)
      .pipe(map(data => data), catchError(this.handleError));
  }

  postNote(text: string, dateDue: Date | undefined) {
    return this.http.post<Note>(this.url, { text, dateDue })
      .pipe(map(data => data), catchError(this.handleError));
  }

  private handleError(res: HttpErrorResponse | any) {
    console.error(res.error || res.body.error);
    return observableThrowError(res.error || 'Server error');
  }
}
