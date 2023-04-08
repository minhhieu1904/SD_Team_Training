import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Pagination, PaginationResult } from '../../utilities/pagination-utility';
import { Brand, Report_Sort_SumResult, SortSumReport } from "../../models/report/MS_QR_Order";
import { SortSumDetailReportParam } from "../../models/report/sortSumReportDetail";
@Injectable({
  providedIn: 'root'
})
export class SortSumReportService {
  apiUrl: string = environment.apiUrl
  constructor(private http: HttpClient) { }
  getData(pagination: Pagination, param: SortSumReport) {
    let params = new HttpParams().appendAll({ ...pagination, ...param });
    return this.http.get<PaginationResult<Report_Sort_SumResult>>(this.apiUrl + "C_SortSumReport/getData", { params: params });
  }
  getBrand() {
    return this.http.get<Brand[]>(this.apiUrl + "C_SortSumReport/GetBrand");
  }
  exportExcel(pagination: Pagination, param: SortSumReport) {
    let params = new HttpParams().appendAll({ ...pagination, ...param });
    return this.http.get(this.apiUrl + "C_SortSumReport/ExportExcel", { params : params , responseType:'blob' });
  }
  ExportDetailExcel(param: SortSumDetailReportParam) {
    let params = new HttpParams().appendAll({...param });
    return this.http.get(this.apiUrl + "C_SortSumReport/ExportDetailExcel", { params : params , responseType:'blob' });
  }
  
}
