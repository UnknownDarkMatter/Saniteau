import { Inject } from '@angular/core';
import { Component, OnInit } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { AppService } from '../../services/AppService';

@Component({
    selector: 'abonne-left-navbar',
    templateUrl: 'abonne-left-navbar.component.html',
    styleUrls: ['./abonne-left-navbar.component.css']
})
export class AbonneLeftNavBarComponent implements OnInit {
    constructor(@Inject(Router) private router, @Inject(AppService) public appService) { }

    closeTopMenu() {
        this.appService.topMenuIsShowing = false;
    }

    ngOnInit(): void {
        this.router.navigate(['abonne-liste']);
    }
}
