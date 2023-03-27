import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@env/environment';
import { ReprintWarehouseScan, ReprintWarehouseScanParam } from '../../models/transaction/reprint-warehouse-scan';
import { Pagination, PaginationResult } from '@utilities/pagination-utility';

@Injectable({
  providedIn: 'root'
})
export class ReprintWarehouseScanService {
  baseUrl: string = `${environment.apiUrl}/C_ReprintWarehouseScan`
  constructor(private http: HttpClient) { }

  getData(param: ReprintWarehouseScanParam, pagination: Pagination) {
    let params = new HttpParams().appendAll({ ...param, ...pagination });
    return this.http.get<PaginationResult<ReprintWarehouseScan>>(`${this.baseUrl}/GetDataPagination`, { params })
  }

  print(dataPrint: ReprintWarehouseScan[]) {
    return this.http.post(`${this.baseUrl}/GetDataDataExport`, { dataPrint });
  }
}
