import { Report_Wksh_SumResult, WkshSumReportParam } from './../../models/report/wksh-sum-report';
import { Pagination, PaginationResult } from './../../utilities/pagination-utility';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from './../../../../environments/environment';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class WkshSumReportService {
  private apiUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  searchWithPagination(pagination: Pagination, param: WkshSumReportParam){
    let params = new HttpParams().appendAll({...pagination, ...param});
    return this.http.get<PaginationResult<Report_Wksh_SumResult>>(this.apiUrl + "WkshSumReport/SearchWithPagination", {params});
  }

  exportExcel(pagination: Pagination, param: WkshSumReportParam){
    let params = new HttpParams().appendAll({...pagination, ...param});
    return this.http.get(this.apiUrl + "WkshSumReport/ExportExcel", {responseType: 'blob' ,params});
  }
}
