import { Component } from '@angular/core';
import { FetchDataService } from './fetch-data.service';
import { Note, NoteType } from './note';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html',
  styleUrls: ['./fetch-data.component.css']
})
export class FetchDataComponent {
  public notes: Note[] = [];
  public fetchDataService: FetchDataService;
  public note: Note = { id: "", noteType: NoteType.Note, dateDue: new Date(), dateCreated: new Date(), text: "", isCompleted: false };
  public selectedNote: Note | null;

  constructor(fetchDataService: FetchDataService) {
    this.fetchDataService = fetchDataService;
    this.getNotes();
    this.selectedNote = null;
  }

  public async getNotes() {
    this.fetchDataService.getNotes().subscribe(result => {
      this.notes = result;
    }, error => console.error(error));
  }

  public async postNote() {
    this.fetchDataService.postNote(this.note.noteType, this.note.text, this.note.dateDue)
      .subscribe(result => this.notes = this.notes.concat(result));
  }

  public async deleteNote(note: Note) {
    this.fetchDataService.deleteNote(note.id)
      .subscribe(x => this.notes = this.notes.filter(n => n != note));
  }

  public async updateText(note: Note, text: string) {
    this.notes = this.notes.map(n => n.id == note.id ? new Note(note.id, text, note.dateCreated, note.dateDue, note.isCompleted, note.noteType) : n);
    this.fetchDataService.updateNote(note.id, text, note.dateDue, note.isCompleted)
      .subscribe(result => this.notes = this.notes.map(n => n.id == result.id ? result : n));
    this.selectedNote = null;
  }

  public async updateDate(note: Note, date: Date | undefined) {
    this.notes = this.notes.map(n => n.id == note.id ? new Note(note.id, note.text, note.dateCreated, date, note.isCompleted, note.noteType) : n);
    this.fetchDataService.updateNote(note.id, note.text, date, note.isCompleted)
      .subscribe(result => this.notes = this.notes.map(n => n.id == result.id ? result : n));
    this.selectedNote = null;
  }

  public async toggleCompletion(note: Note) {
    this.notes = this.notes.map(n => n.id == note.id ? new Note(note.id, note.text, note.dateCreated, note.dateDue, !note.isCompleted, note.noteType) : n);
    this.fetchDataService.updateNote(note.id, note.text, note.dateDue, !note.isCompleted)
      .subscribe(result => this.notes = this.notes.map(n => n.id == result.id ? result : n));
    this.selectedNote = null;
  }

  public cancelEdit() {
    this.selectedNote = null;
  }

  public selectNote(note: Note): void {
    this.selectedNote = note;
  }
}
