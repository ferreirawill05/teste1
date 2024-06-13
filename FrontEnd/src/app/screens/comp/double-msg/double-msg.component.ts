import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialog, MatDialogRef } from '@angular/material/dialog';
import { DataDuplicada } from 'src/app/interfaces/DataDuplicada';

@Component({
  selector: 'app-double-msg',
  templateUrl: './double-msg.component.html',
  styleUrls: ['./double-msg.component.css']
})
export class DoubleMsgComponent {
  data!: DataDuplicada

  constructor(
    public dialogRef: MatDialogRef<DoubleMsgComponent>,
    @Inject(MAT_DIALOG_DATA) public dataDuplicada: DataDuplicada,
    private dialog: MatDialog
    ) { }


    ngOnInit(): void {
      setTimeout(() => {
        this.dialogRef.close();
      }, 7000);
    }
}
