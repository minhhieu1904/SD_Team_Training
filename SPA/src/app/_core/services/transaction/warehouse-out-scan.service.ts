import { OperationResult } from './../../utilities/operation-result';
import { WarehouseOutScan, WarehouseOutScanPickingMainDto, StorageOut } from './../../models/transaction/warehouse-out-scan';
import { PaginationParam, PaginationResult } from './../../utilities/pagination-utility';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BehaviorSubject } from 'rxjs';
import { environment } from './../../../../environments/environment.prod';
import { Injectable } from '@angular/core';
import { PickingScanItemParam, WarehouseOutScanParam, WarehouseOutScanSource } from '../../_helpers/params/transaction/warehouseOutScanParam';
import { MS_QR_PickingMain } from '../../models/common/mS_QR_PickingMain';

@Injectable({
  providedIn: 'root'
})
export class WarehouseOutScanService {
  baseUrl: string = `${environment.apiUrl}C_WarehouseOutScan/`;
  layoutSource = new BehaviorSubject<WarehouseOutScanSource>(null);
  currentLayout = this.layoutSource.asObservable();
  constructor(private http: HttpClient) { }

  getDataMain(pagination: PaginationParam, filterParam: WarehouseOutScanParam) {
    let params = new HttpParams().appendAll({ ...pagination, ...filterParam });
    return this.http.get<PaginationResult<WarehouseOutScan>>(`${this.baseUrl}GetDataMain`, { params });
  }

  getDetailPickingFromMain(pagination: PaginationParam, param: PickingScanItemParam) {
    let params = new HttpParams().appendAll({ ...pagination, ...param })
    return this.http.get<PaginationResult<WarehouseOutScanPickingMainDto>>(`${this.baseUrl}GetDataPickingMain`, { params });
  }

  storageOut(data: MS_QR_PickingMain[]) {
    return this.http.put<OperationResult>(`${this.baseUrl}StorageOut`, data);
  }

  exportExcel(data: StorageOut[]) {
    return this.http.post(`${this.baseUrl}ExportExcel`, data);
  }
}
