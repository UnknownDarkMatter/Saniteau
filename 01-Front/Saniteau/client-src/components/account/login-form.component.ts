import { Component, OnInit } from '@angular/core';
import { Inject } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { HttpService } from '../../services/HttpService';
import { AppService } from '../../services/AppService';
import { LoginDialogComponent, LoginResult } from '../account/login-dialog.component';
import { User } from '../../model/account/User';
import { LoginResponse } from '../../model/account/LoginResponse';

@Component({
    selector: 'login-form',
    templateUrl: 'login-form.component.html',
    styleUrls: ['./login-form.component.css']
})
export class LoginFormComponent implements OnInit {
    constructor(@Inject(AppService) public appService: AppService, @Inject(Router) private router, @Inject(MatDialog) public loginDialog, @Inject(HttpService) public httpService) {
    }
    ngOnInit(): void {
        this.showLoginDialog();
    }

    showLoginDialog() {
        const dialogConfig = new MatDialogConfig();
        dialogConfig.disableClose = true;
        dialogConfig.width = '500px';
        dialogConfig.data = {};
        var dialogRef = this.loginDialog.open(LoginDialogComponent, dialogConfig);
        dialogRef.afterClosed().subscribe(result => {
        });
    }
}
