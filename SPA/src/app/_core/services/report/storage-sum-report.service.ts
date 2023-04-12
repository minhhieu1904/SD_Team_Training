import { ExportDetailExcelParams } from '@models/Report/exportDetailExcelParams';
import { Pagination, PaginationResult } from '@utilities/pagination-utility';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '@env/environment';
import { Injectable } from '@angular/core';
import { Report_Storage_Sum, Report_Storage_Sum_Param } from '@models/Report/report_Storage_Sum_Param';

@Injectable({
  providedIn: 'root'
})
export class StorageSumReportService {
  apiUrl: string = environment.apiUrl;
  constructor(
    private http: HttpClient,
  ) { }

  search(pagination: Pagination, param: Report_Storage_Sum_Param) {
    let params = new HttpParams().appendAll({...pagination, ...param})
    return this.http.get<PaginationResult<Report_Storage_Sum>>(this.apiUrl + "C_StorageSumReport/Search", {params});
  }

  exportExcel(pagination: Pagination, param: Report_Storage_Sum_Param){
    let params = new HttpParams().appendAll({...pagination, ...param})
    return this.http.get(this.apiUrl + 'C_StorageSumReport/ExportExcel', {responseType: 'blob', params});
  }

  exportDetailExcel(param: ExportDetailExcelParams){
    let params = new HttpParams().appendAll({...param})
    return this.http.get(this.apiUrl + 'C_StorageSumReport/ExportDetailExcel', {responseType: 'blob', params});
  }
}
