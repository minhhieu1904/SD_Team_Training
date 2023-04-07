import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {
  MS_DepartmentParam,
  MS_Department,
} from './../models/maintain/msdepartment';
import {
  Pagination,
  PaginationResult,
} from './../utilities/pagination-utility';
import { environment } from './../../../environments/environment';
import { OperationResult } from '@utilities/operation-result';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class DepartmentDataMaintenanceService {
  constructor(private http: HttpClient) { }
  apiUrl = environment.apiUrl;
  dataSource = new BehaviorSubject<MS_DepartmentParam>(null);
  currentDataSource = this.dataSource.asObservable();

  getAll(pagination: Pagination, param: MS_DepartmentParam) {
    let params = new HttpParams().appendAll({ ...pagination, ...param });

    return this.http.get<PaginationResult<MS_Department>>(
      this.apiUrl + 'DepartmentDataMaintenance/GetAll',
      { params }
    );
  }

  search(pageNumber?, pageSize?, text?: string) {
    let params = new HttpParams();
    if (pageNumber != null && pageSize != null) {
      params = params.append('pageNumber', pageNumber);
      params = params.append('pageSize', pageSize);
    }
    params = params.append('text', text);
    return this.http.get<PaginationResult<MS_Department>>(
      this.apiUrl + 'DepartmentDataMaintenance/Search',
      { params }
    );
  }

  create(model: MS_Department): Observable<OperationResult> {
    return this.http.post<OperationResult>(this.apiUrl + 'DepartmentDataMaintenance/Create', model);
  }

  update(model: MS_Department): Observable<OperationResult> {
    return this.http.put<OperationResult>(this.apiUrl + 'DepartmentDataMaintenance/Update', model);
  }
}
