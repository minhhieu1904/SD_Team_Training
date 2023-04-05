import { Brand, MS_QR_Order, WkshSumReport } from '@models/report/wksh-Sum';
import { Pagination, PaginationResult } from '@utilities/pagination-utility';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '@env/environment';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ReportWkshSumService {
apiUrl: string = `${environment.apiUrl}C_Report_wksh_Sum`;

constructor(private http: HttpClient) { }
getData(pagination: Pagination, param: WkshSumReport) {
  let params = new HttpParams().appendAll({ ...pagination, ...param });
  return this.http.get<PaginationResult<MS_QR_Order>>(this.apiUrl + "/getData", { params: params });
}
getBrand() {
  return this.http.get<Brand[]>(this.apiUrl +"/GetBrand");
}
exportExcel(param: MS_QR_Order) {
  let params = new HttpParams().appendAll({ ...param });
  return this.http.get(` ${this.apiUrl}/Export`, {
    params,
    responseType: 'blob',
  });
}
}
