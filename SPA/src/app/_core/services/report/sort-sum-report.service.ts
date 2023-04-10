import { Injectable } from '@angular/core';
import { environment } from '@env/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Pagination, PaginationResult } from '@utilities/pagination-utility';
import {
  SortSumDetailReportParam,
  SortSumReportParam,
} from '@models/report/sortSumReportParam';
import { SortSumReportDTO } from '@models/report/sortSumReportDTO';
import { BrandDTO } from '@models/report/brandDTO';
@Injectable({
  providedIn: 'root',
})
export class SortSumReportService {
  apiUrl = environment.apiUrl + 'C_SortSumReport';

  constructor(private http: HttpClient) {}

  getData(pagination: Pagination, param: SortSumReportParam) {
    let params = new HttpParams().appendAll({ ...pagination, ...param });
    return this.http.get<PaginationResult<SortSumReportDTO>>(
      `${this.apiUrl}/GetData`,
      { params }
    );
  }

  getBrand() {
    return this.http.get<BrandDTO[]>(`${this.apiUrl}/GetBrand`);
  }

  exportExcel(param: SortSumReportParam) {
    let params = new HttpParams().appendAll({ ...param });
    return this.http.get(`${this.apiUrl}/ExportExcel`, {
      params,
      responseType: 'blob',
    });
  }

  exportDetailExcel(param: SortSumDetailReportParam) {
    let params = new HttpParams().appendAll({ ...param });
    return this.http.get(`${this.apiUrl}/ExportDetailExcel`, {
      params,
      responseType: 'blob',
    });
  }
}
