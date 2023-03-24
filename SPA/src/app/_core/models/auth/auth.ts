export interface RoleInformation {
    name: string;
    unique: string;
    position: number | null;
}
export interface ApplicationUser {
    token: string;
    user: User
}
export interface User {
    account: string;
    username: string;
    name: string;
    email: string;
    roles: RoleInformation[]
}
export interface Role {
    role: string;
}