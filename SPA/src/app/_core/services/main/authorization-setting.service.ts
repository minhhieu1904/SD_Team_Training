import { OperationResult } from '@utilities/operation-result';

import { RolesUserStatus } from '@models/maintain/authorization-setting';
import { PaginationParam, PaginationResult } from '@utilities/pagination-utility';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Users } from '@models/common/users';


import { BehaviorSubject, Subject } from 'rxjs';
import { environment } from '@env/environment';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthorizationSettingService {
  apiUrl = environment.apiUrl;
  userAuthorization = new Subject<Users>();
  shareDataSubject = this.userAuthorization.asObservable();
  constructor(private http: HttpClient) { }

  sendDataToOtherComponent(data: Users) {
    this.userAuthorization.next(data)
  }

  getDataUsers(pagination: PaginationParam) {
    let params = new HttpParams().appendAll({ ...pagination });
    return this.http.get<PaginationResult<Users>>(`${this.apiUrl}S_AuthorizationSetting/getDataUsers`, { params });
  }

  getRoleUser(account: string) {
    let params = new HttpParams().set('account', account);
    return this.http.get<RolesUserStatus[]>(`${this.apiUrl}S_AuthorizationSetting/getRoleUsers`, { params });
  }

  updateAuthorUser(data: RolesUserStatus[]) {
    return this.http.post<boolean>(`${this.apiUrl}S_AuthorizationSetting​/updateAuthorUser`, data);
  }

  createUser(data: Users) {
    return this.http.post<OperationResult>(`${this.apiUrl}S_AuthorizationSetting/AddUser`, data);
  }

  updateUser(data: Users) {
    return this.http.put<OperationResult>(`${this.apiUrl}S_AuthorizationSetting/EditUser`, data);
  }
}
