import { Component, OnInit, Inject, EventEmitter, Input, Output } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { HttpService } from '../../services/HttpService';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { AppService } from '../../services/AppService';
import { Abonne } from '../../model/abonne/Abonne';
import { Tarification } from '../../model/abonne/Tarification';
import { DeleteAbonneRequest } from '../../model/abonne/DeleteAbonneRequest';
import { EnregistreAbonneRequest } from '../../model/abonne/EnregistreAbonneRequest';
import { DialogConfirmComponent, DialogConfirmResult } from '../dialogs/dialog-confirm.component';
import { DialogInfoComponent, } from '../dialogs/dialog-info.component';

@Component({
    selector: 'abonne',
    templateUrl: 'abonne.component.html',
    styleUrls: ['./abonne.component.css']
})
export class AbonneComponent implements OnInit {
    @Input() abonne: Abonne;
    @Output() deleted = new EventEmitter<boolean>();
    @Output() updated = new EventEmitter<boolean>();
    isEditMode: boolean;

    public nom: string;
    public prenom: string;
    public numeroEtRue: string;
    public ville: string;
    public codePostal: string;
    public tarification: Tarification;
    public tarificationNames: string[];
    public Tarification = Tarification;

    constructor(@Inject(AppService) public appService, @Inject(HttpService) public httpService, @Inject(MatDialog) public confirmDeleteDialog,
        @Inject(MatSnackBar) public snackBar) {
        this.isEditMode = false;
    }
    ngOnInit(): void {
        this.nom = this.abonne.nom;
        this.prenom = this.abonne.prenom;
        this.numeroEtRue = this.abonne.numeroEtRue;
        this.ville = this.abonne.ville;
        this.codePostal = this.abonne.codePostal;

        this.tarification = this.abonne.tarification;
        this.tarificationNames = Object.keys(this.Tarification).filter(f => !isNaN(Number(f)));
    }

    openConfirmDeleteDialog(): void {
        const dialogConfig = new MatDialogConfig();
        dialogConfig.width = '450px';
        dialogConfig.data = { title: "Etes vous sûrs de vouloir supprimer " + this.prenom + " " + this.nom + " ?" };
        const dialogRef = this.confirmDeleteDialog.open(DialogConfirmComponent, dialogConfig);
        dialogRef.afterClosed().subscribe(result => {
            let confirmResult: DialogConfirmResult = result;
            if (confirmResult.confirm) {
                this.deleteAbonne();
            }
        });
    }

    deleteAbonne() {
        let deleteAbonneRequest: DeleteAbonneRequest = new DeleteAbonneRequest(this.abonne.idAbonne);
        let observable = this.httpService.postAsObservable('Compteurs/DeleteAbonne', deleteAbonneRequest);
        observable.subscribe(data => {
            this.deleted.emit(true);
        }, error => {
                this.snackBar.open('Erreur ' + error.status + ' : ' + error.statusText, '', { duration: 3000 });
        });
    }

    setEditMode(value: boolean) {
        this.isEditMode = value;
        if (value == false) {
            this.nom = this.abonne.nom;
            this.prenom = this.abonne.prenom;
            this.numeroEtRue = this.abonne.numeroEtRue;
            this.ville = this.abonne.ville;
            this.codePostal = this.abonne.codePostal;
            this.tarification = this.abonne.tarification;
       }
    }

    updateAbonne() {
        let errorMessage: string = this.validateUpdate();
        if (errorMessage != '') {
            this.showInfoDialog('Mise à jour d\'un abonné', errorMessage);
            return;
        }
        var tarification: number = +this.tarification;
        let enregistreAbonneRequest: EnregistreAbonneRequest = new EnregistreAbonneRequest(this.abonne.idAbonne, this.abonne.idAdresse, this.nom, this.prenom, this.numeroEtRue, this.ville, this.codePostal, tarification);
        let observable = this.httpService.postAsObservable('Compteurs/EnregistreAbonne', enregistreAbonneRequest);
        observable.subscribe(data => {
            this.updated.emit(true);
        }, error => {
                this.snackBar.open('Erreur ' + error.status + ' : ' + error.statusText, '', { duration: 3000 });
        });

        this.isEditMode = false;
    }

    validateUpdate(): string {
        if (this.tarification == Tarification.NonDéfini) {
            return 'La tarification est requise';
        }
        if (this.isEmptyOrSpaces(this.nom)) {
            return 'Le nom est requis';
        }
        if (this.isEmptyOrSpaces(this.prenom)) {
            return 'Le prénom est requis';
        }
        if (this.isEmptyOrSpaces(this.numeroEtRue)) {
            return 'L\'adresse est requise';
        }
        if (this.isEmptyOrSpaces(this.ville)) {
            return 'La ville est requise';
        }
        if (this.isEmptyOrSpaces(this.codePostal)) {
            return 'Le code postal est requis';
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
        const dialogRef = this.confirmDeleteDialog.open(DialogInfoComponent, dialogConfig);
    }

}
