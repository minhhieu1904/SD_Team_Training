import { QRcodeWIPReportParam } from './../../models/Report/qRCode_WIP_Report/qRcodeWIPReportParam';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from './../../../../environments/environment';
import { Injectable } from '@angular/core';
import { Pagination, PaginationResult } from '../../utilities/pagination-utility';
import { Report_Check_Sum_Miss } from '../../models/Report/qRCode_WIP_Report/reportQRcodeWIPDto';

@Injectable({
  providedIn: 'root'
})
export class QrcodeWipReportService {
  apiUrl: string = environment.apiUrl;
  constructor(
    private http: HttpClient
  ) { }

  search(pagination: Pagination, param: QRcodeWIPReportParam) {
    let params = new HttpParams().appendAll({...pagination, ...param})
    return this.http.get<Report_Check_Sum_Miss>(this.apiUrl + "C_QRCodeWIPReport/Search", {params})
  }

  exportExcel(pagination: Pagination, param: QRcodeWIPReportParam){
    let params = new HttpParams().appendAll({...pagination, ...param})
    return this.http.get(this.apiUrl + 'C_QRCodeWIPReport/ExportExcel', {responseType: 'blob', params})
  }
}
