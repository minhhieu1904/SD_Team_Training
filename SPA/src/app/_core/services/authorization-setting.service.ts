import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { authorizationSetting, UserRole, Users } from '../models/users';
import { Pagination, PaginationResult } from '../utilities/pagination-utility';
import { OperationResult } from '../utilities/operation-result';
@Injectable({
  providedIn: 'root'
})
export class AuthorizationSettingService {
  apiUrl: string = environment.apiUrl;
  constructor(private http: HttpClient) { }
  getData(pagination: Pagination, param: authorizationSetting) { 
    let params = new HttpParams().appendAll({ ...pagination, ...param});
    return this.http.get<PaginationResult<Users>>(this.apiUrl + "C_AuthorizationSetting/getData", { params: params});
  }

  addNew(users : Users) { 
    return this.http.post<OperationResult>(this.apiUrl + "C_AuthorizationSetting/add", users);
  }

  getItem(account:string){
    //khai bao bien
    let params = new HttpParams().set('account', account);
    return this.http.get<Users>(this.apiUrl + "C_AuthorizationSetting/getDataOnly" , { params });
  }
  update(users: authorizationSetting){
    return this.http.put<OperationResult>(this.apiUrl + "C_AuthorizationSetting/update", users);
  }
  //test
  getAuthorizeByUser(account: string) {
    return this.http.get<UserRole>(this.apiUrl + 'C_AuthorizationSetting/GetAuthorizeByUser', {
      params: { account: account },
    });
  }

  updateAuthorizeByUser(params: UserRole) {
    return this.http.put<OperationResult>(
      this.apiUrl + 'C_AuthorizationSetting/UpdateAuthorizeByUser',
      params
    );
  }
  
}
