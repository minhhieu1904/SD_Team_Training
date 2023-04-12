import { ReportSortSumResult, SearchSortSumReportParams } from '@models/Report/reportSortSumResult';
import { Pagination, PaginationResult } from '@utilities/pagination-utility';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '@env/environment';
import { Injectable } from '@angular/core';
import { ExportDetailExcelParams } from '@models/Report/exportDetailExcelParams';

@Injectable({
  providedIn: 'root'
})
export class SortSumReportService {
  apiUrl: string = environment.apiUrl
  constructor(
    private http: HttpClient,
  ) { }

  search(pagination: Pagination, param: SearchSortSumReportParams) {
    let params = new HttpParams().appendAll({...pagination, ...param})
    return this.http.get<PaginationResult<ReportSortSumResult>>(this.apiUrl + "C_SortSumReport/Search", {params});
  }

  exportExcel(pagination: Pagination, param: SearchSortSumReportParams){
    let params = new HttpParams().appendAll({...pagination, ...param})
    return this.http.get(this.apiUrl + 'C_SortSumReport/ExportExcel', {responseType: 'blob', params});
  }

  exportDetailExcel(param: ExportDetailExcelParams){
    let params = new HttpParams().appendAll({...param})
    return this.http.get(this.apiUrl + 'C_SortSumReport/ExportDetailExcel', {responseType: 'blob', params});
  }
}
