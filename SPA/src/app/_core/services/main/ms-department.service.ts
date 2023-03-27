import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http'
import { MS_Department } from '../../models/common/mS_Department'
import { Pagination, PaginationResult } from '@utilities/pagination-utility';
import { OperationResult } from "../../utilities/operation-result";
import { environment } from '@env/environment';

@Injectable({
  providedIn: 'root'
})
export class MSDepartmentService {
  baseUrl: string = environment.apiUrl + 'C_MSDepartment/';

  constructor(
    private _htp: HttpClient
  ) { }

  getDataPaging(pagination: Pagination, param: MS_Department) {
    let params = new HttpParams().appendAll({ ...pagination, ...param });
    return this._htp.get<PaginationResult<MS_Department>>(this.baseUrl + 'GetDataPaging', { params });
  }

  addNew(msDepartment: MS_Department) {
    return this._htp.post<OperationResult>(this.baseUrl + 'AddNew', msDepartment);
  }

  updateItem(msDepartment: MS_Department) {
    return this._htp.put<OperationResult>(this.baseUrl + 'Edit', msDepartment);
  }

  getItem(manuf: string, parNo: string) {
    // debugger
    let params = new HttpParams().set('manuf', manuf).set('parNo', parNo);
    return this._htp.get<MS_Department>(this.baseUrl + 'get-item', { params });
  }
}
