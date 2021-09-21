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

        let observable = this.httpService.getAsObservable('DSP/ObtientDeleguant');
        observable.subscribe(data => {
            this.deleguant = data as Deleguant;

            if (this.deleguant == null) {
                this.deleguant = new Deleguant(0, "Ville de Paris", "1 rue de l'hotel de ville, 75000, PARIS", new Date(2019, 4 - 1, 12, 0, 0, 0, 0));//4 = mai
            }
        }, error => {
            this.snackBar.open('Erreur ' + error.status + ' : ' + error.statusText, '', { duration: 3000 });
        });

    //    this.nomDelegant = "Ville de Paris";
    //    this.adresseDelegant = "1 rue de l'hotel de ville, 75000, PARIS";
    //    this.dateContratDelegant = new Date(2019, 4 - 1, 12, 0, 0, 0, 0);//4 = mai
    }
    private setEditMode(isEditMode: boolean) {
        if (isEditMode && this.deleguant != null) {
            this.nomDelegantControl.setValue(this.deleguant.nom);
            this.adresseDelegantControl.setValue(this.deleguant.adresse);
            this.dateContratDelegantControl.setValue(this.deleguant.dateContrat);
        }
        this.isEditMode = isEditMode;
    }

    private creerRetributionsDelegataire() {
        alert('TODO : creerRetributionsDelegataire');
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
}

