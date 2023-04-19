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
  position: number ;
}


export interface UserparamLogin {
  account: string;
  password: string;
}


export interface Role {
  role: string;
}



