export class Note {

  constructor(id: string, text: string, dateCreated: Date, dateDue: Date | undefined, isCompleted: boolean, noteType: NoteType) {
    this.id = id;
    this.text = text;
    this.dateCreated = dateCreated;
    this.dateDue = dateDue;
    this.isCompleted = isCompleted;
    this.noteType = noteType;
  }
  id: string;
  text: string;
  dateCreated: Date;
  dateDue: Date | undefined;
  isCompleted: boolean;
  noteType: NoteType;

}

export enum NoteType {
  Note,
  Task,
  Event
}
