import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from './../../../environments/environment';
import { Pagination, PaginationResult } from './../utilities/pagination-utility'
import { User, UserParam, UserRole } from './../models/maintain/user'
import { BehaviorSubject, Observable } from 'rxjs';
import { OperationResult } from './../utilities/operation-result';

@Injectable({
  providedIn: 'root',
})
export class AuthorizationSettingService {
  constructor(private http: HttpClient) {}

  apiUrl = environment.apiUrl;
  dataSource = new BehaviorSubject<UserParam>(null);
  currentDataSource = this.dataSource.asObservable();

  getAll(pagination: Pagination, param: UserParam){
    let params = new HttpParams().appendAll({...pagination, ...param});
    return this.http.get<PaginationResult<User>>(this.apiUrl + 'AuthorizationSetting/GetAll', {params});
  }

  update(model: User): Observable<OperationResult>{
    return this.http.put<OperationResult>(this.apiUrl + 'AuthorizationSetting/Update' , model);
  }

  create(model: User): Observable<OperationResult>{
    return this.http.post<OperationResult>(this.apiUrl + 'AuthorizationSetting/Create', model);
  }

  getAllRoleByAccount(account: string) {
    return this.http.get<UserRole>(this.apiUrl + 'AuthorizationSetting/GetAllRoleByAccount', {params: {account: account}});
  }

  updateRoleByAccount(model: UserRole): Observable<OperationResult> {
    return this.http.put<OperationResult>(this.apiUrl + 'AuthorizationSetting/UpdateRoleByAccount' , model);
  }
}
