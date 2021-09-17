import { Component, OnInit, Inject, EventEmitter, Input, Output } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { HttpService } from '../../services/HttpService';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { AppService } from '../../services/AppService';
import { DialogConfirmComponent, DialogConfirmResult } from '../dialogs/dialog-confirm.component';
import { DialogInfoComponent, } from '../dialogs/dialog-info.component';
import { Compteur } from '../../model/compteur/Compteur';
import { DialogAppairageComponent } from './dialog-appairage.component';
import { FormControl } from '@angular/forms';
import { EnregistreCompteurRequest } from '../../model/compteur/EnregistreCompteurRequest';
import { RequestReponse } from '../../model/RequestReponse';

@Component({
    selector: 'compteur',
    templateUrl: 'compteur.component.html',
    styleUrls: ['./compteur.component.css']
})
export class CompteurComponent implements OnInit {
    @Input() compteur: Compteur;
    @Output() deleted = new EventEmitter<boolean>();
    @Output() updated = new EventEmitter<boolean>();
    isEditMode: boolean;

    private idCompteur: number;
    private numeroCompteur: string;
    private numeroCompteurControl: FormControl = new FormControl();
    private compteurEstPose: boolean;
    private compteurEstAppaire: boolean;
    private pdlNumero: string;

    constructor(@Inject(AppService) public appService,
        @Inject(HttpService) public httpService,
        @Inject(MatDialog) public confirmDialog,
        @Inject(MatSnackBar) public snackBar,
        @Inject(MatDialog) public appairageDialog)
    {
        this.isEditMode = false;
    }
    ngOnInit(): void {
        this.idCompteur = this.compteur.idCompteur;
        this.numeroCompteur = this.compteur.numeroCompteur;
        this.numeroCompteurControl.setValue(this.compteur.numeroCompteur);
        this.compteurEstPose = this.compteur.compteurEstPose;
        this.compteurEstAppaire = this.compteur.compteurEstAppaire;
        this.pdlNumero = this.compteur.pdl != null ? this.compteur.pdl.numeroPDL : '';
    }

    openConfirmDeleteDialog(): void {
        const dialogConfig = new MatDialogConfig();
        dialogConfig.width = '450px';
        dialogConfig.data = { title: "Etes vous sûrs de vouloir supprimer le compteur " + this.numeroCompteur + " ?" };
        const dialogRef = this.confirmDialog.open(DialogConfirmComponent, dialogConfig);
        dialogRef.afterClosed().subscribe(result => {
            let confirmResult: DialogConfirmResult = result;
            if (confirmResult.confirm) {
                this.deleteCompteur();
            }
        });
    }

    deleteCompteur() {
        alert('todo : supprimer compteur');
    }

    setEditMode(value: boolean) {
        this.isEditMode = value;
        if (value == false) {
            this.idCompteur = this.compteur.idCompteur;
            this.numeroCompteur = this.compteur.numeroCompteur;
            this.compteurEstPose = this.compteur.compteurEstPose;
            this.compteurEstAppaire = this.compteur.compteurEstAppaire;
            this.pdlNumero = this.compteur.pdl != null ? this.compteur.pdl.numeroPDL : '';
       }
    }

    updateCompteur() {
        let errorMessage: string = this.validateUpdate();
        if (errorMessage != '') {
            this.showInfoDialog('Mise à jour d\'un compteur', errorMessage);
            return;
        }
        let spinnerDialogRef = this.appService.showSpinner();
        let enregistreCompteurRequest: EnregistreCompteurRequest = new EnregistreCompteurRequest(this.idCompteur, this.numeroCompteurControl.value);
        let observable = this.httpService.postAsObservable('Compteurs/EnregistreCompteur', enregistreCompteurRequest);
        observable.subscribe(data => {
            let result: RequestReponse = data as RequestReponse;
            if (result.isError) {
                this.snackBar.open('Erreur : ' + result.errorMessage, '', { duration: 2000 });
                return;
            }
            this.numeroCompteur = this.numeroCompteurControl.value;
            this.snackBar.open('Le compteur a été enregistré', '', { duration: 2000 });
            this.isEditMode = false;
            spinnerDialogRef.close();
        }, error => {
            this.snackBar.open('Erreur ' + error.status + ' : ' + error.statusText, '', { duration: 3000 });
            this.isEditMode = false;
            spinnerDialogRef.close();
        });
    }

    validateUpdate(): string {
        if (this.isEmptyOrSpaces(this.numeroCompteurControl.value)) {
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
        const dialogRef = this.confirmDialog.open(DialogInfoComponent, dialogConfig);
    }

    openCompteurAppairage() {
        const dialogConfig = new MatDialogConfig();
        dialogConfig.disableClose = false;
        dialogConfig.data = { title: "Appairage d'un compteur", compteur: this.compteur };
        const dialogRef = this.appairageDialog.open(DialogAppairageComponent, dialogConfig);
        dialogRef.afterClosed().subscribe(result => {
            this.updated.emit(false);
        });
    }

}
