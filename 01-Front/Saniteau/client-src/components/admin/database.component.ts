import { Component } from '@angular/core';
import { Inject } from '@angular/core';
import { AppService } from '../../services/AppService';

@Component({
    selector: 'admin-database',
    templateUrl: 'database.component.html',
    styleUrls: ['./database.component.css']
})

export class AdminDatabaseComponent {
    constructor(@Inject(AppService) public appService) {
    }
}
