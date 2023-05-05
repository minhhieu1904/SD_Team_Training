import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '@env/environment';
import {
  PaginationParam,
  PaginationResult,
} from '@utilities/pagination-utility';
import { User } from '@models/maintain/role';
import { OperationResult } from '@utilities/operation-result';
import { List_RoleUserParam } from '@models/maintain/list_RoleUserParam';


@Injectable({
  providedIn: 'root',
})
export class AuthorizationSettingService {
  baseUrl: string = environment.apiUrl + 'C_AuthorizationSetting/';
  constructor(private http: HttpClient) {}
  
  getData(pagination: PaginationParam, account: string) {
    let params = new HttpParams().appendAll({ ...pagination, account});
    return this.http.get<PaginationResult<User>>(this.baseUrl + 'GetData', 
    { params });
  }
  getDataOnly(account: string) {
    return this.http.get<User>(this.baseUrl + 'GetDataOnly', {
      params: { account },
    });
  }
  add(user: User) {
    return this.http.post<OperationResult>(this.baseUrl + 'Add', user);
    
  }
  update(user: User){
    return this.http.put<OperationResult>(this.baseUrl + 'Update', user)
  }
  getAuthorizeByUser(account: string) {
    return this.http.get<List_RoleUserParam>(
      this.baseUrl + 'GetAuthorizeByUser',
      { params: { account } }
    )
  }
  updateAuthorizeByUser(listRole: List_RoleUserParam){
    return this.http.put<OperationResult>(this.baseUrl + 'UpdateAuthorizeByUser', listRole)
  }
}
