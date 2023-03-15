import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { MS_Location, WarehouseDataBasic } from '../models/warehouse-basic-data';
import { Pagination, PaginationResult } from '../utilities/pagination-utility';
import { OperationResult } from '../utilities/operation-result';
@Injectable({
  providedIn: 'root'
})
export class S_warehouse_basic_dataService {
  apiUrl: string = environment.apiUrl;
  constructor(private http: HttpClient) { }
  getData(pagination: Pagination, param: WarehouseDataBasic) {
    let params = new HttpParams().appendAll({ ...pagination, ...param });
    return this.http.get<PaginationResult<MS_Location>>(this.apiUrl + "C_WarehouseBasicData/getData", { params: params });
  }

  addNew(mslocation: MS_Location) {
    return this.http.post<OperationResult>(this.apiUrl + "C_WarehouseBasicData/add", mslocation);
  }

  getItem(manuf: string, storeH: string) {
    //khai bao bien
    let params = new HttpParams().set('manuf', manuf).set('storeH', storeH);
    return this.http.get<MS_Location>(this.apiUrl + "C_WarehouseBasicData/getDataOnly", { params });
  }
  update(mslocation: MS_Location) {
    return this.http.post<OperationResult>(this.apiUrl + "C_WarehouseBasicData/update", mslocation);
  }
}
