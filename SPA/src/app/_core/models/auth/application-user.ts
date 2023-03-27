export interface ApplicationUser {
    token: string,
    user: User
}

export interface RoleInformation {
    name: string;
    unique: string;
    position: number | null;
}

export interface User {
    account: string;
    username: string;
    name: string;
    email: string;
    roles: RoleInformation[]
}