export class EnregistreCompteurRequest {
    public idCompteur: number;
    public nomCompteur: string;


    constructor(idCompteur: number, nomCompteur: string) {
        this.idCompteur = idCompteur;
        this.nomCompteur = nomCompteur;
    }

}