import { Inject } from '@angular/core';
import { Injectable } from '@angular/core';
import { MatDialog, MatDialogConfig, MatDialogRef } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { DialogSpinnerComponent } from '../components/dialogs/dialog-spinner.component';

@Injectable({
    providedIn: 'root',
})
export class AppService {
    public PageTitle: string;
    public topMenuIsShowing: boolean;
    public isLoggedIn: boolean;
    public loginName: string;

    constructor(@Inject(Router) private router: Router,
        @Inject(MatDialog) public spinnerDialog,
    ) {
        this.PageTitle = "(non défini)";
        this.topMenuIsShowing = false;
    }

    logout() {
        this.isLoggedIn = false;
        this.loginName = '';
        localStorage.removeItem('auth_token');
        localStorage.removeItem('username');
        this.router.navigate(['login-form']);
    }

    showSpinner() {
        const dialogConfig = new MatDialogConfig();
        dialogConfig.disableClose = false;
        dialogConfig.width = '500px';
        dialogConfig.data = {};
        return this.spinnerDialog.open(DialogSpinnerComponent, dialogConfig);
    }
}