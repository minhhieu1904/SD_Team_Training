import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { MsLocation } from '@models/msLocation';
import { WarehouseBasicDataParam } from '@models/warehouseBasicDataParam';
import {
  PaginationParam,
  PaginationResult,
} from '@utilities/pagination-utility';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment';
import { OperationResult } from '../../utilities/operation-result';

@Injectable({
  providedIn: 'root',
})
export class WarehouseBasicDataService {
  baseUrl: string = environment.apiUrl + 'C_WarehouseBasicData';
  constructor(private http: HttpClient) {}

  getData(
    pagination: PaginationParam,
    param: WarehouseBasicDataParam
  ): Observable<PaginationResult<MsLocation>> {
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

  getDataOnly(manuf: string, storeH: string): Observable<MsLocation> {
    let params = new HttpParams().set('manuf', manuf).set('storeH', storeH);

    return this.http.get<MsLocation>(`${this.baseUrl}/GetDataOnly`, { params });
  }

  add(model: MsLocation): Observable<OperationResult> {
    return this.http.post<OperationResult>(`${this.baseUrl}/Add`, model);
  };

  update(model: MsLocation): Observable<OperationResult> {
    return this.http.post<OperationResult>(`${this.baseUrl}/Update`, model);
  };

  delete(StoreH: string): Observable<OperationResult> {
    return this.http.post<OperationResult>(`${this.baseUrl}/Delete`, {param: {StoreH},});
  };
}
