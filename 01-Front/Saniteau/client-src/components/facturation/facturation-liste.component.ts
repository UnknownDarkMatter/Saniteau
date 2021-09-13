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
import Swal from 'sweetalert2'
import { FacturationLigne } from '../../model/facturation/FacturationLigne';
declare const paypal: any;


@Component({
    selector: 'facturation-liste',
    templateUrl: 'facturation-liste.component.html',
    styleUrls: ['./facturation-liste.component.css'],
     
})
export class FacturationListeComponent implements OnInit {
    public toogleAbonneCampagneLabel: string;
    public grouperParAbonne: boolean;
    private facturations: Facturation[] =[];
    public facturationsParAbonnes: FacturationParAbonne[] = [];
    public facturationsParCampagnes: FacturationParCampagne[] = [];

    constructor(@Inject(AppService) public appService: AppService,
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

    openDetails(idFacturation:string) {
        let idFacturationAsNumber: number = +idFacturation;
        let facturation = this.getFacturationsParId(idFacturationAsNumber);
        let idAbonneAsNumber: number = + facturation.abonne.idAbonne;
        Swal.fire({
            title: 'Facture',
            width: '80%',
            html: '<iframe width="100%" height="300" src="/Facturation/ObtenirFacture?idFacturation=' + idFacturation + '&idAbonne=' + facturation.abonne.idAbonne + '" frameborder="0"></iframe>'
        });
    }

    payFacturation(idFacturation: string) {
        let idFacturationAsNumber: number = +idFacturation;
        let facturation = this.getFacturationsParId(idFacturationAsNumber);
        let idAbonneAsNumber: number = + facturation.abonne.idAbonne;
        let facturationMontantAsString = this.appService.numberToString(this.getFacturationAmountEuros(facturation), 2);
        Swal.fire({
            title: 'Paiement de ' + facturationMontantAsString,
            width: '80%',
            showCancelButton: true,
            showConfirmButton: false,
            allowOutsideClick: false,
            html: '<span>Test cards : Mastercard 2223016768739313, Visa 4012888888881881</span> <br/><div id="paypal-buttons"></div>'
        });

        //https://developer.paypal.com/docs/checkout/integrate/
        let clientId: string = 'sb';
        this.loadExternalScript("https://www.paypal.com/sdk/js?client-id=" + clientId + "&currency=EUR&intent=capture").then(() => {

            this.loadInlineScript(`
paypal.Buttons({
    createOrder: function(data, actions) {
      return actions.order.create({
        purchase_units: [{
          amount: {
            value: '` + facturationMontantAsString + `',
            currency_code: 'EUR',
            breakdown: {
                        item_total: {value: '` + facturationMontantAsString + `', currency_code: 'EUR'}
                    }
          },
        items: [{
                    name: 'Facture eau',
                    unit_amount: {value: '` + facturationMontantAsString + `', currency_code: 'EUR'},
                    quantity: '1',
                    category: 'DIGITAL_GOODS'
                }]
        }],
        application_context: {
        	shipping_preference: 'NO_SHIPPING',
      	},
        payer: {
            name: {
                given_name: '` + facturation.abonne.nom + `',
                surname: '` + facturation.abonne.prenom + `'
            }
        }
      });
    },

    onApprove: function(data, actions) {
        return actions.order.capture().then(function(details) {
            alert('Transaction ' + details.id + ' completed by ' + details.payer.name.given_name + '!');
        });
    }

  }).render('#paypal-buttons');
`);
        });


    }

    private loadExternalScript(scriptUrl: string) {
        return new Promise((resolve, reject) => {
            const scriptElement = document.createElement('script')
            scriptElement.src = scriptUrl
            scriptElement.onload = resolve
            document.body.appendChild(scriptElement)
        })
    }
    private loadInlineScript(scriptText: string) {
        return new Promise((resolve, reject) => {
            const scriptElement = document.createElement('script')
            scriptElement.text = scriptText
            scriptElement.onload = resolve
            document.body.appendChild(scriptElement)
        })
    }


    private getFacturationAmountEuros(facturation: Facturation): number {
        let amountEuros: number = 0;
        for (let ligneFacturation of facturation.facturationLignes) {
            amountEuros += ligneFacturation.montantEuros;
        }
        return amountEuros;
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
