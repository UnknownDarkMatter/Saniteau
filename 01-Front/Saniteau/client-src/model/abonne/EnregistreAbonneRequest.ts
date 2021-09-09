import { Tarification } from '../../model/abonne/Tarification';


export class EnregistreAbonneRequest {
    public IdAbonne: number;
    public IdAdresse: number;
    public Nom: string;
    public Prenom: string;
    public Adresse: string;
    public Ville: string;
    public CodePostal: string;
    public Tarification: Tarification;

    constructor(idAbonne: number, idAdresse: number, nom: string, prenom: string, adresse: string, ville: string, codePostal: string, tarification: Tarification) {
        this.IdAbonne = idAbonne;
        this.IdAdresse = idAdresse;
        this.Nom = nom;
        this.Prenom = prenom;
        this.Adresse = adresse;
        this.Ville = ville;
        this.CodePostal = codePostal;
        this.Tarification = tarification;
    }
}