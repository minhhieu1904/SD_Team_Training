import { environment } from './../../../environments/environment';
import {Users, UserRole } from './../../_core/models/roleUsers';
import { OperationResult } from './../utilities/operation-result';
import { HttpParams } from '@angular/common/http';
import { Pagination, PaginationResult } from './../utilities/pagination-utility';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UsersService {
  apiUrl:string = environment.apiUrl;
  dataSources = new BehaviorSubject<Users>(null);
  currentDataSource = this.dataSources.asObservable();
  constructor(private http: HttpClient) {}

  getData(searchParam: Users, pagination: Pagination) {
    let params = new HttpParams().appendAll({ ...searchParam, ...pagination });
    return this.http.get<PaginationResult<Users>>(this.apiUrl + 'C_Account/Search', {params})
  }

  add(params: Users) {
    return this.http.post<OperationResult>(this.apiUrl + 'C_Account/Create', params)
  }

  update(params: Users) {
    return this.http.put<OperationResult>(this.apiUrl + 'C_Account/Update', params)
  }

  getItem(manuf: string, location: string) {
    let params = new HttpParams().set('manuf', manuf).set('location', location);
    return this.http.get<Users>(this.apiUrl + 'C_Account/Search', {params})
  }

  getAuthorizeByUser(account: string) {
    return this.http.get<UserRole>(this.apiUrl + 'C_Account/GetAuthorizeByUser', {params: { account: account },});
  }

  updateAuthorizeByUser(userRole: UserRole) {
    return this.http.put<OperationResult>(this.apiUrl + 'C_Account/UpdateAuthorizeByUser', userRole);
  }
}
