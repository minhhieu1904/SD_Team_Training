
import { HttpParams, HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@env/environment';
import { MS_Shift, MS_ShiftParam } from '@models/shift';
import { OperationResult } from '@utilities/operation-result';
import { Pagination, PaginationResult } from '@utilities/pagination-utility';

import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class Shift_dataService {
  baseUrl = environment.apiUrl;
  dataSource = new BehaviorSubject<MS_ShiftParam>(null);
  currentDataSource = this.dataSource.asObservable();

  constructor(private http: HttpClient) {}

  // search param truoc roi mới pagination
  getData(searchParam: MS_ShiftParam, pagination: Pagination) {
    // cach 2 ... la` lay tat ca ben trong models
    let params = new HttpParams().appendAll({ ...searchParam, ...pagination });
    return this.http.get<PaginationResult<MS_Shift>>(
      this.baseUrl + 'C_shift/getdata',
      { params }
    );
  }
  add(params: MS_ShiftParam) {
    return this.http.post<OperationResult>(
      this.baseUrl + 'C_shift/add',
      params
    );
  }

  upDate(params: MS_ShiftParam) {
    return this.http.put<OperationResult>(
      this.baseUrl + 'C_shift/update',
      params
    );
  }
  getItem(manuf: string, shift: string) {
    // cach 2 ... la` lay tat ca ben trong models
    let params = new HttpParams().set('manuf', manuf).set('shift', shift);
    return this.http.get<MS_Shift>(
      this.baseUrl + 'C_shift/getdata',
      { params }
    );
  }
}
