import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Pagination, PaginationResult } from '../../utilities/pagination-utility';
import { WkshSumReport, MS_QR_Order, Brand } from "../../models/report/MS_QR_Order";
import { OperationResult } from '@utilities/operation-result';
@Injectable({
  providedIn: 'root'
})
export class WkshSumReportService {
  apiUrl: string = environment.apiUrl

  constructor(private http: HttpClient) { }
  getData(pagination: Pagination, param: WkshSumReport) {
    let params = new HttpParams().appendAll({ ...pagination, ...param });
    return this.http.get<PaginationResult<MS_QR_Order>>(this.apiUrl + "C_WkshSumReport/getData", { params: params });
  }
  getBrand() {
    return this.http.get<Brand[]>(this.apiUrl + "C_WkshSumReport/GetBrand");
  }
  exportExcel(pagination: Pagination, param: WkshSumReport) {
    // Cái này giống như hình ảnh,excel chuyển qua cái này thì mới có thể di chuyển
    let params = new HttpParams().appendAll({ ...pagination, ...param });
    return this.http.get(this.apiUrl + "C_WkshSumReport/ExportExcel", { params : params , responseType:'blob' });
  }
}
