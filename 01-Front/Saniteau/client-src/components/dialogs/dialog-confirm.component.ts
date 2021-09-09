import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Component } from '@angular/core';
import { Inject } from '@angular/core';
import { AppService } from '../../services/AppService';

export class DialogConfirmResult {
    public confirm: boolean;

    constructor(confirm: boolean) {
        this.confirm = confirm;
    }
}

@Component({
    selector: 'dialog-confirm',
    templateUrl: 'dialog-confirm.component.html',
    styleUrls: ['./dialog-confirm.component.css']
})
export class DialogConfirmComponent {
    public title: string;

    constructor(@Inject(AppService) public appService,
        @Inject(MatDialogRef) public dialogRef: MatDialogRef<DialogConfirmComponent>,
        @Inject(MAT_DIALOG_DATA) data) {
        this.title = data.title;
    }

    cancel() {
        this.dialogRef.close(new DialogConfirmResult(false));
    }

    confirm() {
        this.dialogRef.close(new DialogConfirmResult(true));
    }
}
