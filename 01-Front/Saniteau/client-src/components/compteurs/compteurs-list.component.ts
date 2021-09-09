import { Component, Inject, OnInit} from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { COMMA, ENTER } from '@angular/cdk/keycodes';
import { FormControl } from '@angular/forms';
import { map, startWith } from 'rxjs/operators';
import { Observable } from 'rxjs/internal/Observable';
import { of } from 'rxjs';
import { AppService } from '../../services/AppService';
import { HttpService } from '../../services/HttpService';
import { Abonne } from '../../model/abonne/Abonne';
import { MatAutocompleteSelectedEvent } from '@angular/material/autocomplete';
import { EnregistreCompteurRequest } from '../../model/compteur/EnregistreCompteurRequest';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { DialogInfoComponent } from '../dialogs/dialog-info.component';
import { RequestReponse } from '../../model/RequestReponse';
import { Compteur } from '../../model/compteur/Compteur';
import { MatChipInputEvent } from '@angular/material/chips';

export interface CritereRecherche {
    filtre: string;
}

@Component({
    selector: 'compteurs-list',
    templateUrl: 'compteurs-list.component.html',
    styleUrls: ['./compteurs-list.component.css']
})
export class CompteursListComponent implements OnInit {
    public nomCompteurCree: string;
    public compteurs: Compteur[] = [];
    public compteursFromDb: Compteur[] = [];

    readonly separatorKeysCodes: number[] = [ENTER, COMMA];
    criteresRecherche: CritereRecherche[] = [];
    critereRechercheVisible = true;
    critereRechercheSelectable = true;
    critereRechercheRemovable = true;
    critereRechercheAddOnBlur = true;

    constructor(@Inject(AppService) public appService,
        @Inject(HttpService) public httpService,
        @Inject(MatDialog) public infoDialog,
        @Inject(MatSnackBar) public snackBar) {

        this.getAllCompteurs();
    }

    ngOnInit(): void {
    }

    getAllCompteurs() {
        let spinnerDialogRef = this.appService.showSpinner();
        let observable = this.httpService.getAsObservable('Compteurs/GetAllCompteurs');
        observable.subscribe(data => {
            this.compteurs = data as Compteur[];
            this.compteursFromDb = data as Compteur[];
            this.rechercheCompteurs();
            spinnerDialogRef.close();
        }, error => {
            spinnerDialogRef.close();
            this.snackBar.open('Erreur ' + error.status + ' : ' + error.statusText, '', { duration: 3000 });
        });
    }

    creerCompteur() {
        let errorMessage: string = this.validateCreation();
        if (errorMessage != '') {
            this.showInfoDialog('Création de compteur', errorMessage);
            return;
        }
        let enregistreCompteurRequest: EnregistreCompteurRequest = new EnregistreCompteurRequest(0, this.nomCompteurCree);
        let observable = this.httpService.postAsObservable('Compteurs/EnregistreCompteur', enregistreCompteurRequest);
        observable.subscribe(data => {
            let result: RequestReponse = data as RequestReponse;
            if (result.isError) {
                this.snackBar.open('Erreur : ' + result.errorMessage, '', { duration: 2000 });
                return;
            }
            this.nomCompteurCree = '';
            this.getAllCompteurs();
            this.snackBar.open('Le compteur a été créé', '', { duration: 2000 });
        }, error => {
            this.snackBar.open('Erreur ' + error.status + ' : ' + error.statusText, '', { duration: 3000 });
        });
    }

    onDeleted() {
        this.snackBar.open('Le compteur a été supprimé', '', { duration: 2000 });
        this.getAllCompteurs();
    }

    onUpdated(emitedValue) {
        this.getAllCompteurs();
        if (emitedValue) {
            this.snackBar.open('Le compteur a été mis à jour', '', { duration: 2000 });
        }
    }

    validateCreation(): string {
        if (this.isEmptyOrSpaces(this.nomCompteurCree)) {
            return 'Le nom de compteur est requis';
        }

        return '';
    }

    isEmptyOrSpaces(str: string) {
        return str == null || str.trim() == '';
    }

    showInfoDialog(title: string, message: string): void {
        const dialogConfig = new MatDialogConfig();
        dialogConfig.disableClose = false;
        dialogConfig.autoFocus = true;
        dialogConfig.width = '500px';
        dialogConfig.data = { title: title, message: message };
        const dialogRef = this.infoDialog.open(DialogInfoComponent, dialogConfig);
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
        this.rechercheCompteurs();
    }

    removeCritereRecherche(critereRecherche: CritereRecherche): void {
        const index = this.criteresRecherche.indexOf(critereRecherche);

        if (index >= 0) {
            this.criteresRecherche.splice(index, 1);
        }
        this.rechercheCompteurs();
    }

    rechercheCompteurs() {
        var _self = this;
        let compteursFiltre: Compteur[] = [];
        this.compteursFromDb.forEach(function (compteurTmp) {
            let add: boolean = _self.compteurContainsAnyOfTheFiltres(compteurTmp);
            if (add) {
                compteursFiltre.push(compteurTmp);
            }
        });
        this.compteurs = compteursFiltre;
    }

    compteurContainsAnyOfTheFiltres(compteur: Compteur): boolean {
        var _self = this;
        var contains = true;
        this.criteresRecherche.forEach(function (critereRecherche: CritereRecherche) {
            if (!_self.compteurContainsFiltre(compteur, critereRecherche.filtre)) {
                contains = false;
                return;
            }
        });
        return contains;
    }

    compteurContainsFiltre(compteur: Compteur, filtre: string): boolean {
        if (compteur.numeroCompteur.toLowerCase().indexOf(filtre.toLowerCase()) >= 0) {
            return true;
        }
        if (compteur.compteurEstPose && 'Compteur posé'.toLowerCase().indexOf(filtre.toLowerCase()) >= 0) {
            return true;
        }
        if (!compteur.compteurEstPose && 'Compteur non posé'.toLowerCase().indexOf(filtre.toLowerCase()) >= 0) {
            return true;
        }
        if (compteur.compteurEstAppaire && 'Compteur appairé'.toLowerCase().indexOf(filtre.toLowerCase()) >= 0) {
            return true;
        }
        if (!compteur.compteurEstAppaire && 'Compteur non appairé'.toLowerCase().indexOf(filtre.toLowerCase()) >= 0) {
            return true;
        }

        return false;
    }

}
