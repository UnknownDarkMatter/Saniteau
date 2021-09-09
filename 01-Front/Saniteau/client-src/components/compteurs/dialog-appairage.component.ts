import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Component, EventEmitter, OnInit, Output, ViewChild } from '@angular/core';
import { Inject } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Observable } from 'rxjs';
import { map, startWith } from 'rxjs/operators';
import { MatAutocomplete, MatAutocompleteSelectedEvent, MatAutocompleteTrigger } from '@angular/material/autocomplete';
import { AppService } from '../../services/AppService';
import { MatStepper } from '@angular/material/stepper';
import { HttpService } from '../../services/HttpService';
import { Compteur } from '../../model/compteur/Compteur';
import { MatSnackBar } from '@angular/material/snack-bar';
import { RequestReponse } from '../../model/RequestReponse';
import { Abonne } from '../../model/abonne/Abonne';
import { AdressePDL } from '../../model/compteur/AdressePDL';
import { MatChip, MatChipList, MatChipListChange } from '@angular/material/chips';
import { Tarification } from '../../model/abonne/Tarification';


@Component({
    selector: 'dialog-appairage',
    templateUrl: 'dialog-appairage.component.html',
    styleUrls: ['./dialog-appairage.component.css']
})
export class DialogAppairageComponent implements OnInit {
    @ViewChild('stepper') stepper: MatStepper;
    @ViewChild(MatChipList) abonneAssociationChipList: MatChipList;
    @ViewChild(MatAutocompleteTrigger) abonneAssociationAutocompleteTrigger: MatAutocompleteTrigger;

    public title: string;
    public compteur: Compteur;
    //stepper inputs
    poseCompteurFormGroup: FormGroup;
    associePDLFormGroup: FormGroup;
    appaireCompteurFormGroup: FormGroup;
    //abonne PDL autocomplete
    public selectedAbonneForCreation: Abonne = null;
    abonnesForCreation: Abonne[] = [];
    abonnesFilteredForCreation: Abonne[] = [];
    public abonnesSansCompteurList: Observable<Abonne[]>;
    public chipsSelectedForCreationSelectable = true;
    public chipsSelectedForCreationRemovable = true;
    //abonne PDL generalités
    public isAdressePDLCree: boolean = false;

    constructor(@Inject(AppService) public appService,
        @Inject(MatDialogRef) public dialogRef: MatDialogRef<DialogAppairageComponent>,
        @Inject(MAT_DIALOG_DATA) data,
        @Inject(FormBuilder) private _formBuilder: FormBuilder,
        @Inject(HttpService) public httpService: HttpService,
        @Inject(MatSnackBar) public snackBar) {
        this.title = data.title;
        this.compteur = data.compteur;
    }
    ngOnInit(): void {
        this.poseCompteurFormGroup = this._formBuilder.group({
            poseCompteurCtrl: ['', Validators.required]
        });
        this.associePDLFormGroup = this._formBuilder.group({
            myCreateAbonneControl: ['', Validators.required],
            associeAdressePDLControl: ['', Validators.required],
        });
        this.appaireCompteurFormGroup = this._formBuilder.group({
            appaireCompteurCtrl: ['', Validators.required]
        });
        this.updatePoseCompteurFormGroup();
        this.updateAssociePDLFormGroup();
        this.initAbonnesList();
        this.selectedAbonneForCreation = null;
        this.updateAppairageCompteurFormGroup();
    }

    //fonctions générales
    close() {
        this.dialogRef.close();
    }

    isEmptyOrSpaces(str: string) {
        return str == null || str.trim() == '';
    }

    //Pose Retrait compteur
    updatePoseCompteurFormGroup() {
        this.poseCompteurFormGroup.patchValue({ poseCompteurCtrl: '' });
        let observable = this.httpService.postAsObservable('Compteurs/GetCompteur', this.compteur.idCompteur);
        observable.subscribe(data => {
            this.compteur = data as Compteur;
            if (this.compteur.compteurEstPose) {
                this.poseCompteurFormGroup.patchValue({ poseCompteurCtrl: 'pose' });
            } else {
                this.clearAssociationAbonnePDL();
           }
        }, error => {
            this.poseCompteurFormGroup.patchValue({ poseCompteurCtrl: '' });
            this.snackBar.open('Erreur ' + error.status + ' : ' + error.statusText, '', { duration: 3000 });
        });
    }

