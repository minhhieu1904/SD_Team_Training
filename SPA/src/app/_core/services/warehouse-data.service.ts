
import { Observable } from 'rxjs';

import { MS_Location, MS_LocationParam } from './../_models/warehouse_data/warehouse_data';
import { OperationResult } from './../utilities/operation-result';
import { environment } from './../../../environments/environment';
import { HttpParams, HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Pagination, PaginationResult } from '../utilities/pagination-utility';

@Injectable({
  providedIn: 'root'
})
export class WarehouseDataService {

  baseUrl = environment.apiUrl;
  dataSources = new BehaviorSubject<MS_LocationParam>(null);
  currentDataSource = this.dataSources.asObservable();
  constructor(private http: HttpClient) { }
  getData(searchParam: MS_LocationParam, pagination: Pagination) {
    let params = new HttpParams().appendAll({ ...searchParam, ...pagination });
    return this.http.get<PaginationResult<MS_Location>>(this.baseUrl + 'C_WarehouseBasicdata/getdata', { params });

  }
  add(params: MS_LocationParam) {
    return this.http.post<OperationResult>(this.baseUrl + ' C_WarehouseBasicdata/add', params);

  }
  upDate(params: MS_LocationParam) {


    return this.http.put<OperationResult>(this.baseUrl + 'C_WarehouseBasicdata/update', params);
  }
  getItem(manuf: string, storeH: string) {
    // cach 2 ... la` lay tat ca ben trong models
    let params = new HttpParams().set('manuf', manuf).set('storeH', storeH);
    return this.http.get<MS_Location>(this.baseUrl + 'C_WarehouseBasicdata/getdata', { params });
  }
}
