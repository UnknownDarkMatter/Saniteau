
export class User {
    public userName: string;
    public password: string;
    public prenom: string;
    public nom: string;


    constructor(userName: string, password: string, prenom: string, nom: string) {
        this.userName = userName;
        this.password = password;
        this.prenom = prenom;
        this.nom = nom;
    }

}