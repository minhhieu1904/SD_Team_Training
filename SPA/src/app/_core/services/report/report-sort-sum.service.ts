import { Injectable } from '@angular/core';
import { environment } from '@env/environment'
import { HttpClient, HttpParams } from '@angular/common/http';
import { Pagination, PaginationResult } from '@utilities/pagination-utility'
import { Brand, MSQRSortDto, ReportSortSum, ReportSortSumExcelDetail, SortSumDetailReportParam} from '../../models/report/report_Sort_Sum';


@Injectable({
  providedIn: 'root'
})
export class ReportSortSumService {
  apiUrl: string = `${environment.apiUrl}C_ReportSortSum`;
  constructor(private http: HttpClient) { }


  getData(pagination: Pagination, param: ReportSortSum) {
    let params = new HttpParams().appendAll({ ...pagination, ...param });
    return this.http.get<PaginationResult<MSQRSortDto>>(this.apiUrl + '/GetData', { params: params });
  }

  getExcel(param: ReportSortSum) {
    let params = new HttpParams().appendAll({ ...param })
    return this.http.get(this.apiUrl + '/ExportExcel', { params, responseType: 'blob' });
  }

  getExcelDetail(param: SortSumDetailReportParam) {
    let params = new HttpParams().appendAll({ ...param })
    return this.http.get(this.apiUrl + '/ExportExcelDetail', { params, responseType: 'blob' });
  }

  getBrand() {
    return this.http.get<Brand[]>(this.apiUrl + '/GetBrand');
  }


}