    poserCompteur() {
        let observable = this.httpService.postAsObservable('Compteurs/PoseCompteur', this.compteur);
        observable.subscribe(data => {
            let response = data as RequestReponse;
            if (response.isError) {
                this.snackBar.open('Erreur ' + response.errorMessage, '', { duration: 3000 });
            }
            this.updatePoseCompteurFormGroup();
        }, error => {
            this.snackBar.open('Erreur ' + error.status + ' : ' + error.statusText, '', { duration: 3000 });
            this.updatePoseCompteurFormGroup();
        });
    }
    retirerCompteur() {
        let observable = this.httpService.postAsObservable('Compteurs/RetireCompteur', this.compteur);
        observable.subscribe(data => {
            let response = data as RequestReponse;
            if (response.isError) {
                this.snackBar.open('Erreur ' + response.errorMessage, '', { duration: 3000 });
            }
            this.updatePoseCompteurFormGroup();
        }, error => {
            this.snackBar.open('Erreur ' + error.status + ' : ' + error.statusText, '', { duration: 3000 });
            this.updatePoseCompteurFormGroup();
        });
    }

    //Association abonné : autocomplete
    private initAbonnesList() {
        let observable = this.httpService.getAsObservable('Compteurs/GetAllAbonnes?filtrerAbonnesAvecCompteurs=true');
        observable.subscribe(data => {
            this.abonnesForCreation = data as Abonne[];
            this.abonnesSansCompteurList = this.associePDLFormGroup.get('myCreateAbonneControl').valueChanges.pipe(
                startWith(''),
                map(value => {
                    return this._filter(value);
                })
            );
        }, error => {
            this.snackBar.open('Erreur ' + error.status + ' : ' + error.statusText, '', { duration: 3000 });
            this.abonnesForCreation = [];
        });
    }

    private _filter(value: string): Abonne[] {
        if (value == null) { return; }
        const filterValue = value.toLowerCase();
        const filteredState = this.abonnesForCreation.filter(option => this.getDisplayName(option).toLowerCase().indexOf(filterValue) >= 0);
        return filteredState;
    }

    getDisplayName(abonne: Abonne) {
        return abonne.prenom + ' ' + abonne.nom + ', ' + abonne.numeroEtRue + ' ' + abonne.ville + ' ' + abonne.codePostal;
    }

    onSelectedAbonneForCreation(event: MatAutocompleteSelectedEvent) {
        this.selectedAbonneForCreation = this.abonnesForCreation.filter(m => this.getDisplayName(m) == event.option.value)[0];
        this.abonnesFilteredForCreation = this.abonnesForCreation.filter(m => this.getDisplayName(m) == event.option.value);
        let input: any = document.getElementById('myAssociePDLCtrl');
        input.value = '';
    }

    removeChipsSelectedForCreation(abonne) {
        this.selectedAbonneForCreation = null;
        this.abonnesFilteredForCreation = [];
        this.abonnesSansCompteurList = this.associePDLFormGroup.get('myCreateAbonneControl').valueChanges.pipe(
            startWith(''),
            map(value => {
                return this._filter(value);
            })
        );
        this.associePDLFormGroup.patchValue({ myCreateAbonneControl: '' });
    }

    //Association abonné : association adresse PDL
    updateAssociePDLFormGroup() {
        this.associePDLFormGroup.patchValue({ associeAdressePDLControl: '' });
        this.isAdressePDLCree = false;
        this.chipsSelectedForCreationRemovable = true;
        this.associePDLFormGroup.controls['myCreateAbonneControl'].enable();
        let observable = this.httpService.postAsObservable('Compteurs/GetCompteur', this.compteur.idCompteur);
        observable.subscribe(data => {
            this.compteur = data as Compteur;
            if (this.compteur.pdl != null && !this.isEmptyOrSpaces(this.compteur.pdl.numeroPDL)) {

                let observable = this.httpService.getAsObservable('Compteurs/GetAbonne?idAbonne=' + this.compteur.idAbonne );
                observable.subscribe(data => {
                    let abonne = data as Abonne;
                    this.selectedAbonneForCreation = abonne;
                    this.abonnesFilteredForCreation = [];
                    this.abonnesFilteredForCreation.push(abonne);
                    this.associePDLFormGroup.patchValue({ associeAdressePDLControl: 'pdl cree' });
                    this.isAdressePDLCree = true;
                    this.chipsSelectedForCreationRemovable = false;
                    this.associePDLFormGroup.controls['myCreateAbonneControl'].disable();
                }, error => {
                    this.isAdressePDLCree = false;
                    this.associePDLFormGroup.patchValue({ associeAdressePDLControl: '' });
                    this.snackBar.open('Erreur ' + error.status + ' : ' + error.statusText, '', { duration: 3000 });
                });
            }
        }, error => {
            this.isAdressePDLCree = false;
            this.associePDLFormGroup.patchValue({ associeAdressePDLControl: '' });
            this.snackBar.open('Erreur ' + error.status + ' : ' + error.statusText, '', { duration: 3000 });
        });
    }

    clearAssociationAbonnePDL() {
        this.abonnesFilteredForCreation = [];
        this.isAdressePDLCree = false;
        this.associePDLFormGroup.patchValue({ associeAdressePDLControl: '' });
        this.chipsSelectedForCreationRemovable = true;
        this.associePDLFormGroup.controls['myCreateAbonneControl'].enable();
        this.initAbonnesList();
        this.clearAppairage();
    }

