import { Inject } from '@angular/core';
import { Component, OnInit } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { AppService } from '../../services/AppService';

@Component({
    selector: 'compteurs-left-navbar',
    templateUrl: 'compteurs-left-navbar.component.html',
    styleUrls: ['./compteurs-left-navbar.component.css']
})

export class CompteursLeftNavBarComponent implements OnInit {

    constructor(@Inject(Router) private router, @Inject(AppService) public appService) {    }

    closeTopMenu() {
        this.appService.topMenuIsShowing = false;
    }


    ngOnInit(): void {
        this.router.navigate(['compteurs-list']);
    }
}
