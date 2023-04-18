export interface RoleInformation {
  name: string;
  unique: string;
  position: number | null;
}

export interface UserLoginParam {
  account: string;
  password: string;
}

export interface Role {
  role: string;
}

export interface UserForLogged {
  account: string;
  name: string;
  username: string;
  email: string;
  roles: RoleInformation[];
  roleAll: RoleInformation[];
}

