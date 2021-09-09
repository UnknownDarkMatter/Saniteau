import { ClasseLigneFacturation } from "./ClasseLigneFacturation";

export class FacturationLigne {
    public idFacturationLigne: number;
    public idFacturation: number;
    public classeLigneFacturation: ClasseLigneFacturation;
    public montantEuros: number;
    public consommationM3: number;
    public prixM3: number;


}