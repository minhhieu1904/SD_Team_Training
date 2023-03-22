export interface Users {
    account: string;
    password: string;
    name: string;
    email: string;
    is_active: boolean;
    update_by: string;
}
export interface Role {
    role_unique: string;
    role_name: string;
    role_type: string;
    role_note: string;
    role_sequence: number;
    isCheck: boolean;
  }
export interface UserRole {
    account: string;
    listRoles: Role[];
  }
export interface authorizationSetting {
    account: string;
    name: string;
}
export interface authorizationSettingEdit extends authorizationSetting {
    password: string,
    email: string,
    is_active: boolean
}
