import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@env/environment';
import { OperationResult } from '@utilities/operation-result';
import {
  Pagination,
  PaginationParam,
  PaginationResult,
} from '../../utilities/pagination-utility';
import { Observable } from 'rxjs';
import { MsDepartment, DepartmentDataMaintainParam } from '../../models/maintain/msDepartment';

@Injectable({
  providedIn: 'root',
})
export class DepartmentDataMaintainService {
  baseUrl: string = environment.apiUrl + 'C_DepartmentDataMaintain';

  constructor(private http: HttpClient) {}

  getData(
    pagination: Pagination, 
    param: DepartmentDataMaintainParam
    ): Observable<PaginationResult<MsDepartment>>{
      let params = new HttpParams().appendAll({...pagination, ...param});
      return this.http.get<PaginationResult<MsDepartment>>(
        `${this.baseUrl}/GetData`, {params}
      );
  }
  getDataOnly(manuf: string, parNo: string): Observable<MsDepartment> {
    let params = new HttpParams().set('manuf', manuf).set('parNo', parNo);

    return this.http.get<MsDepartment>(`${this.baseUrl}/GetDataOnly`, {
      params,
    });
  }
  add(model:MsDepartment){
    return this.http.post<OperationResult>
    (`${this.baseUrl}/Add`, model)
  }
  update(model:MsDepartment){
    return this.http.put<OperationResult>
    (`${this.baseUrl}/Update`, model)}
  delete(){}
}
