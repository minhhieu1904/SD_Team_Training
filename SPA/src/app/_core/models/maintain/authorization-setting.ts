import { Users } from './../common/users';
export interface AuthorizationParam {
  account: string,
  name: string,
  is_active: string
}

export interface AuthorizationType {
  users: Users
  type: string;
}

export interface RolesUserStatus {
  user_account: string;
  role_unique: string;
  role_name: string;
  role_sequence: number;
  status: boolean;
}
