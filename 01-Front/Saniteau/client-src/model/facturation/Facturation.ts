import { Abonne } from "../abonne/Abonne";
import { FacturationLigne } from "./FacturationLigne";

export class Facturation {
    public idFacturation: number;
    public idCampagneFacturation: number;
    public abonne: Abonne;
    public dateFacturation: Date;
    public dateFacturationAsString: string;
    public facturationLignes: FacturationLigne[];
    public idDernierIndex: number;
    public payee: boolean;
}