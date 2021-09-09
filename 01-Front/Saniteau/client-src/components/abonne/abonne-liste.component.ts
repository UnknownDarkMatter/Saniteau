import { Component, Inject } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatChipInputEvent } from '@angular/material/chips';
import { COMMA, ENTER } from '@angular/cdk/keycodes';
import { HttpService } from '../../services/HttpService';
import { AppService } from '../../services/AppService';
import { Abonne } from '../../model/abonne/Abonne';
import { Tarification } from '../../model/abonne/Tarification';
import { EnregistreAbonneRequest } from '../../model/abonne/EnregistreAbonneRequest';
import { DialogInfoComponent, } from '../dialogs/dialog-info.component';

export interface CritereRecherche {
    filtre: string;
}

@Component({
    selector: 'abonne-liste',
    templateUrl: 'abonne-liste.component.html',
    styleUrls: ['./abonne-liste.component.css']
})
export class AbonneListeComponent {
    public abonnes: Abonne[];
    public abonnesFromDb: Abonne[];

    public nom: string;
    public prenom: string;
    public adresse: string;
    public ville: string;
    public codepostal: string;
    public tarification: Tarification;
    public tarificationNames: string[];
    public Tarification = Tarification;

    public abonne: Abonne;

    readonly separatorKeysCodes: number[] = [ENTER, COMMA];
    criteresRecherche: CritereRecherche[] = [];
    critereRechercheVisible = true;
    critereRechercheSelectable = true;
    critereRechercheRemovable = true;
    critereRechercheAddOnBlur  = true;

    constructor(@Inject(AppService) public appService: AppService, @Inject(HttpService) public httpService, @Inject(MatDialog) public confirmDeleteDialog,
                        @Inject(MatSnackBar) public snackBar){
        this.getAllAbonnes();
        this.tarificationNames = Object.keys(this.Tarification).filter(f => !isNaN(Number(f)));;
        this.getAllAbonnes();
        this.tarification = Tarification.NonDéfini;
    }

    getAllAbonnes() {
        let spinnerDialogRef = this.appService.showSpinner();
        let observable = this.httpService.getAsObservable('Compteurs/GetAllAbonnes?filtrerAbonnesAvecCompteurs=false');
        observable.subscribe(data => {
            this.abonnes = data as Abonne[];
            this.abonnesFromDb = data as Abonne[];
            this.rechercheAbonnes();
            spinnerDialogRef.close();
        }, error => {
            spinnerDialogRef.close();
            this.snackBar.open('Erreur ' + error.status + ' : ' + error.statusText, '', { duration: 3000 });
        });
    }

    creerAbonne() {
        let errorMessage: string = this.validateCreation();
        if (errorMessage != '') {
            this.showInfoDialog('Création d\'abonné', errorMessage);
            return;
        }
        var tarification: number = +this.tarification;
        let createAbonneRequest: EnregistreAbonneRequest = new EnregistreAbonneRequest(0, 0, this.nom, this.prenom, this.adresse, this.ville, this.codepostal, tarification);
        let observable = this.httpService.postAsObservable('Compteurs/EnregistreAbonne', createAbonneRequest);
        observable.subscribe(data => {
            this.abonne = data;
            this.nom = "";
            this.prenom = "";
            this.adresse = "";
            this.ville = "";
            this.codepostal = "";
            this.tarification = Tarification.NonDéfini;

            this.getAllAbonnes();
            this.snackBar.open('L\'abonné a été créé', '', {duration: 2000});
        }, error => {
                this.snackBar.open('Erreur ' + error.status + ' : ' + error.statusText, '', { duration: 3000 });
        });
    }

    onDeleted() {
        this.snackBar.open('L\'abonné a été supprimé', '', { duration: 2000 });
        this.getAllAbonnes();
    }

    onUpdated() {
        this.snackBar.open('L\'abonné a été mis à jour', '', { duration: 2000 });
        this.getAllAbonnes();
    }

    validateCreation(): string {
        if (this.tarification == Tarification.NonDéfini) {
            return 'La tarification est requise';
        }            
        if (this.isEmptyOrSpaces(this.nom)) {
            return 'Le nom est requis';
        }
        if (this.isEmptyOrSpaces(this.prenom)) {
            return 'Le prénom est requis';
        }
        if (this.isEmptyOrSpaces(this.adresse)) {
            return 'L\'adresse est requise';
        }
        if (this.isEmptyOrSpaces(this.ville)) {
            return 'La ville est requise';
        }
        if (this.isEmptyOrSpaces(this.codepostal)) {
            return 'Le code postal est requis';
        }
        return '';
    }

    isEmptyOrSpaces(str: string) {
        return str == null || str.trim() == '';
    }

    showInfoDialog(title : string, message : string): void {
        const dialogConfig = new MatDialogConfig();
        dialogConfig.disableClose = false;
        dialogConfig.autoFocus = true;
        dialogConfig.width = '500px';
        dialogConfig.data = { title: title, message: message };
        const dialogRef = this.confirmDeleteDialog.open(DialogInfoComponent, dialogConfig);
    }


    addCritereRecherche(event: MatChipInputEvent): void {
      const input = event.input;
        const value = event.value;

        // Add our fruit
        if ((value || '').trim()) {
            this.criteresRecherche.push({ filtre: value.trim() });
        }

        // Reset the input value
        if (input) {
            input.value = '';
        }
        this.rechercheAbonnes();
    }

    removeCritereRecherche(critereRecherche: CritereRecherche): void {
        const index = this.criteresRecherche.indexOf(critereRecherche);

        if (index >= 0) {
            this.criteresRecherche.splice(index, 1);
        }
        this.rechercheAbonnes();
    }

    rechercheAbonnes() {
        var _self = this;
        let abonnesFiltre: Abonne[] = [];
        this.abonnesFromDb.forEach(function (abonneeTmp) {
            let add: boolean = _self.abonneContainsAnyOfTheFiltres(abonneeTmp);
            if (add) {
                abonnesFiltre.push(abonneeTmp);
            }
        });
        this.abonnes = abonnesFiltre;
    }

    abonneContainsAnyOfTheFiltres(abonne: Abonne): boolean {
        var _self = this;
        var contains = true;
        this.criteresRecherche.forEach(function (critereRecherche: CritereRecherche) {
            if (!_self.abonneContainsFiltre(abonne, critereRecherche.filtre)){
                contains = false;
                return;
            }
        });
        return contains;
    }

    abonneContainsFiltre(abonne: Abonne, filtre: string): boolean {
        if (abonne.prenom.toLowerCase().indexOf(filtre.toLowerCase()) >= 0) {
            return true;
        }
        if (abonne.nom.toLowerCase().indexOf(filtre.toLowerCase()) >= 0) {
            return true;
        }
        if (abonne.numeroEtRue.toLowerCase().indexOf(filtre.toLowerCase()) >= 0) {
            return true;
        }
        if (abonne.ville.toLowerCase().indexOf(filtre.toLowerCase()) >= 0) {
            return true;
        }
        if (abonne.codePostal.toLowerCase().indexOf(filtre.toLowerCase()) >= 0) {
            return true;
        }
        if (Tarification[abonne.tarification].toLowerCase().indexOf(filtre.toLowerCase()) >= 0) {
            return true;
        }
        
        return false;
    }
}
