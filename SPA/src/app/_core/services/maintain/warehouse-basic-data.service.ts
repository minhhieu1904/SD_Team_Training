import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { MsLocation } from '@models/maintain/msLocation';
import { WarehouseBasicDataParam } from '@models/maintain/warehouseBasicDataParam';
import {
  PaginationParam,
  PaginationResult,
} from '@utilities/pagination-utility';
import { environment } from '@env/environment';
import { OperationResult } from '@utilities/operation-result';

@Injectable({
  providedIn: 'root',
})
export class WarehouseBasicDataService {
  baseUrl: string = environment.apiUrl + 'C_WarehouseBasicData';
  constructor(private http: HttpClient) {}

  getData(pagination: PaginationParam, param: WarehouseBasicDataParam) {
    // Cách khai báo thứ nhất

    // let params = new HttpParams()
    // .set('pageNumber', pagination.pageNumber)
    // .set('pageSize', pagination.pageSize)

    // Cách khai báo thứ hai
    let params = new HttpParams().appendAll({ ...pagination, ...param });

    return this.http.get<PaginationResult<MsLocation>>(
      `${this.baseUrl}/GetData`,
      { params }
    );
  }

  getDataOnly(manuf: string, storeH: string) {
    let params = new HttpParams().set('manuf', manuf).set('storeH', storeH);

    return this.http.get<MsLocation>(`${this.baseUrl}/GetDataOnly`, { params });
  }

  add(model: MsLocation) {
    return this.http.post<OperationResult>(`${this.baseUrl}/Add`, model);
  }

  update(model: MsLocation) {
    return this.http.put<OperationResult>(`${this.baseUrl}/Update`, model);
  }

  delete(StoreH: string) {
    return this.http.post<OperationResult>(`${this.baseUrl}/Delete`, {
      param: { StoreH },
    });
  }
}
