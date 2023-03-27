export interface Users {
    account: string;
    password: string;
    name: string;
    email: string;
    isActive: boolean;
    updateBy: string;
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

export interface RoleUserDTO {
    user_account: string;
    role_unique: string;
    create_by: string;
    create_time: string;
}
export interface UsersParam extends Users {

}