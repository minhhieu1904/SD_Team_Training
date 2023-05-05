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
    roleAll: RoleInformation[]
}
export interface Role {
    role: string;
}

export interface UserLogin {
    account: string;
    password: string;
  }
  export interface Userparam {
    user_account: string;
    user_name: string;
  }