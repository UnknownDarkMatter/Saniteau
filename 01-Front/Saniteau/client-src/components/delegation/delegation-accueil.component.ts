import { Component, Inject, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { FormControl } from '@angular/forms';
import { AppService } from '../../services/AppService';
import { HttpService } from '../../services/HttpService';
import { Deleguant } from '../../model/delegation/Deleguant';

@Component({
    selector: 'delegation-accueil',
    templateUrl: 'delegation-accueil.component.html',
    styleUrls: ['./delegation-accueil.component.css']
})
export class DelegationAccueilComponent implements OnInit {
    private errorMessage: string = "";
    private isEditMode: boolean;
    private canSelectDelegant: boolean;

    private deleguants: Deleguant[];
    private deleguant: Deleguant;
    private nomDelegantControl: FormControl = new FormControl();
    private adresseDelegantControl: FormControl = new FormControl();
    private dateContratDelegantControl: FormControl = new FormControl();

    constructor(@Inject(AppService) public appService,
        @Inject(HttpService) public httpService: HttpService,
        @Inject(MatSnackBar) public snackBar: MatSnackBar   ) {
    }

    ngOnInit(): void {
        this.isEditMode = false;
        this.deleguant = null;
        this.canSelectDelegant = true;

        let observable = this.httpService.getAsObservable('DSP/ObtientDeleguants');
        observable.subscribe(data => {
            this.deleguants = this.convertArrayOfDelegants(data);
        }, error => {
            this.snackBar.open('Erreur ' + error.status + ' : ' + error.statusText, '', { duration: 3000 });
        });
    }

    private convertArrayOfDelegants(deleguants: any): Deleguant[] {
        let result: Deleguant[] = [];
        for (var i = 0; i < deleguants.length; i++) {
            let delegantAsAny: any = deleguants[i];
            var dateParts = delegantAsAny.dateContrat.split("/");
            result.push(new Deleguant(delegantAsAny.idDelegant, delegantAsAny.nom, delegantAsAny.adresse,
                new Date(+dateParts[2], dateParts[1] - 1, +dateParts[0])));
        }
        return result;
    }

    private setEditMode(isEditMode: boolean) {
        if (isEditMode && this.deleguant != null) {
            this.nomDelegantControl.setValue(this.deleguant.nom);
            this.adresseDelegantControl.setValue(this.deleguant.adresse);
            this.dateContratDelegantControl.setValue(this.deleguant.dateContrat);
        }
        this.isEditMode = isEditMode;
        this.canSelectDelegant = !isEditMode;
    }

    private updateDelegataire() {
        if (this.deleguant == null) {
            this.deleguant = new Deleguant(0, this.nomDelegantControl.value, this.adresseDelegantControl.value, new Date(this.dateContratDelegantControl.value.toISOString()));
        } else {
            this.deleguant.nom = this.nomDelegantControl.value;
            this.deleguant.adresse = this.adresseDelegantControl.value;
            this.deleguant.dateContrat = new Date(this.dateContratDelegantControl.value.toISOString());
        }
        alert('TODO : updateDelegataire ' + this.deleguant.nom + ', ' + this.deleguant.adresse + ', ' + this.dateToString(this.deleguant.dateContrat));
    }

    private dateToString(date: Date): string {
        return date.getDate().toString().padStart(2, '0') + '/' + (date.getMonth() + 1).toString().padStart(2, '0') + '/' + date.getFullYear();
        //return date.toDateString();
    }

    private onSelectDelegantChange(option: string) {
        if (option == "") {
            this.deleguant = null;
            return;
        }
        this.deleguant = this.deleguants.filter(m => m.nom == option)[0];
    }

    private getDelegantNom() {
        if (this.deleguant == null) { return ""; }
        return this.deleguant.nom;
    }

    private getDelegantAdresse() {
        if (this.deleguant == null) { return ""; }
        return this.deleguant.adresse;
    }

    private getDelegantDateContrat() {
        if (this.deleguant == null) { return ""; }
        return this.dateToString(this.deleguant.dateContrat);
    }

    private creerRetributionsDelegataire() {
        alert('TODO : creerRetributionsDelegataire');
    }
}

