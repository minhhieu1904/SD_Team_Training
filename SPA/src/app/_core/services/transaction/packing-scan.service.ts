import { Pagination, PaginationResult } from '@utilities/pagination-utility';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@env/environment';
import { PackingScanDto, PackingScanExportDto, PackingScanViewDto } from '@models/transaction/packing-scan';
import { OperationResult } from '@utilities/operation-result';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PackingScanService {
  baseUrl: string = `${environment.apiUrl}C_PackingScan/`
  constructor(private http: HttpClient) { }

  checkScanItem(scanText: string): Observable<OperationResult> {
    let params = new HttpParams().set('scanText', scanText);
    return this.http.get<OperationResult>(`${this.baseUrl}CheckScanItem`, { params });
  }

  saveScanList(data: PackingScanDto): Observable<OperationResult> {
    return this.http.post<OperationResult>(`${this.baseUrl}SaveScanList`, data);
  }

  getList(pagination: Pagination, transactionNo: string): Observable<PaginationResult<PackingScanViewDto>> {
    let params = new HttpParams()
      .set('TransactionNo', transactionNo)
      .set('PageNumber', pagination.pageNumber.toString())
      .set('PageSize', pagination.pageSize.toString())

    return this.http.get<PaginationResult<PackingScanViewDto>>(`${this.baseUrl}GetList`, { params });
  }

  getDataExport(transactionNo: string): Observable<PackingScanExportDto[]> {
    let params = new HttpParams()
      .set('TransactionNo', transactionNo)
    return this.http.get<PackingScanExportDto[]>(`${this.baseUrl}GetDataExport`, { params });
  }

}
