import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http'
import { MSDepartment } from '../models/mS_Department'
import { Pagination, PaginationResult } from '@utilities/pagination-utility';
import { OperationResult } from "../utilities/operation-result";
  
@Injectable({
  providedIn: 'root'
})
export class MSDepartmentService {
baseUrl: string = 'https://localhost:5001/api/MSDepartment/';

constructor(
  private _htp: HttpClient
) { }

getDataPaging(pagination: Pagination, param: MSDepartment) {
  let params = new HttpParams().appendAll({...pagination, ...param});
  return this._htp.get<PaginationResult<MSDepartment>>(this.baseUrl + 'GetDataPaging', { params });
}

addNew(msDepartment: MSDepartment) {
  return this._htp.post<OperationResult>(this.baseUrl + 'AddNew', msDepartment);
}

updateItem(msDepartment: MSDepartment) {
  return this._htp.put<OperationResult>(this.baseUrl + 'Edit', msDepartment);
}

getItem(manuf: string, parNo: string) {
  debugger
  let params = new HttpParams().set('manuf', manuf).set('parNo', parNo);
  return this._htp.get<MSDepartment>(this.baseUrl + 'get-item', { params });
}
}
