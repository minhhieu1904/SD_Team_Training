import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from './../../../environments/environment';
import { Pagination, PaginationResult } from './../utilities/pagination-utility';
import { PackingScanDTO, PackingScanExportDTO, ViewDataPackingScan } from '../models/transaction/packing-scan';
import { OperationResult } from '@utilities/operation-result';
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class PackingScanService {

  constructor(private http: HttpClient) { }
  // apiUrl = environment.apiUrl;

  // getData(pagination: Pagination, transactionNo: string) {
  //   let params = new HttpParams()
  //     .set('TransactionNo', transactionNo)
  //     .set('pageNumber', pagination.pageNumber)
  //     .set('pageSize', pagination.pageSize);
  //   return this.http.get<PaginationResult<ViewDataPackingScan>>(this.apiUrl + 'PackingScan/GetData', { params })
  // }

  // checkScan(scanText: string) {
  //   let params = new HttpParams().set('scanText', scanText)
  //   return this.http.get<OperationResult>(this.apiUrl + 'PackingScan/CheckScan', { params });
  // }

  // saveScanList(model: PackingScanDTO) {
  //   return this.http.post<OperationResult>(this.apiUrl + 'PackingScan/SaveScanList', model);
  // }

  // getDataExport(transactionNo: string, pagination: Pagination) {
  //   let params = new HttpParams()
  //     .set('transactionNo', transactionNo)
  //     .set('pageNumber', pagination.pageNumber)
  //     .set('pageSize', pagination.pageSize)

  //   return this.http.get<PackingScanExportDTO[]>(this.apiUrl + 'PackingScan/GetDataExport', { params })
  // }

  baseUrl = `${environment.apiUrl}PackingScan/`;

  checkScanItem(scanText: string): Observable<OperationResult> {
    let params = new HttpParams()
      .set('scanText', scanText)
    return this.http.get<OperationResult>(`${this.baseUrl}CheckScanItem`, {params});
  }

  saveScanList(data: PackingScanDTO): Observable<OperationResult> {
    return this.http.post<OperationResult>(`${this.baseUrl}SaveScanList`, data);
  }

  getList(pagination: Pagination, transactionNo: string): Observable<PaginationResult<ViewDataPackingScan>>{
    let params = new HttpParams()
      .set('TransactionNo',transactionNo)
      .set('PageNumber', pagination.pageNumber.toString())
      .set('pageSize', pagination.pageSize.toString());

    return this.http.get<PaginationResult<ViewDataPackingScan>>(`${this.baseUrl}GetList`, {params});
  }

  getDataExport(transactionNo: string): Observable<PackingScanExportDTO[]>{
    let params = new HttpParams()
      .set('TransactionNo', transactionNo)
    return this.http.get<PackingScanExportDTO[]>(`${this.baseUrl}GetDataExport`, { params });
  }
}
