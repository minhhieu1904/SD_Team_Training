import { MS_Package } from './../models/mS_Package_DTO';
import { OperationResult } from './../utilities/operation-result';
import {
  Pagination,
  PaginationResult,
} from './../utilities/pagination-utility';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { environment } from '@env/environment';

@Injectable({
  providedIn: 'root',
})
export class StandardPackingQuantitySettingService {
  apiUrl:string = environment.apiUrl;
  msPackage = new BehaviorSubject<MS_Package>(null);
  msPackageCurrent = this.msPackage.asObservable();
  constructor(private http: HttpClient) {}

  getData(
    param: MS_Package,
    pagination: Pagination
  ): Observable<PaginationResult<MS_Package>> {
    let params = new HttpParams()
      .set('PageSize', pagination.pageSize ?? '')
      .set('PageNumber', pagination.pageNumber ?? '')
      .set('PackageNo', param.packageNo ?? '')
      .set('PackageQty', param.packageQty ?? '')
      .set('Manuf', null);
    console.log(params);

    return this.http.get<PaginationResult<MS_Package>>(
      this.apiUrl + 'C_PackingQuantity/Search',
      { params: params }
    );
  }

  addNew(model: MS_Package): Observable<OperationResult> {
    return this.http.post<OperationResult>(this.apiUrl + 'C_PackingQuantity/Create', model);
  }

  update(model: MS_Package): Observable<OperationResult> {
    return this.http.put<OperationResult>(this.apiUrl + 'C_PackingQuantity/Update', model);
  }
}
