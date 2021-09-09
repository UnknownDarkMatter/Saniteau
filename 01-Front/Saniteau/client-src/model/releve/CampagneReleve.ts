
export class CampagneReleve {
    public idCampagneReleve: number;
    public dateCampagne: Date;
    public consommationUsagersM3: number;
    public consommationPompePrincipaleM3: number;
    public formatedDate: string;
    public pourcentageFuites: number;

    constructor(idCampagneReleve: number, dateCampagne: Date, consommationUsagersM3: number, consommationPompePrincipaleM3: number) {
        this.idCampagneReleve = idCampagneReleve;
        this.dateCampagne = dateCampagne;
        this.consommationUsagersM3 = consommationUsagersM3;
        this.consommationPompePrincipaleM3 = consommationPompePrincipaleM3;
        this.formatedDate = this.getFormatedDate();
        this.pourcentageFuites = this.getPourcentageFuites();
    }

    getFormatedDate(): string {
        let date = new Date(this.dateCampagne);
        let year = date.getFullYear().toString();
        let month = (date.getMonth() + 1).toString().padStart(2, '0');
        let day = date.getDate().toString().padStart(2, '0');
        let hour = date.getHours().toString().padStart(2, '0');
        let minute = date.getMinutes().toString().padStart(2, '0');
        let second = date.getSeconds().toString().padStart(2, '0');
        return year + "/" + month + "/" + day + " " + hour + ":" + minute + ":" + second;
    }

    getPourcentageFuites(): number {
        if (this.consommationPompePrincipaleM3 == 0) {
            return 0;
        }
        let pct = ((this.consommationPompePrincipaleM3 - this.consommationUsagersM3) * 100 / this.consommationPompePrincipaleM3);
        return Math.round(pct * 100) /100;
    }

}