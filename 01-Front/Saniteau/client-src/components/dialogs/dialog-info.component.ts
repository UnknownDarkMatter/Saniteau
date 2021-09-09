import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Component } from '@angular/core';
import { Inject } from '@angular/core';
import { AppService } from '../../services/AppService';

@Component({
    selector: 'dialog-info',
    templateUrl: 'dialog-info.component.html',
    styleUrls: ['./dialog-info.component.css']
})
export class DialogInfoComponent {
    public title: string;
    public message: string;

    constructor(@Inject(AppService) public appService,
        @Inject(MatDialogRef) public dialogRef: MatDialogRef<DialogInfoComponent>,
        @Inject(MAT_DIALOG_DATA) data) {
        this.title = data.title;
        this.message = data.message;
    }

    confirm() {
        this.dialogRef.close();
    }
}
