export interface Role {
    roleUnique: string;
    roleName: string;
    roleType: string;
    roleNote: string;
    roleSequence: number;
    updateBy: string;
    updateTime: string;
}
export interface RoleUser {
    userAccount: string;
    roleUnique: string;
    createBy: string;
    createTime: string;
}
export interface User {
    account: string;
    password: string;
    name: string;
    email: string;
    isActive: boolean;
    updateBy: string;
    updateTime: string;
}
export interface RoleParam {
    userAccount: string;
    roleUnique: string;
    roleName: string;
    roleType: string;
    roleNote: string;
    roleSequence: number;
    isCheck: boolean;
}
export interface UserParam {
    account: string;
    password: string;
    name: string;
    email: string;
    isActive: boolean;
    updateBy: string;
    updateTime: string;
}