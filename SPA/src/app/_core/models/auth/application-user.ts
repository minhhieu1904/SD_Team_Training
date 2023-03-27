
export interface UserLoginParam {
  account: string;
  username: string;
  name: string;
  email: string;
  roles: RoleInformation[];
  roleAll : RoleInformation[];

}

export interface RoleInformation {
  name: string;
  unique: string;
  position: number | null;
}


export interface Userparam {
  user_account: string;
  user_name: string;
}


export interface Role {
  role: string;
}
