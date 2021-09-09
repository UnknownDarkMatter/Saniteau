import { MAT_DIALOG_DATA, MatDialogRef, MatDialogConfig, MatDialog } from '@angular/material/dialog';
import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Inject } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { AppService } from '../../services/AppService';
import { HttpService } from '../../services/HttpService';
import { AccountCreateComponent } from './account-create.component';
import { User } from '../../model/account/User';
import { LoginResponse } from '../../model/account/LoginResponse';

export class LoginResult {
    userName: string;
    password: string;

    constructor(userName: string, password: string) {
        this.userName = userName;
        this.password = password;
    }
}

@Component({
    selector: 'login-dialog',
    templateUrl: 'login-dialog.component.html',
    styleUrls: ['./login-dialog.component.css']
})
export class LoginDialogComponent implements OnInit {
    public userName: string;
    public password: string;
    public errorMessage: string;
    public showSpinner: boolean;

    constructor(@Inject(AppService) public appService: AppService,
        @Inject(MatDialogRef) public dialogRef: MatDialogRef<LoginDialogComponent>,
        @Inject(MAT_DIALOG_DATA) data,
        @Inject(MatDialog) public creerAccountDialog,
        @Inject(HttpService) public httpService,
        @Inject(Router) private router,
        @Inject(MatSnackBar) public snackBar) {
    }
    ngOnInit(): void {
        this.showSpinner = false;
    }

    login() {
        this.showSpinner = true;
        this.errorMessage = '';
        localStorage.setItem('username', '');
        let user = new User(this.userName, this.password, '', '');
        let observable = this.httpService.postAsObservable('Account/Login', user);
        observable.subscribe(data => {
            let loginResponse: LoginResponse = data;
            if (loginResponse.isError) {
                this.errorMessage = loginResponse.errorMessage;
                this.showSpinner = false;
                return;
            }
            this.dialogRef.close();
            localStorage.setItem('auth_token', loginResponse.token.auth_token);
            this.appService.isLoggedIn = true;
            localStorage.setItem('username', this.userName);
            this.showSpinner = false;

            this.router.navigate(['dashboard']);
            return;
        }, error => {
                this.showSpinner = false;
                this.snackBar.open('Erreur ' + error.status + ' : ' + error.statusText, '', { duration: 3000 });
        });
    }

    creerAccount() {
        this.dialogRef.close(new LoginResult(this.userName, this.password));
        const dialogConfig = new MatDialogConfig();
        dialogConfig.disableClose = true;
        dialogConfig.width = '500px';
        dialogConfig.data = {};
        var dialogRef = this.creerAccountDialog.open(AccountCreateComponent, dialogConfig);
    }

    onKeypressEvent(event: any) {
        var charCode = (event.which) ? event.which : event.keyCode;
        if (charCode == 13) {
            this.login();
        }
    }
}
