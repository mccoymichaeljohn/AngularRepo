export class Note {

  constructor(id: string, text: string, dateCreated: Date, dateDue: Date | undefined) {
    this.id = id;
    this.text = text;
    this.dateCreated = dateCreated;
    this.dateDue = dateDue;
  }
  id: string;
  text: string;
  dateCreated: Date;
  dateDue: Date | undefined;
}
