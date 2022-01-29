import { Component, EventEmitter, Input } from '@angular/core';

@Component({
  selector: 'editable',
  inputs: ["value", "type"],
  outputs: [
    "cancelEvents: cancel",
    "textChangeEvents: textChange",
    "dateChangeEvents: dateChange"
  ],
  template: `
    <input
			type={{type}}
			name=value
			autofocus
			[(ngModel)]="pendingValue"
			(keydown.Enter)="processChanges()"
			(keydown.Meta.Enter)="processChanges()"
			(keydown.Escape)="cancel()"
		/>
  `
})
export class EditableComponent {
  @Input() type: "text" | "date" = "text";
  //public type!: "text" | "date";
  public cancelEvents: EventEmitter<void>;
  public pendingValue: string | Date | undefined | null;
  public value!: string | Date | undefined | null;
  public textChangeEvents: EventEmitter<string>;
  public dateChangeEvents: EventEmitter<Date | undefined>;

  constructor() {
    this.cancelEvents = new EventEmitter();
    this.pendingValue = "";
    this.textChangeEvents = new EventEmitter();
    this.dateChangeEvents = new EventEmitter();
  }

  public cancel(): void {
    this.cancelEvents.emit();
  }

  public ngOnInit(): void {
    this.pendingValue = this.value;
  }

  public processChanges(): void {
    console.log(this.type);
    console.log(this.pendingValue);
    if (this.pendingValue === this.value) {
      this.cancelEvents.emit();
    } else if (this.type === "text") {
      this.textChangeEvents.emit(this.pendingValue?.toString());
    } else {
      this.dateChangeEvents.emit(<Date | undefined>this.pendingValue);
    }
  }
}
