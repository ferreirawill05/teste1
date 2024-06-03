import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-msg-error',
  templateUrl: './msg-error.component.html',
  styleUrls: ['./msg-error.component.css']
})
export class MsgErrorComponent implements OnInit{

    constructor(public dialogRef:MatDialogRef<MsgErrorComponent>){}

  ngOnInit(): void {
    setTimeout(() => {
      this.dialogRef.close();
    }, 4000);
  }
}
