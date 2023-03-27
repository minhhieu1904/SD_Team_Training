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
}

export interface RoleInfomation {
  name: string;
  unique: string;
  position: number | null;
}
