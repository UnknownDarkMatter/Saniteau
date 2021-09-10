import { Component, OnInit } from '@angular/core';
import { Inject } from '@angular/core';
import { MatSlideToggleChange } from '@angular/material/slide-toggle';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Abonne } from '../../model/abonne/Abonne';
import { Facturation } from '../../model/facturation/Facturation';
import { FacturationParAbonne } from '../../model/facturation/FacturationParAbonne';
import { FacturationParCampagne } from '../../model/facturation/FacturationParCampagne';
import { AppService } from '../../services/AppService';
import { HttpService } from '../../services/HttpService';

@Component({
    selector: 'facturation-liste',
    templateUrl: 'facturation-liste.component.html',
    styleUrls: ['./facturation-liste.component.css']
})
export class FacturationListeComponent implements OnInit {
    public toogleAbonneCampagneLabel: string;
    public grouperParAbonne: boolean;
    private facturations: Facturation[] =[];
    public facturationsParAbonnes: FacturationParAbonne[] = [];
    public facturationsParCampagnes: FacturationParCampagne[] = [];

    constructor(@Inject(AppService) public appService,
        @Inject(HttpService) public httpService: HttpService,
        @Inject(MatSnackBar) public snackBar: MatSnackBar) {
        this.toogleAbonneCampagneLabel = 'Grouper par facturations';
        this.grouperParAbonne = false;
   }
    ngOnInit(): void {
        this.getFacturations();
    }

    toogleAbonneCampagne(event: MatSlideToggleChange) {
        this.grouperParAbonne = event.checked;
        if (this.grouperParAbonne) {
            this.toogleAbonneCampagneLabel = 'Grouper par abonné';
        } else {
            this.toogleAbonneCampagneLabel = 'Grouper par facturations';
        }
    }

    creerFacturations() {
        let observable = this.httpService.getAsObservable('Facturation/CreerFacturations');
        observable.subscribe(data => {
            let facturations: Facturation[] = data as Facturation[];
            if (facturations.length == this.facturations.length) {
                this.snackBar.open('Il n\'y a pas eu de relevé depuis la dernière facturation', '', { duration: 3000 });
            }
            this.displayFacturations(facturations);
            this.facturations = facturations;
        }, error => {
            this.snackBar.open('Erreur ' + error.status + ' : ' + error.statusText, '', { duration: 3000 });
        });
    }

    getFacturations() {
        let observable = this.httpService.getAsObservable('Facturation/ObtenirFacturations');
        observable.subscribe(data => {
            let facturations: Facturation[] = data as Facturation[];
            this.displayFacturations(facturations);
            this.facturations = facturations;
        }, error => {
            this.snackBar.open('Erreur ' + error.status + ' : ' + error.statusText, '', { duration: 3000 });
        });
    }

    displayFacturations(facturations: Facturation[]) {
        this.facturationsParAbonnes = [];
        this.facturationsParCampagnes = [];
        for (var i = 0; i < facturations.length; i++) {
            var facturation = facturations[i];
            this.initializeFacturation(facturation);
            let facturationParAbonne = this.getFacturationsParAbonne(facturation.abonne);
            if (facturationParAbonne == null) {
                facturationParAbonne = new FacturationParAbonne(facturation.abonne, []);
                this.facturationsParAbonnes.push(facturationParAbonne);
            }
            let facturationParCampagne = this.getFacturationsParCampagne(facturation.idCampagneFacturation);
            if (facturationParCampagne == null) {
                facturationParCampagne = new FacturationParCampagne(facturation.idCampagneFacturation, facturation.dateFacturation, []);
                this.facturationsParCampagnes.push(facturationParCampagne);
            }
        }
        for (var i = 0; i < facturations.length; i++) {
            var facturation = facturations[i];
            let facturationParAbonne = this.getFacturationsParAbonne(facturation.abonne);
            facturationParAbonne.facturations.push(facturation);
            let facturationParCampagne = this.getFacturationsParCampagne(facturation.idCampagneFacturation);
            facturationParCampagne.facturations.push(facturation);
        }
    }

    public openDetails(idFacturation:string) {
        let idFacturationAsNumber: number = +idFacturation;
        let facturation = this.getFacturationsParId(idFacturationAsNumber);
        let idAbonneAsNumber: number = + facturation.abonne.idAbonne;
        alert('TODO: afficher pdf de facture de ' + facturation.abonne.prenom + ' ' + facturation.abonne.nom + ' émise le ' + facturation.dateFacturationAsString);
    }

    public payFacturation(idFacturation: string) {
        let idFacturationAsNumber: number = +idFacturation;
        let facturation = this.getFacturationsParId(idFacturationAsNumber);
        let idAbonneAsNumber: number = + facturation.abonne.idAbonne;
        alert('TODO: payer facture de ' + facturation.abonne.prenom + ' ' + facturation.abonne.nom + ' émise le ' + facturation.dateFacturationAsString);
    }

    private getFacturationsParAbonne(abonne: Abonne) {
        for (var i = 0; i < this.facturationsParAbonnes.length; i++) {
            var facturationParAbonne = this.facturationsParAbonnes[i];
            if (facturationParAbonne.abonne.idAbonne == abonne.idAbonne) {
                return facturationParAbonne;
            }
        }
        return null;
    }

    private getFacturationsParCampagne(idCampagneFacturation : number) {
        for (var i = 0; i < this.facturationsParCampagnes.length; i++) {
            var facturationParCampagne = this.facturationsParCampagnes[i];
            if (facturationParCampagne.idCampagneFacturation == idCampagneFacturation) {
                return facturationParCampagne;
            }
        }
        return null;
    }

    private getFacturationsParId(idFacturation: number): Facturation {
        for (var i = 0; i < this.facturations.length; i++) {
            var facturation = this.facturations[i];
            if (facturation.idFacturation == idFacturation) {
                return facturation;
            }
        }
        return null;
    }

    private getFormatedDate(date: Date): string {
        let date2 = new Date(date);
        let year = date2.getFullYear().toString();
        let month = (date2.getMonth() + 1).toString().padStart(2, '0');
        let day = date2.getDate().toString().padStart(2, '0');
        let hour = date2.getHours().toString().padStart(2, '0');
        let minute = date2.getMinutes().toString().padStart(2, '0');
        let second = date2.getSeconds().toString().padStart(2, '0');
        return day + "/" + month + "/" + year + " à " + hour + ":" + minute + ":" + second;
    }

    //ou are probably trying to call this method on an object dynamically casted from a JSON
    //Dynamically casted object does not have the method defined in the class, they just respect the contract and have the property
    private initializeFacturation(facturation: Facturation) {
        facturation.dateFacturationAsString = this.getFormatedDate(facturation.dateFacturation);
    }
}
