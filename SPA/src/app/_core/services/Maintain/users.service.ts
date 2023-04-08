import { Pagination, PaginationResult } from '@utilities/pagination-utility';
import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BehaviorSubject } from 'rxjs';
import { UserRole, Users } from '@models/maintain/roleUsers';
import { environment } from '@env/environment';
import { OperationResult } from '@utilities/operation-result';


@Injectable({
  providedIn: 'root',
})
export class UsersService {
  baseUrl = environment.apiUrl;
  dataSources = new BehaviorSubject<Users>(null);
  currentDataSource = this.dataSources.asObservable();
  constructor(private http: HttpClient) {}

  getData(searchParam: Users, pagination: Pagination) {
    let params = new HttpParams().appendAll({ ...searchParam, ...pagination });
    return this.http.get<PaginationResult<Users>>(
      this.baseUrl + 'C_Account/getData',
      { params }
    );
  }
  add(params: Users) {
    return this.http.post<OperationResult>(
      this.baseUrl + 'C_Account/add',
      params
    );
  }
  upDate(params: Users) {
    return this.http.put<OperationResult>(
      this.baseUrl + 'C_Account/update',
      params
    );
  }
  getItem(manuf: string, location: string) {
    // cach 2 ... la` lay tat ca ben trong models
    let params = new HttpParams().set('manuf', manuf).set('location', location);
    return this.http.get<Users>(this.baseUrl + 'C_Account/getData', {
      params,
    });
  }

  getAuthorizeByUser(account: string) {
    return this.http.get<UserRole>(this.baseUrl + 'C_Account/GetAuthorizeByUser', {
      params: { account: account },
    });
  }

  updateAuthorizeByUser(params: UserRole) {
    return this.http.put<OperationResult>(
      this.baseUrl + 'C_Account/UpdateAuthorizeByUser',
      params
    );
  }
}
