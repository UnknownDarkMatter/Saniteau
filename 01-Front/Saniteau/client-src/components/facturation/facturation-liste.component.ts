import { Component, OnInit, ViewChild } from '@angular/core';
import { Inject } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MatSlideToggleChange } from '@angular/material/slide-toggle';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Abonne } from '../../model/abonne/Abonne';
import { Facturation } from '../../model/facturation/Facturation';
import { FacturationParCampagne } from '../../model/facturation/FacturationParCampagne';
import { AppService } from '../../services/AppService';
import { HttpService } from '../../services/HttpService';
import Swal from 'sweetalert2'
import { PaypalOrder } from '../../model/paiement/PaypalOrder';
import { RequestReponse } from '../../model/RequestReponse';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { DialogPayementRecordingComponent } from '../dialogs/dialog-payment-recording.component';
declare const paypal: any;


@Component({
    selector: 'facturation-liste',
    templateUrl: 'facturation-liste.component.html',
    styleUrls: ['./facturation-liste.component.css'],

})
export class FacturationListeComponent implements OnInit {
    private facturations: Facturation[] = [];
    private facturationsParCampagnes: FacturationParCampagne[] = [];
    private errorMessage: string;

    private filterByPaymentAsString: string;
    private filterByPaymentAsModel: string = "0";

    private filterByAbonneAsNomPrenom: string;
    private filterByAbonneControl: FormControl = new FormControl();
    private filterByAbonneChipList: string[] = [];

    constructor(@Inject(AppService) public appService: AppService,
        @Inject(HttpService) public httpService: HttpService,
        @Inject(Router) private router: Router,
        @Inject(MatSnackBar) public snackBar: MatSnackBar,
        @Inject(MatDialog) public dialog: MatDialog    ) {
   }
    ngOnInit(): void {
        this.getFacturations();
        this.errorMessage = "";
        this.filterByPaymentAsModel = "0";
        this.filterByPaymentAsString = "paye-non-paye";
        this.filterByAbonneControl.valueChanges.subscribe(x => {
            this.filterFacturationsByAbonneOnAutoCompleteClosed();
            this.filterByAbonneChipList = [];
        })
    }

