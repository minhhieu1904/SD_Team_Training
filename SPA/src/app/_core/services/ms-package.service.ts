import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http'
import { Pagination, PaginationResult } from '@utilities/pagination-utility';
import { MSPackage } from '../models/mS_Package';
import { OperationResult } from "../utilities/operation-result";

@Injectable({
  providedIn: 'root'
})
export class MSPackageService {
baseUrl: string = 'https://localhost:5001/api/MSPackage/'
constructor(private _htp: HttpClient) { }

getData(pagination: Pagination, param: MSPackage) {
  let params = new HttpParams().appendAll({...pagination, ...param});
  return this._htp.get<PaginationResult<MSPackage>>(this.baseUrl + 'GetDataPaing', { params });
}

addNew(model: MSPackage) {
  return this._htp.post<OperationResult>(this.baseUrl + 'Add' , model);
}

updateItem(model: MSPackage) {
  return this._htp.put<OperationResult>(this.baseUrl + 'Edit', model);
}

getItem(manuf: string, packageNo: string) {
  let params = new HttpParams().set('manuf', manuf).set('packageNo', packageNo);
  return this._htp.get<MSPackage>(this.baseUrl + 'GetItem', { params });
}

}
