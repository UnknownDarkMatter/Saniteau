import { Component, Inject, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { AppService } from '../../services/AppService';

@Component({
    selector: 'delegation-accueil',
    templateUrl: 'delegation-accueil.component.html',
    styleUrls: ['./delegation-accueil.component.css']
})
export class DelegationAccueilComponent implements OnInit {
    private errorMessage: string = "";
    private isEditMode: boolean;

    private nomDelegant: string;
    private nomDelegantControl: FormControl = new FormControl();
    private adresseDelegant: string;
    private adresseDelegantControl: FormControl = new FormControl();
    private dateContratDelegant: Date;
    private dateContratDelegantControl: FormControl = new FormControl();

    constructor(@Inject(AppService) public appService) {
    }

    ngOnInit(): void {
        this.isEditMode = false;
        this.nomDelegant = "Ville de Paris";
        this.adresseDelegant = "1 rue de l'hotel de ville, 75000, PARIS";
        this.dateContratDelegant = new Date(2019, 4-1 , 12, 0, 0, 0, 0);//4 = mai
    }
    private setEditMode(isEditMode: boolean) {
        if (isEditMode) {
            this.nomDelegantControl.setValue(this.nomDelegant);
            this.adresseDelegantControl.setValue(this.adresseDelegant);
            this.dateContratDelegantControl.setValue(this.dateContratDelegant);
        }
        this.isEditMode = isEditMode;
    }

    private creerRetributionsDelegataire() {
        alert('TODO : creerRetributionsDelegataire');
    }

    private updateDelegataire() {
        this.nomDelegant = this.nomDelegantControl.value;
        this.adresseDelegant = this.adresseDelegantControl.value;
        this.dateContratDelegant = this.dateContratDelegantControl.value;
        alert('TODO : updateDelegataire ' + this.nomDelegant + ', ' + this.adresseDelegant + ', ' + this.dateContratDelegant.getDay() + '/' + this.dateContratDelegant.getMonth() + '/' + this.dateContratDelegant.getFullYear());
    }

    private dateToString(date: Date): string {
        return date.getDay().toString().padStart(2, '0') + '/' + date.getMonth().toString().padStart(2, '0') + '/' + date.getFullYear();
        //return date.toDateString();
    }
}
