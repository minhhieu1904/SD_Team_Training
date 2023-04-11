import { Injectable } from '@angular/core';
import { environment } from '@env/environment';
import { Pagination, PaginationResult } from '@utilities/pagination-utility';
import { CheckSumMissReportParam } from '@models/report/checkSumMissReportParam';
import { HttpClient, HttpParams } from '@angular/common/http';
import { CheckSumMissDetailReportDTO } from '@models/report/checkSumMissReportDTO';
@Injectable({
  providedIn: 'root',
})
export class QrcodeWipReportService {
  apiUrl = environment.apiUrl + 'C_QRcodeWipReport';
  constructor(private http: HttpClient) {}

  getData(pagination: Pagination, param: CheckSumMissReportParam) {
    let params = new HttpParams().appendAll({ ...pagination, ...param });
    return this.http.get<PaginationResult<CheckSumMissDetailReportDTO>>(
      `${this.apiUrl}/GetData`,
      { params }
    );
  }

  exportExcel(param: CheckSumMissReportParam) {
    let params = new HttpParams().appendAll({ ...param });
    return this.http.get(`${this.apiUrl}/ExportExcel`, {
      params,
      responseType: 'blob',
    });
  }
}
