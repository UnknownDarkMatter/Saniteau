import { Component } from '@angular/core';
import { Inject } from '@angular/core';
import { AppService } from '../../services/AppService';

@Component({
    selector: 'delegation-accueil',
    templateUrl: 'delegation-accueil.component.html',
    styleUrls: ['./delegation-accueil.component.css']
})
export class DelegationAccueilComponent {
    private errorMessage: string = "";

    constructor(@Inject(AppService) public appService) {
    }

    private creerRetributionsDelegataire() {
        alert('TODO : creerRetributionsDelegataire');
    }

}
