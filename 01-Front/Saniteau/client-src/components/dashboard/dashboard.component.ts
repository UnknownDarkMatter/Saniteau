import { Component, OnInit } from '@angular/core';
import { Inject } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { AppService } from '../../services/AppService';

@Component({
    selector: 'dashboard',
    templateUrl: 'dashboard.component.html',
    styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
    constructor(@Inject(Router) private router, @Inject(AppService) public appService) {
}


    ngOnInit(): void {
        this.router.navigate([{ outlets: { leftnavbaroutlet: ['dashboard-left-navbar'] } }]);
    }
}