    associeAbonnePDL() {
        if (this.selectedAbonneForCreation == null) {
            this.associePDLFormGroup.patchValue({ associeAdressePDLControl: '' });
            return;
        }
        let adressePDL: AdressePDL = new AdressePDL(this.compteur.idCompteur, this.selectedAbonneForCreation.idAdresse);
        let observable = this.httpService.postAsObservable('Compteurs/AssocieAdressePDL', adressePDL);
        observable.subscribe(data => {
            let compteur = data as Compteur;
            this.compteur = compteur;
            this.updateAssociePDLFormGroup();
        }, error => {
            this.snackBar.open('Erreur ' + error.status + ' : ' + error.statusText, '', { duration: 3000 });
            this.updateAssociePDLFormGroup();
        });
    }

    dissocieAbonnePDL() {
        if (this.selectedAbonneForCreation == null) {
            this.associePDLFormGroup.patchValue({ associeAdressePDLControl: '' });
            return;
        }
        let adressePDL: AdressePDL = new AdressePDL(this.compteur.idCompteur, this.selectedAbonneForCreation.idAdresse);
        let observable = this.httpService.postAsObservable('Compteurs/DissocieAdressePDL', adressePDL);
        observable.subscribe(data => {
            let compteur = data as Compteur;
            this.compteur = compteur;
            this.updateAssociePDLFormGroup();
            this.initAbonnesList();
            this.clearAppairage();
        }, error => {
            this.snackBar.open('Erreur ' + error.status + ' : ' + error.statusText, '', { duration: 3000 });
            this.updateAssociePDLFormGroup();
        });
    }

    //appairage
    updateAppairageCompteurFormGroup() {
        this.appaireCompteurFormGroup.patchValue({ appaireCompteurCtrl: '' });
        let observable = this.httpService.postAsObservable('Compteurs/GetCompteur', this.compteur.idCompteur);
        observable.subscribe(data => {
            this.compteur = data as Compteur;
            if (this.compteur.compteurEstAppaire) {
                this.appaireCompteurFormGroup.patchValue({ appaireCompteurCtrl: 'appairé' });
            }
        }, error => {
            this.appaireCompteurFormGroup.patchValue({ appaireCompteurCtrl: '' });
            this.snackBar.open('Erreur ' + error.status + ' : ' + error.statusText, '', { duration: 3000 });
        });
    }

    clearAppairage() {
        this.appaireCompteurFormGroup.patchValue({ appaireCompteurCtrl: '' });
    }

    appairerCompteur() {
        let observable = this.httpService.postAsObservable('Compteurs/AppaireCompteur', this.compteur);
        observable.subscribe(data => {
            let response = data as RequestReponse;
            if (response.isError) {
                this.snackBar.open('Erreur ' + response.errorMessage, '', { duration: 3000 });
            }
            this.updateAppairageCompteurFormGroup();
        }, error => {
            this.snackBar.open('Erreur ' + error.status + ' : ' + error.statusText, '', { duration: 3000 });
            this.updateAppairageCompteurFormGroup();
        });
    }

    desappairerCompteur() {
        let observable = this.httpService.postAsObservable('Compteurs/DesappaireCompteur', this.compteur);
        observable.subscribe(data => {
            let response = data as RequestReponse;
            if (response.isError) {
                this.snackBar.open('Erreur ' + response.errorMessage, '', { duration: 3000 });
            }
            this.updateAppairageCompteurFormGroup();
        }, error => {
            this.snackBar.open('Erreur ' + error.status + ' : ' + error.statusText, '', { duration: 3000 });
            this.updateAppairageCompteurFormGroup();
        });
    }

    //changeStep(index: number) {
    //    let canChange: boolean = true;
    //    switch (index) {
    //        case 0: {
    //            break;
    //        }
    //        case 1: {
    //            if (this.stepper.selectedIndex + 1 == index) {
    //                alert(this.poseCompteurFormGroup.valid);
    //                canChange = this.poseCompteurFormGroup.valid;
    //            }
    //            break;
    //        }
    //        case 2: {
    //            if (this.stepper.selectedIndex + 1 == index) {
    //                canChange = this.associePDLFormGroup.valid;
    //            }
    //            break;
    //        }
    //        case 3: {
    //            if (this.stepper.selectedIndex + 1 == index) {
    //                canChange = this.appaireCompteurFormGroup.valid;
    //            }
    //            break;
    //        }
    //    }
    //    if (canChange) {
    //        this.stepper.linear = false;
    //        this.stepper.selectedIndex = index;
    //        setTimeout(() => {
    //            this.stepper.linear = true;
    //        });
    //    }
    //}
}
