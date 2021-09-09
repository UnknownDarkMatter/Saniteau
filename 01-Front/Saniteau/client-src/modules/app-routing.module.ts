import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AuthGuard } from '../security/AuthGuard';

import { DashboardLeftNavBarComponent } from '../components/navigation/dashboard-left-navbar.component';
import { DashboardComponent } from '../components/dashboard/dashboard.component';
import { AccountCreateComponent } from '../components/account/account-create.component';
import { LoginFormComponent } from '../components/account/login-form.component';

import { AdminLeftNavBarComponent } from '../components/navigation/admin-left-navbar.component';
import { AdminDateComponent } from '../components/admin/date.component';
import { AdminDatabaseComponent } from '../components/admin/database.component';

import { CompteursLeftNavBarComponent } from '../components/navigation/compteurs-left-navbar.component';
import { CompteursListComponent } from '../components/compteurs/compteurs-list.component';
import { CompteursReleveComponent } from '../components/compteurs/compteurs-releve.component';

import { DelegationLeftNavBarComponent } from '../components/navigation/delegation-left-navbar.component';
import { DelegationAccueilComponent } from '../components/delegation/delegation-accueil.component';
import { DelegationPayeComponent } from '../components/delegation/delegation-paye.component';

import { AbonneLeftNavBarComponent } from '../components/navigation/abonne-left-navbar.component';
import { AbonneListeComponent } from '../components/abonne/abonne-liste.component';
import { FacturationListeComponent } from '../components/facturation/facturation-liste.component';


const routes: Routes = [
    { path: '', redirectTo: 'dashboard', pathMatch: 'full' },

    { path: 'login-form', component: LoginFormComponent },
    { path: 'account-create', component: AccountCreateComponent },

    { path: 'dashboard-left-navbar', outlet: "leftnavbaroutlet", canActivate: [AuthGuard], component: DashboardLeftNavBarComponent },
    { path: 'dashboard', canActivate: [AuthGuard], component: DashboardComponent },

    { path: 'admin-left-navbar', outlet: "leftnavbaroutlet", canActivate: [AuthGuard], component: AdminLeftNavBarComponent },
    { path: 'admin-date', canActivate: [AuthGuard], component: AdminDateComponent },
    { path: 'admin-database', canActivate: [AuthGuard], component: AdminDatabaseComponent },

    { path: 'compteurs-left-navbar', outlet: "leftnavbaroutlet", canActivate: [AuthGuard], component: CompteursLeftNavBarComponent },
    { path: 'compteurs-list', canActivate: [AuthGuard], component: CompteursListComponent },
    { path: 'compteurs-releve', canActivate: [AuthGuard], component: CompteursReleveComponent },

    { path: 'delegation-left-navbar', outlet: "leftnavbaroutlet", canActivate: [AuthGuard], component: DelegationLeftNavBarComponent },
    { path: 'delegation-accueil', canActivate: [AuthGuard], component: DelegationAccueilComponent },
    { path: 'delegation-paye', canActivate: [AuthGuard], component: DelegationPayeComponent },

    { path: 'abonne-left-navbar', canActivate: [AuthGuard], outlet: "leftnavbaroutlet", component: AbonneLeftNavBarComponent },
    { path: 'abonne-liste', canActivate: [AuthGuard], component: AbonneListeComponent },
    { path: 'abonne-facturation', canActivate: [AuthGuard], component: FacturationListeComponent },

    { path: '**', redirectTo: 'dashboard' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
