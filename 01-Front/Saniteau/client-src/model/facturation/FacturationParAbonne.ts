import { Abonne } from "../abonne/Abonne";
import { Facturation } from "./Facturation";

export class FacturationParAbonne {
    public abonne: Abonne;
    public facturations: Facturation[];

    constructor(abonne: Abonne, facturations: Facturation[]) {
        this.abonne = abonne;
        this.facturations = facturations;
    }
}