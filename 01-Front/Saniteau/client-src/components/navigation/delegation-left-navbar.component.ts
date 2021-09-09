import { Inject } from '@angular/core';
import { Component, OnInit } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { AppService } from '../../services/AppService';

@Component({
    selector: 'delegation-left-navbar',
    templateUrl: 'delegation-left-navbar.component.html',
    styleUrls: ['./delegation-left-navbar.component.css']
})
export class DelegationLeftNavBarComponent implements OnInit {
    constructor(@Inject(Router) private router, @Inject(AppService) public appService) { }

    closeTopMenu() {
        this.appService.topMenuIsShowing = false;
    }

    ngOnInit(): void {
        this.router.navigate(['delegation-accueil']);
    }
}
