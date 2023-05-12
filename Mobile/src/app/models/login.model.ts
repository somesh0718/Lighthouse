export class LoginModel {
    UserId: string;
    Password: string;
    IsMobile: boolean;
    RememberMe: boolean;
    AuthToken: string;

    constructor() {
        this.UserId = 'sushmagawco88@gmail.vom';
        this.Password = 'Pass$123';
        this.RememberMe = true;
        this.RememberMe = false;
        this.AuthToken = '';
    }
}
