import { Injectable } from '@angular/core';
import { Pagination, PaginationResult } from '@utilities/pagination-utility';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '@env/environment';
import { WkshSumReportParam } from '@models/report/wkshSumReportParam';
import { MsQrOrder } from '@models/report/msQrOrder';
@Injectable({
  providedIn: 'root',
})
export class WkshSumReportService {
  apiUrl = environment.apiUrl + 'C_WkshSumReport';
  constructor(private http: HttpClient) {}

  getData(pagination: Pagination, param: WkshSumReportParam) {
    let params = new HttpParams().appendAll({ ...pagination, ...param });
    return this.http.get<PaginationResult<MsQrOrder>>(
      `${this.apiUrl}/GetData`,
      { params }
    );
  }

  exportExcel(param: WkshSumReportParam) {
    let params = new HttpParams().appendAll({ ...param });
    return this.http.get(`${this.apiUrl}/ExportExcel`, {
      params,
      responseType: 'blob',
    });
  }
}
