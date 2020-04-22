export class ApiRegister{
	Email: string;
    Password: string;

    clear(): void {
		this.Email = '';
        this.Password = '';
    }
}
