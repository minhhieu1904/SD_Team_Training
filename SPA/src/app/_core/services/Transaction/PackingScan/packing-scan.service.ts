import { PackingScanExportDTO } from '@models/Transaction/PackingScan/PackingScanExportDTO';
import { PackingScanViewDTO } from '@models/Transaction/PackingScan/PackingScanViewDTO';
import { PackingScanDTOParam } from '@models/Transaction/PackingScan/PackingScanDTOParam';
import { OperationResult } from '@utilities/operation-result';
import { Pagination, PaginationResult } from '@utilities/pagination-utility';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '@env/environment';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PackingScanService {

  apiUrl: string = `${environment.apiUrl}C_PackingScan`;

  constructor(private http: HttpClient) { }

    checkScanItem(scanText: string): Observable<OperationResult> {
      let params = new HttpParams().set('scanText', scanText)
      return this.http.get<OperationResult>(`${this.apiUrl}/checkScanItem`, {params});
    }

    saveScanList(data: PackingScanDTOParam): Observable<OperationResult> {
      return this.http.post<OperationResult>(`${this.apiUrl}/savePackingScanList`, data);
    }

    getList(pagination: Pagination, transactionNo: string): Observable<PaginationResult<PackingScanViewDTO>>{
      let params = new HttpParams()
        .set('TransactionNo',transactionNo)
        .set('PageNumber', pagination.pageNumber.toString())
        .set('pageSize', pagination.pageSize.toString());
      return this.http.get<PaginationResult<PackingScanViewDTO>>(`${this.apiUrl}/getList`, {params});
    }

    getDataExport(transactionNo: string): Observable<PackingScanExportDTO[]>{
      let params = new HttpParams()
        .set('TransactionNo', transactionNo)
      return this.http.get<PackingScanExportDTO[]>(`${this.apiUrl}/GetDataExport`, { params });
    }

}
