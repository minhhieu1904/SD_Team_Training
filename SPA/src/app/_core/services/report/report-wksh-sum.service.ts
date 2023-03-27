import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { Brand } from '../../models/report/report_wksh_Sum';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Pagination, PaginationResult } from '../../utilities/pagination-utility'
import { MSQROrder, ReportwkshSum } from '../../models/report/report_wksh_Sum';

@Injectable({
  providedIn: 'root'
})
export class ReportWkshSumService {
  apiUrl: string = `${environment.apiUrl}C_ReportWkshSum`;
  constructor(
    private _http: HttpClient
  ) { }

  getBrand() {
    return this._http.get<Brand[]>(this.apiUrl + '/GetBrand');
  }

  getData(pagination: Pagination, param: ReportwkshSum) {
    let params = new HttpParams().appendAll({ ...pagination, ...param });
    return this._http.get<PaginationResult<MSQROrder>>(this.apiUrl + '/GetData', { params });
  }

  exportExcel(param: MSQROrder) {
    let params = new HttpParams().appendAll({ ...param });
    return this._http.get(this.apiUrl + '/ExportExcel', { params, responseType: 'blob' });
  }

}
