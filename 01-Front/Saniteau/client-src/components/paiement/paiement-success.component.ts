import { Component, OnInit } from '@angular/core';
import { Inject } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { AppService } from '../../services/AppService';

@Component({
    selector: 'paiement-success',
    templateUrl: 'paiement-success.component.html',
    styleUrls: ['./paiement-success.component.css']
})
export class PaiementSuccessComponent implements OnInit {
    constructor(@Inject(Router) private router, @Inject(AppService) public appService) {
}


    ngOnInit(): void {
        this.router.navigate([{ outlets: { leftnavbaroutlet: ['abonne-left-navbar'] } }]);
    }
}
