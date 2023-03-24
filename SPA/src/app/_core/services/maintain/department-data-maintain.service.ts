import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@env/environment';
import { OperationResult } from '@utilities/operation-result';
import {
  PaginationParam,
  PaginationResult,
} from '@utilities/pagination-utility';
import { Observable } from 'rxjs';
import { DepartmentDataParam } from '../../models/maintain/departmentDataParam';
import { MsDepartment } from '../../models/maintain/msDepartment';

@Injectable({
  providedIn: 'root',
})
export class DepartmentDataMaintainService {
  baseUrl: string = environment.apiUrl + 'C_DepartmentData';

  constructor(private http: HttpClient) {}

  getData(
    pagination: PaginationParam,
    param: DepartmentDataParam
  ): Observable<PaginationResult<MsDepartment>> {
    let params = new HttpParams().appendAll({ ...pagination, ...param });

    return this.http.get<PaginationResult<MsDepartment>>(
      `${this.baseUrl}/GetData`,
      { params }
    );
  }
  getDataOnly(manuf: string, parNo: string): Observable<MsDepartment> {
    let params = new HttpParams().set('manuf', manuf).set('parNo', parNo);

    return this.http.get<MsDepartment>(`${this.baseUrl}/GetDataOnly`, {
      params,
    });
  }

  add(model: MsDepartment): Observable<OperationResult> {
    return this.http.post<OperationResult>(`${this.baseUrl}/Add`, model);
  }

  update(model: MsDepartment): Observable<OperationResult> {
    return this.http.put<OperationResult>(`${this.baseUrl}/Update`, model);
  }

  delete(parNo: string): Observable<OperationResult> {
    return this.http.post<OperationResult>(`${this.baseUrl}/Delete`, {
      params: { parNo },
    });
  }
}
