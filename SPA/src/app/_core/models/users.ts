export interface Users {
    account: string;
    password: string;
    name: string;
    email: string;
    isActive: boolean;
    updateBy: string;
}

export interface UsersParam extends Users {

}