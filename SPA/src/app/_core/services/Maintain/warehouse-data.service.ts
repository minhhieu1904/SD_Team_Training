import { HttpParams, HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@env/environment';
import { MS_Location, MS_LocationParam } from '@models/warehouse';
import { OperationResult } from '@utilities/operation-result';
import { Pagination, PaginationResult } from '@utilities/pagination-utility';
import { BehaviorSubject } from 'rxjs';

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
    return this.http.get<PaginationResult<MS_Location>>(this.baseUrl + 'C_Warehouse/getdata', { params });

  }
  add(params: MS_LocationParam) {
    return this.http.post<OperationResult>(this.baseUrl + 'C_Warehouse/add', params);

  }
  upDate(params: MS_LocationParam) {
    return this.http.put<OperationResult>(this.baseUrl + 'C_Warehouse/upDate', params);
  }
  getItem(manuf: string, location: string) {
    // cach 2 ... la` lay tat ca ben trong models
     let params = new HttpParams().set('manuf', manuf).set('location', location);
    return this.http.get<MS_Location>(this.baseUrl + 'C_Warehouse/getdata', {params} );
  }
}
