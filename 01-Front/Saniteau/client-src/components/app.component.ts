import { Inject } from '@angular/core';
import { Component } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { AppService } from '../services/AppService';

@Component({
    selector: 'app-root',
    templateUrl: 'app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent {
    leftMenuIsShowing: boolean;

    constructor(@Inject(AppService) public appService: AppService) {
        this.appService.topMenuIsShowing = false;
        this.leftMenuIsShowing = true;
    }

    toggleTopMenu() {
        this.appService.topMenuIsShowing = !this.appService.topMenuIsShowing;
    }

    toggleLeftMenu() {
        this.leftMenuIsShowing = !this.leftMenuIsShowing;
    }

    showLeftMenu() {
        this.leftMenuIsShowing = true;
    }

    logout() {
        this.appService.logout();
   }
}
