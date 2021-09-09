import { Injectable, Inject, OnInit } from '@angular/core';
import { Router, CanActivate } from '@angular/router';
import { Observable } from 'rxjs';
import { of } from 'rxjs';
import { catchError, map } from 'rxjs/operators'
import { LoginResponse } from '../model/account/LoginResponse';
import { HttpService } from '../services/HttpService';
import { AppService } from '../services/AppService';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { LoginDialogComponent } from '../components/account/login-dialog.component';
import { User } from '../model/account/User';

@Injectable({
    providedIn: 'root',
})
export class AuthGuard implements OnInit, CanActivate {
    constructor(@Inject(HttpService) public httpService: HttpService,
        @Inject(AppService) public appService: AppService,
        @Inject(Router) private router: Router,
        @Inject(MatDialog) private loginDialog
        ) { }

    ngOnInit(): void {
        //does not work
        //this.router.onSameUrlNavigation = 'reload';
    }

    canActivate(): Observable<boolean> {
        let user = new User(localStorage.getItem('username'), '', '', '');
        let observable = this.httpService.postAsObservable('Account/IsAuthorized', user);
        return observable.pipe(
            map((response: LoginResponse) => {
                if (response.isError) {
                    this.handleCannotNavigate();
                }
                this.appService.isLoggedIn = !response.isError;
                localStorage.setItem('auth_token', response.token.auth_token);
                return !response.isError;
            }),
            catchError((error) => {
                this.handleCannotNavigate();
                return of(false);
            })
        );
    }

    handleCannotNavigate() {
        let url = this.router.url;
        this.appService.logout();
        if (url == '/login-form') {
            //setTimeout(this.showLoginDialog, 10, this.loginDialog);
            this.showLoginDialog(this.loginDialog);
        }
    }

    showLoginDialog(loginDialog) {
        const dialogConfig = new MatDialogConfig();
        dialogConfig.disableClose = true;
        dialogConfig.width = '500px';
        dialogConfig.data = {};
        loginDialog.open(LoginDialogComponent, dialogConfig);
    }


}