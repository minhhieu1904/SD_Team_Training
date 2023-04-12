import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Pagination, PaginationResult } from '../../utilities/pagination-utility';
import { Brand, StorageSumDetailReportParam, StorageSumReportParam, StorageSumReportResult } from "../../models/report/Storage_Sum_Report";
import { SortSumDetailReportParam } from "../../models/report/sortSumReportDetail";

@Injectable({
  providedIn: 'root'
})
export class StorageSumReportService {
  apiUrl: string = environment.apiUrl
  constructor(private http: HttpClient) { }
  getData(pagination: Pagination, param: StorageSumReportParam) {
    let params = new HttpParams().appendAll({ ...pagination, ...param });
    return this.http.get<PaginationResult<StorageSumReportResult>>(this.apiUrl + "C_StorageSumReport/getData", { params: params });
  }
  getBrand() {
    return this.http.get<Brand[]>(this.apiUrl + "C_StorageSumReport/GetBrand");
  }
  exportExcel(pagination: Pagination, param: StorageSumReportParam) {
    let params = new HttpParams().appendAll({ ...pagination, ...param });
    return this.http.get(this.apiUrl + "C_StorageSumReport/ExportExcel", { params : params , responseType:'blob' });
  }
  ExportDetailExcel(param: StorageSumDetailReportParam) {
    let params = new HttpParams().appendAll({...param });
    return this.http.get(this.apiUrl + "C_StorageSumReport/ExportDetailExcel", { params : params , responseType:'blob' });
  }
  
}
