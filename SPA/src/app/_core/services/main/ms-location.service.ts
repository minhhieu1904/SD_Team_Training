import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Pagination, PaginationResult } from '@utilities/pagination-utility';
import { MSLocationParam, MSLocation } from '@models/common/mS_Location';
import { OperationResult } from '@utilities/operation-result';
import { environment } from '@env/environment';
import { LocationParam } from '@models/maintain/warehouse-basic-data-maintenance';
@Injectable({
  providedIn: 'root'
})
export class MsLocationService {
  baseUrl: string = environment.apiUrl + 'C_MSLocation/';
  constructor(private _htp: HttpClient) { }

  getData(pagination: Pagination, param: MSLocationParam) {
    let params = new HttpParams().appendAll({ ...pagination, ...param });
    return this._htp.get<PaginationResult<MSLocation>>(this.baseUrl + 'GetDataPaging', { params });
  }

  addNew(msLocation: LocationParam) {
    return this._htp.post<OperationResult>(this.baseUrl + 'Add', msLocation);
  }

  updateWarehouse(msLocation: MSLocation) {
    return this._htp.put<OperationResult>(this.baseUrl + 'Edit', msLocation);
  }

  getItem(manuf: string, storeH: string) {
    let params = new HttpParams().set('manuf', manuf).set('storeH', storeH);
    return this._htp.get<MSLocation>(this.baseUrl + 'GetItem', { params });
  }
}
