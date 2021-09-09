import { Component } from '@angular/core';
import { Inject } from '@angular/core';
import { AppService } from '../../services/AppService';

@Component({
    selector: 'delegation-paye',
    templateUrl: 'delegation-paye.component.html',
    styleUrls: ['./delegation-paye.component.css']
})
export class DelegationPayeComponent {
    constructor(@Inject(AppService) public appService) {
    }
}
