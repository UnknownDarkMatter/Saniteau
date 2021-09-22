export class Deleguant {

    public idDelegant: number;
    public nom: string;
    public adresse: string;
    public dateContrat: Date;

    constructor(idDelegant: number, nom: string, adresse: string, dateContrat: Date) {
        this.idDelegant = idDelegant;
        this.nom = nom;
        this.adresse = adresse;
        this.dateContrat = dateContrat;
    }

}