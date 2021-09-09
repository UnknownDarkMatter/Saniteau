import { Token } from "./Token";

export class LoginResponse {
    public isError: boolean;
    public errorMessage: string;
    public token: Token;

}
