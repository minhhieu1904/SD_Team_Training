import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import {
  Pagination,
  PaginationResult,
} from '../../utilities/pagination-utility';
import { UserParam } from '../../models/maintain/userParam';
import { User } from '../../models/maintain/user';
import { OperationResult } from '../../utilities/operation-result';
import { List_RoleUserParam } from '../../models/maintain/list_RoleUserParam';

@Injectable({
  providedIn: 'root',
})
export class AuthorizationSettingService {
  baseUrl: string = environment.apiUrl + 'C_Authorization';
  constructor(private http: HttpClient) {}

  add(user: User) {
    return this.http.post<OperationResult>(this.baseUrl + '/Add', user);
  }

  update(user: User) {
    return this.http.put<OperationResult>(this.baseUrl + '/Update', user);
  }

  getData(pagination: Pagination, account: string) {
    let params = new HttpParams().appendAll({ ...pagination, account });
    return this.http.get<PaginationResult<User>>(this.baseUrl + '/GetData', {
      params,
    });
  }

  getDataOnly(account: string) {
    return this.http.get<User>(this.baseUrl + '/GetDataOnly', {
      params: { account },
    });
  }
  getAuthorizeByUser(account: string) {
    return this.http.get<List_RoleUserParam>(
      this.baseUrl + '/GetAuthorizeByUser',
      { params: { account } }
    );
  }

  updateAuthorizeByUser(listRole: List_RoleUserParam) {
    return this.http.put<OperationResult>(
      this.baseUrl + '/UpdateAuthorizeByUser',
      listRole
    );
  }
}
