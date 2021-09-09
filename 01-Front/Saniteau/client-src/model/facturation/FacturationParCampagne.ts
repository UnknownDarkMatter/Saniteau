import { Facturation } from "./Facturation";

export class FacturationParCampagne {
    public idCampagneFacturation: number;
    public dateFacturation: Date;
    public dateFacturationAsString: string;
    public facturations: Facturation[];

    constructor(idCampagneFacturation: number, dateFacturation: Date, facturations: Facturation[]) {
        this.idCampagneFacturation = idCampagneFacturation;
        this.dateFacturation = dateFacturation;
        this.facturations = facturations;
        this.dateFacturationAsString = this.getFormatedDate(dateFacturation);
    }

    private getFormatedDate(date: Date): string {
        let date2 = new Date(date);
        let year = date2.getFullYear().toString();
        let month = (date2.getMonth() + 1).toString().padStart(2, '0');
        let day = date2.getDate().toString().padStart(2, '0');
        let hour = date2.getHours().toString().padStart(2, '0');
        let minute = date2.getMinutes().toString().padStart(2, '0');
        let second = date2.getSeconds().toString().padStart(2, '0');
        return day + "/" + month + "/" + year + " à " + hour + ":" + minute + ":" + second;
    }

}