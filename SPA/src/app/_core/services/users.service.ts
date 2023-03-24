import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Pagination, PaginationResult } from '@utilities/pagination-utility';
import { UserRole, Users } from '@models/users';
import { OperationResult } from '@utilities/operation-result';

@Injectable({
  providedIn: 'root'
})
export class UsersService {
  baseUrl: string = 'https://localhost:5001/api/Users/';

  constructor(
    private _http: HttpClient
  ) { }

  getData(pagination: Pagination, param: Users) {
    let params = new HttpParams().appendAll({ ...pagination, ...param });
    return this._http.get<PaginationResult<Users>>(this.baseUrl + 'GetData', { params });
  }

  addNew(model: Users) {
    return this._http.post<OperationResult>(this.baseUrl + 'AddNew', model);
  }

  editItem(model: Users) {
    return this._http.put<OperationResult>(this.baseUrl + 'Edit', model);
  }

  getItem(account: string, name: string) {
    let params = new HttpParams().set('account', account).set('name', name);
    return this._http.get<Users>(this.baseUrl + 'GetItem', { params });
  }

  getAuthorizeByUser(account: string) {
    
    return this._http.get<UserRole>(this.baseUrl + 'GetAuthorizeByUser', { params: { account: account } });
  }

  updateAuthorizeByUser(params: UserRole) {
    console.log(params);
    
    return this._http.put<OperationResult>(this.baseUrl + 'UpdateAuthorizeByUser', params);
  }
}


