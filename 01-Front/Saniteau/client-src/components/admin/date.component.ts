import { Component } from '@angular/core';
import { Inject } from '@angular/core';
import { AppService } from '../../services/AppService';

@Component({
    selector: 'admin-date',
    templateUrl: 'date.component.html',
    styleUrls: ['./date.component.css']
})
export class AdminDateComponent {
    constructor(@Inject(AppService) public appService) {
    }

}
