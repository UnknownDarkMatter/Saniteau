import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Component } from '@angular/core';
import { Inject } from '@angular/core';
import { AppService } from '../../services/AppService';

@Component({
    selector: 'dialog-payment-recording',
    templateUrl: 'dialog-payment-recording.component.html',
    styleUrls: ['./dialog-payment-recording.component.css']
})
export class DialogPayementRecordingComponent {

    constructor(@Inject(AppService) public appService, @Inject(MAT_DIALOG_DATA) data) {
    }

}
