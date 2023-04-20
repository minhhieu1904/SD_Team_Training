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

export interface UserLoginParam {
  username: string;
  password: string;
}

export interface UserForLogged {
  id: string;
  name: string;
  email: string;
  username: string;
  roles: RoleInfomation[];
  roleAll: RoleInfomation[]
}

export interface RoleInfomation {
  name: string;
  unique: string;
  position: number;
}