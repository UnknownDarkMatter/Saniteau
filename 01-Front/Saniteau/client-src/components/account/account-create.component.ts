import { MAT_DIALOG_DATA, MatDialogRef, MatDialogConfig, MatDialog } from '@angular/material/dialog';
import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Inject } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { AppService } from '../../services/AppService';
import { HttpService } from '../../services/HttpService';
import { User } from '../../model/account/User';
import { LoginResponse } from '../../model/account/LoginResponse';
import { LoginDialogComponent } from '../../components/account/login-dialog.component';

export class AccountCreateResult {
    isCreated: boolean;

    constructor(isCreated: boolean) {
        this.isCreated = isCreated;
    }
}

@Component({
    selector: 'account-create',
    templateUrl: 'account-create.component.html',
    styleUrls: ['./account-create.component.css']
})
export class AccountCreateComponent implements OnInit {
    public nom: string;
    public prenom: string;
    public userName: string;
    public password: string;
    public errorMessage: string;

    constructor(@Inject(AppService) public appService,
        @Inject(MatDialogRef) public dialogRef: MatDialogRef<AccountCreateComponent>,
        @Inject(MatDialog) public loginDialog,
        @Inject(MAT_DIALOG_DATA) data,
        @Inject(HttpService) public httpService,
        @Inject(Router) private router,
        @Inject(MatSnackBar) public snackBar
    ) { }


    ngOnInit(): void {
    }

    createAccount() {
        this.errorMessage = '';
        let user = new User(this.userName, this.password, this.prenom, this.nom);
        let observable = this.httpService.postAsObservable('Account/CreateAccount', user);
        observable.subscribe(data => {
            let createAccountResponse: LoginResponse = data;
            if (createAccountResponse.isError) {
                this.errorMessage = createAccountResponse.errorMessage;
                return;
            }
            this.dialogRef.close();
            localStorage.setItem('auth_token', createAccountResponse.token.auth_token);
            localStorage.setItem('username', this.userName);
            this.appService.isLoggedIn = true;
            this.router.navigate(['dashboard']);
            return;
        }, error => {
                this.snackBar.open('Erreur ' + error.status + ' : ' + error.statusText, '', { duration: 3000 });
        });
    }

    login() {
        this.dialogRef.close();
        const dialogConfig = new MatDialogConfig();
        dialogConfig.disableClose = true;
        dialogConfig.width = '500px';
        dialogConfig.data = {};
        var dialogRef = this.loginDialog.open(LoginDialogComponent, dialogConfig);
    }

    onKeypressEvent(event: any) {
        var charCode = (event.which) ? event.which : event.keyCode;
        if (charCode == 13) {
            this.createAccount();
        }
    }
}
