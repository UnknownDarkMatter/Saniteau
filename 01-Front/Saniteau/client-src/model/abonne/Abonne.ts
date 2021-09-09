import { Tarification } from '../../model/abonne/Tarification';


export class Abonne {
    public idAbonne: number;
    public idAdresse: number;
    public nom: string;
    public prenom: string;
    public tarification: Tarification;
    public numeroEtRue: string;
    public ville :string;
    public codePostal :string;


    constructor(idAbonne: number, idAdresse: number, nom: string, prenom: string, tarification: Tarification, numeroEtRue: string, ville: string, codePostal: string ) {
        this.idAbonne = idAbonne;
        this.idAdresse = idAdresse;
        this.nom = nom;
        this.prenom = prenom;
        this.tarification = tarification;
        this.numeroEtRue = numeroEtRue;
        this.ville = ville;
        this.codePostal = codePostal;
    }

}