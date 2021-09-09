import { PDL } from "./PDL";

export class Compteur {
    public idCompteur: number;
    public numeroCompteur: string;
    public compteurEstPose: boolean;
    public compteurEstAppaire: boolean;
    public pdl: PDL;
    public idAbonne: number;


    constructor(idCompteur: number, numeroCompteur: string, compteurEstPose: boolean, compteurEstAppaire: boolean, pdl: PDL, idAbonne: number) {
        this.idCompteur = idCompteur;
        this.numeroCompteur = numeroCompteur;
        this.compteurEstPose = compteurEstAppaire;
        this.compteurEstAppaire = compteurEstPose;
        this.pdl = pdl;
        this.idAbonne = idAbonne;
    }

}