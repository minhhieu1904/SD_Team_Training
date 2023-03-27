import { MS_QR_Storage } from './../../models/transaction/warehouse-scan';
import { Pagination, PaginationResult } from './../../utilities/pagination-utility';
import { OperationResult } from './../../utilities/operation-result';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { environment } from './../../../../environments/environment';
import { Injectable } from '@angular/core';
import { WarehouseScanDto } from '../../models/transaction/warehouse-scan';

@Injectable({
  providedIn: 'root'
})
export class WarehouseScanService {
  apiUrl = environment.apiUrl;
  paramForm = new BehaviorSubject<string>({} as string);
  currentParamForm = this.paramForm.asObservable();
  constructor(
    private http: HttpClient
  ) { }

  setParamForm = (item: string) =>
    this.paramForm.next(item)

  checkScanCode(target: string): Observable<OperationResult> {
    return this.http.post<OperationResult>(`${this.apiUrl}C_WareHouseScan/CheckScanCode`, null, { params: { target } });
  }

  saveQRStorage(data: WarehouseScanDto): Observable<OperationResult> {
    return this.http.post<OperationResult>(`${this.apiUrl}C_WareHouseScan/SaveQRStorage`, data);
  }

  getListWarehouseScan(pagination: Pagination, trNo: string): Observable<PaginationResult<MS_QR_Storage>> {
    let params = new HttpParams().appendAll({ ...pagination, trNo });
    return this.http.get<PaginationResult<MS_QR_Storage>>(`${this.apiUrl}C_WareHouseScan/GetListQRStorage`, { params });
  }

  exportReport(trNo: string) {
    return this.http.post(`${this.apiUrl}C_WareHouseScan/ExportExcel`, {}, { params: { trNo }, responseType: 'blob' })
  }
}
