import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppRoutingModule } from './app-routing.module';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatButtonModule } from '@angular/material/button';
import  {MatToolbarModule} from "@angular/material/toolbar";
import { MatTabsModule } from '@angular/material/tabs';
import { MatListModule } from '@angular/material/list';
import { MatSidenavModule } from '@angular/material/sidenav'; 
import { MatRadioModule } from '@angular/material/radio';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card'; 
import { MatInputModule } from '@angular/material/input'; 
import { MatSelectModule } from '@angular/material/select';
import { MatChipsModule } from '@angular/material/chips'; 
import { MatDialogModule } from '@angular/material/dialog'; 
import { MatSnackBarModule } from '@angular/material/snack-bar'; 
import { MatDividerModule} from '@angular/material/divider';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatStepperModule } from '@angular/material/stepper'; 
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatExpansionModule } from '@angular/material/expansion'; 

import { FlexLayoutModule } from '@angular/flex-layout';
import { DataTablesModule } from "angular-datatables";

import { AppComponent } from '../components/app.component';
import { DialogSpinnerComponent } from '../components/dialogs/dialog-spinner.component';
import { AccountCreateComponent } from '../components/account/account-create.component';
import { LoginFormComponent } from '../components/account/login-form.component';
import { LoginDialogComponent } from '../components/account/login-dialog.component';
import { DialogConfirmComponent } from '../components/dialogs/dialog-confirm.component';
import { DialogInfoComponent } from '../components/dialogs/dialog-info.component';
import { DialogPayementRecordingComponent } from '../components/dialogs/dialog-payment-recording.component';

import { DashboardLeftNavBarComponent } from '../components/navigation/dashboard-left-navbar.component';
import { DashboardComponent } from '../components/dashboard/dashboard.component';

import { AdminLeftNavBarComponent } from '../components/navigation/admin-left-navbar.component';
import { AdminDateComponent } from '../components/admin/date.component';
import { AdminDatabaseComponent } from '../components/admin/database.component';

import { CompteursLeftNavBarComponent } from '../components/navigation/compteurs-left-navbar.component';
import { CompteursListComponent } from '../components/compteurs/compteurs-list.component';
import { CompteursReleveComponent } from '../components/compteurs/compteurs-releve.component';
import { CompteurComponent } from '../components/compteurs/compteur.component';
import { DialogAppairageComponent } from '../components/compteurs/dialog-appairage.component';

import { DelegationLeftNavBarComponent } from '../components/navigation/delegation-left-navbar.component';
import { DelegationAccueilComponent } from '../components/delegation/delegation-accueil.component';
import { DelegationPayeComponent } from '../components/delegation/delegation-paye.component';

import { AbonneLeftNavBarComponent } from '../components/navigation/abonne-left-navbar.component';
import { AbonneListeComponent } from '../components/abonne/abonne-liste.component';
import { AbonneComponent } from '../components/abonne/abonne.component';

import { FacturationListeComponent } from '../components/facturation/facturation-liste.component';
import { PaiementSuccessComponent } from '../components/paiement/paiement-success.component';


//import * as ngCore from '@angular/core';
//declare const paypal: any;
//let PayPalButtonModule = paypal.Button.driver('angular2', ngCore);

@NgModule({
    declarations: [
        AppComponent, AccountCreateComponent, LoginFormComponent, LoginDialogComponent, DialogConfirmComponent, DialogInfoComponent,
        DashboardLeftNavBarComponent, DashboardComponent,
        AdminLeftNavBarComponent, AdminDateComponent, AdminDatabaseComponent,
        CompteursLeftNavBarComponent, CompteursListComponent, CompteursReleveComponent, CompteurComponent, DialogAppairageComponent,
        DelegationLeftNavBarComponent, DelegationAccueilComponent, DelegationPayeComponent,
        AbonneComponent, AbonneLeftNavBarComponent, AbonneListeComponent, 
        FacturationListeComponent, PaiementSuccessComponent,
        DialogSpinnerComponent, DialogPayementRecordingComponent
  ],
    imports: [
        CommonModule,
        HttpClientModule,
        BrowserModule,
        FormsModule,
        ReactiveFormsModule,
        AppRoutingModule,
        RouterModule,

        FlexLayoutModule,
        DataTablesModule,

        BrowserAnimationsModule,
        MatButtonModule,
        MatToolbarModule,
        MatTabsModule,
        MatListModule,
        MatSidenavModule,
        MatRadioModule,
        MatIconModule,
        MatCardModule,
        MatInputModule,
        MatSelectModule,
        MatChipsModule,
        MatDialogModule,
        MatSnackBarModule,
        MatDividerModule,
        MatProgressSpinnerModule,
        MatAutocompleteModule,
        MatStepperModule,
        MatSlideToggleModule,
        MatExpansionModule

        //,PayPalButtonModule
  ],
    providers: [],
    bootstrap: [AppComponent],
    entryComponents: [
        AccountCreateComponent, LoginDialogComponent, DialogConfirmComponent, DialogInfoComponent, DialogSpinnerComponent, 
        DialogAppairageComponent, DialogPayementRecordingComponent]
})
export class AppModule { }  

