import { MS_Department } from '../models/mS_Department_DTO';
import { Pagination, PaginationResult } from '../utilities/pagination-utility';
import { BehaviorSubject, Observable } from 'rxjs';
import { HttpParams } from '@angular/common/http';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { OperationResult } from '../utilities/operation-result';

@Injectable({
  providedIn: 'root',
})
export class DepartmentDataMaintainService {
  baseUrl: string = 'https://localhost:5001/api/DepartmentDataMaintain/';
  msDepartment = new BehaviorSubject<MS_Department>(null);
  msDepartmentCurrent = this.msDepartment.asObservable();

  constructor(private http: HttpClient) {}

  // lay data dung http.get, them moi data dung http.post, update dung http.put, xóa data dung http.delete

  getData(
    param: MS_Department,
    pagination: Pagination
  ): Observable<PaginationResult<MS_Department>> {
    // {...pagination } này là set lại giá trị cho nó, ban đầu không có thì nó cứ gửi mặc định là 1, giờ nó có thì mình set lại giá trị cho nó
    let params = new HttpParams()
      .set('PageSize', pagination.pageSize ?? '')
      .set('PageNumber', pagination.pageNumber ?? '')
      .set('ParNo', param.parNo ?? '')
      .set('ParName', param.parName ?? '')
      .set('Manuf', 'N');
    return this.http.get<PaginationResult<MS_Department>>(
      this.baseUrl + 'Search',
      {
        params: params,
      }
    );
  }

  // service add new
  addNew(model: MS_Department): Observable<OperationResult> {
    console.log('md', model);
    return this.http.post<OperationResult>(this.baseUrl + 'Create', model);
  }
  update(model: MS_Department): Observable<OperationResult> {
    return this.http.put<OperationResult>(this.baseUrl + 'Update', model);
  }
}
