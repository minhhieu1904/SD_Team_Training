import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { CheckSumMissDetailReportDTO, CheckSumMissReportParam } from '../../models/report/report_QRCode_WIP';
import { Pagination, PaginationResult } from '../../utilities/pagination-utility';

@Injectable({
  providedIn: 'root'
})
export class ReportQrcodeWipService {
  apiUrl: string = `${environment.apiUrl}C_QRCodeWIPReport`;
  constructor(
    private _http: HttpClient,
  ) { }


  getData(pagination: Pagination, param: CheckSumMissReportParam) {
    let params = new HttpParams().appendAll({ ...pagination, ...param });
    return this._http.get<PaginationResult<CheckSumMissDetailReportDTO>>(this.apiUrl + '/GetData', { params });
  }

  exportExcel(param: CheckSumMissReportParam) {
    let params = new HttpParams().appendAll({ ...param });
    return this._http.get(this.apiUrl + '/ExportExcel', { params, responseType: 'blob' });
  }
}
