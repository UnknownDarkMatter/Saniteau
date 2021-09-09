import { Inject } from '@angular/core';
import { Component, OnInit } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { AppService } from '../../services/AppService';

@Component({
    selector: 'admin-left-navbar',
    templateUrl: 'admin-left-navbar.component.html',
    styleUrls: ['./admin-left-navbar.component.css']
})
export class AdminLeftNavBarComponent implements OnInit {
    constructor(@Inject(Router) private router, @Inject(AppService) public appService) { }

    closeTopMenu() {
        this.appService.topMenuIsShowing = false;
    }

    ngOnInit(): void {
        this.router.navigate(['admin-database']);
    }
}
