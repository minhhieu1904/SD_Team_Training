import { ReportQRCODEWIPParam,QRCODEWIPDetailReportDTO } from '@models/report/QRCODE_WIP';

import { Pagination, PaginationResult } from '@utilities/pagination-utility';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '@env/environment';
import { Injectable } from '@angular/core';


@Injectable({
  providedIn: 'root',
})
export class ReportQRCODEWIPService {
  apiUrl: string = `${environment.apiUrl}C_Report_QRCODE_WIP`;

  constructor(private http: HttpClient) {}
  getData(pagination: Pagination, param: ReportQRCODEWIPParam) {
    let params = new HttpParams().appendAll({ ...pagination, ...param });
    return this.http.get<PaginationResult<QRCODEWIPDetailReportDTO>>(
      `${this.apiUrl}/GetData`,
      { params }
    );
  }

  exportExcel(param: ReportQRCODEWIPParam) {
    let params = new HttpParams().appendAll({ ...param });
    return this.http.get(`${this.apiUrl}/Export`, {
      params,
      responseType: 'blob',
    });
  }
}
