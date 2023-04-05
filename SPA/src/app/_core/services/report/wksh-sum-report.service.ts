import { Injectable } from '@angular/core';
import {environment } from '../../../../environments/environment'
import { HttpClient, HttpParams } from '@angular/common/http';
import { Pagination, PaginationResult } from '../../utilities/pagination-utility';
import { WkshSumReport, MsQrOrder } from "../../models/msQrOrder";
import { ExportData } from "../../models/exportData";
import { __param } from 'tslib';

@Injectable({
  providedIn: 'root'
})
export class WkshSumReportService {
baseUrl: string = environment.apiUrl + 'C_WkshSumReport'
constructor(private http: HttpClient) { }
getData(pagination: Pagination, param: WkshSumReport){
  let params = new HttpParams().appendAll({...pagination,...param});
  return this.http.get<PaginationResult<MsQrOrder>>(this.baseUrl + "/getData", {params})
}

// exportExcel(exportData : ExportData){
//   let params = new HttpParams().appendAll({...exportData});
//   return this.http.get(`${this.baseUrl}/exportExcel`, {params})
// }

  exportExcel(pagination: Pagination, param: WkshSumReport) {
    let params = new HttpParams().appendAll({ ...pagination, ...param });
    return this.http.get(this.baseUrl + "/ExportExcel", { params , responseType:'blob' });
  }

}
