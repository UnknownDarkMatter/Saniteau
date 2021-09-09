import { Component, OnInit, Inject, EventEmitter, Input, Output } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { HttpService } from '../../services/HttpService';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { AppService } from '../../services/AppService';
import { DialogConfirmComponent, DialogConfirmResult } from '../dialogs/dialog-confirm.component';
import { DialogInfoComponent, } from '../dialogs/dialog-info.component';
import { Compteur } from '../../model/compteur/Compteur';
import { DialogAppairageComponent } from './dialog-appairage.component';

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

    public idCompteur: number;
    public numeroCompteur: string;
    public compteurEstPose: boolean;
    public compteurEstAppaire: boolean;
    public pdlNumero: string;

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
        alert('todo : update d\'un compteur');

        this.isEditMode = false;
    }

    validateUpdate(): string {
        if (this.isEmptyOrSpaces(this.numeroCompteur)) {
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
