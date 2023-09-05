import { OperationResult } from '@utilities/operation-result';
import { Pagination, PaginationResult } from '@utilities/pagination-utility';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '@env/environment';
import { Injectable } from '@angular/core';
import { AuthorizationParam, RolesUserStatus } from '@models/maintain/authorization-setting';
import { Users } from '@models/common/users';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthorizationSettingService {
  apiUrl = `${environment.apiUrl}C_1_5_AuthorizationSetting/`;
  layoutSource = new BehaviorSubject<Users>(null);

  constructor(private http: HttpClient) { }

  sendDataToOtherComponent(data: Users) {
    this.layoutSource.next(data);
  }

  getData(param: AuthorizationParam, pagination: Pagination) {
    return this.http.get<PaginationResult<Users>>(`${this.apiUrl}GetData`, { params: { ...param, ...pagination } });
  }

  addNew(model: Users) {
    return this.http.post<OperationResult>(`${this.apiUrl}AddNew`, model);
  }

  edit(model: Users) {
    return this.http.put<OperationResult>(`${this.apiUrl}Edit`, model);
  }

  getRoleUser(account: string) {
    let params = new HttpParams().set('account', account);
    return this.http.get<RolesUserStatus[]>(`${this.apiUrl}GetRoleUser`, { params });
  }

  updateRolesUser(data: RolesUserStatus[]) {
    return this.http.post<boolean>(`${this.apiUrl}EditRoleUser`, data);
  }
}