    private creerFacturations() {
        this.clearFilters();
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

    private getFacturations() {
        let observable = this.httpService.getAsObservable('Facturation/ObtenirFacturations');
        observable.subscribe(data => {
            let facturations: Facturation[] = data as Facturation[];
            this.displayFacturations(facturations);
            this.facturations = facturations;
        }, error => {
            this.snackBar.open('Erreur ' + error.status + ' : ' + error.statusText, '', { duration: 3000 });
        });
    }

    private displayFacturations(facturations: Facturation[]) {
        this.facturationsParCampagnes = [];
        for (let facturation of facturations) {
            this.initializeFacturation(facturation);
            let facturationParCampagne = this.getFacturationsParCampagne(facturation.idCampagneFacturation);
            if (facturationParCampagne == null) {
                facturationParCampagne = new FacturationParCampagne(facturation.idCampagneFacturation, facturation.dateFacturation, []);
                this.facturationsParCampagnes.push(facturationParCampagne);
            }
        }
        for (let facturation of facturations) {
            let facturationParCampagne = this.getFacturationsParCampagne(facturation.idCampagneFacturation);
            facturationParCampagne.facturations.push(facturation);
        }
    }

    private ouvrirPopupFactureAsPdf(idFacturation:string) {
        let idFacturationAsNumber: number = +idFacturation;
        let facturation = this.getFacturationsParId(idFacturationAsNumber);
        let idAbonneAsNumber: number = + facturation.abonne.idAbonne;
        Swal.fire({
            title: 'Facture',
            width: '80%',
            html: '<iframe width="100%" height="300" src="/Facturation/ObtenirFacture?idFacturation=' + idFacturation + '&idAbonne=' + facturation.abonne.idAbonne + '" frameborder="0"></iframe>'
        });
    }

    private payFacturation(idFacturation: string) {
        let idFacturationAsNumber: number = +idFacturation;
        let facturation = this.getFacturationsParId(idFacturationAsNumber);
        let idAbonneAsNumber: number = + facturation.abonne.idAbonne;
        let facturationMontantAsString = this.appService.numberToString(this.getFacturationAmountEuros(facturation), 2);
        window['myFacturationListeComponent'] = this; //todo:voir comment créer le bouton en typescript plutot que d'avoir une reference à window
        this.errorMessage = "";

        Swal.fire({
            title: 'Paiement de ' + facturationMontantAsString.replace('.', ',') + ' €',
            width: '80%',
            showCancelButton: true,
            showConfirmButton: false,
            allowOutsideClick: false,
            html: '<span>Test cards : Mastercard 2223016768739313, Visa 4012888888881881</span> <br/><div id="paypal-buttons"></div>'
        });
        this.addPaypalCheckoutButtons(facturationMontantAsString, facturation, idFacturation);
    }

    private enregistrePayment(paymentDetails: any, idFacturation: number) {
        Swal.close();
        const dialogRef = this.dialog.open(DialogPayementRecordingComponent, {});

        let paypalOrder: PaypalOrder = new PaypalOrder();
        paypalOrder.orderId = paymentDetails.id;
        paypalOrder.idFacturation = idFacturation;
        let observable = this.httpService.postAsObservable('/Payment/EnregistrePayment', paypalOrder);
        observable.subscribe(data => {
            let requestResponse: RequestReponse = data as RequestReponse;
            if (requestResponse.isError) {
                dialogRef.close();
                this.snackBar.open(requestResponse.errorMessage, '', { duration: 3000 });
                this.errorMessage = requestResponse.errorMessage;
                return;
            }
            dialogRef.close();
            //this.snackBar.open('Le paiement a réussi', '', { duration: 3000 });
            this.router.navigate(['paiement-success']);
        }, error => {
            dialogRef.close();
            this.snackBar.open('Erreur ' + error.status + ' : ' + error.statusText, '', { duration: 3000 });
        });
        
    }

    private addPaypalCheckoutButtons(facturationMontantAsString: string, facturation: Facturation, idFacturation:string) {
        //https://developer.paypal.com/docs/checkout/integrate/
        let clientId: string = 'AcZ2w1FzoK4FurjtjHHjJMTQIo0eJuiHcDNVMojRiWlXxTMKwrl-BMTVWnuLD9_MmEEx7tZNTm8NbQ9n';
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
            window['myFacturationListeComponent'].enregistrePayment(details, ` + idFacturation + `);
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

    private filterFacturations() {
        var facturations: Facturation[] = [];
        for (let facturation of this.facturations) {
            let addFacturation: boolean;
            addFacturation = true;
            if (!this.facturationMatchesPayeeFilter(facturation, this.filterByPaymentAsString)) {
                addFacturation = false;
            }
            if (!this.facturationMatchesAbonneFilter(facturation, this.filterByAbonneAsNomPrenom)) {
                addFacturation = false;
            }
            if (addFacturation) {
                facturations.push(facturation);
            }
        }
        this.displayFacturations(facturations);
    }

    private filterFacturationsByPayment(filter) {
        this.filterByPaymentAsString = filter;
        this.filterFacturations();
    }

    private filterFacturationsByAbonneOnOptionSelected(nomPrenomAbonne) {
        this.filterByAbonneAsNomPrenom = nomPrenomAbonne;
        this.filterFacturations();
        this.filterByAbonneChipList.push(nomPrenomAbonne);
    }

    private filterFacturationsByAbonneOnAutoCompleteClosed() {
        this.filterByAbonneAsNomPrenom = this.filterByAbonneControl.value;
        this.filterFacturations();
    }

    private filterByAbonneChipListClear() {
        this.filterByAbonneControl.setValue("");
        this.filterByAbonneChipList = [];
    }

    private facturationMatchesPayeeFilter(facturation: Facturation, filter: string): boolean {
        switch (filter) {
            case 'paye-non-paye': {
                return true;
            }
            case 'paye': {
                return facturation.payee;
            }
            case 'non-paye': {
                return !facturation.payee;
            }
            default: {
                break;
            }
        }
    }

    private facturationMatchesAbonneFilter(facturation: Facturation, nomPrenomAbonne: string): boolean {
        if ((nomPrenomAbonne ?? "").replace(" ", "") == "") { return true; }
        return facturation.abonne.prenom + ' ' + facturation.abonne.nom == nomPrenomAbonne;
    }

    private getAbonnes(): Abonne[] {
        var abonnnes: Abonne[] = [];
        for (let facturation of this.facturations) {
            if (abonnnes.filter(abonne => abonne.idAbonne == facturation.abonne.idAbonne).length == 0) {
                abonnnes.push(facturation.abonne);
            }
        }
        return abonnnes;
    }

    private clearFilters() {
        this.filterByPaymentAsModel = "0";
        this.filterByPaymentAsString = "paye-non-paye";
        this.filterByAbonneControl.setValue("");
        this.filterByAbonneChipList = [];
    }

}
