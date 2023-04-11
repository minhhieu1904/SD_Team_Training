import { Brand, Report_Sort_SumParam, SortSumDTO, SortSumDeltailDTOParam } from '@models/report/sort-Sum';
import { Pagination, PaginationResult } from '@utilities/pagination-utility';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@env/environment';

@Injectable({
  providedIn: 'root'
})
export class ReportSortSumService {

  apiUrl: string = `${environment.apiUrl}C_Report_Sort_Sum`;

  constructor(private http: HttpClient) { }
  getData(pagination: Pagination, param: SortSumDTO) {
    let params = new HttpParams().appendAll({ ...pagination, ...param });
    return this.http.get<PaginationResult<Report_Sort_SumParam>>(this.apiUrl + "/getData", { params: params });
  }
  getBrand() {
    return this.http.get<Brand[]>(this.apiUrl +"/GetBrand");
  }

  exportExcel(param: Report_Sort_SumParam) {
    let params = new HttpParams().appendAll({ ...param });
    return this.http.get(`${this.apiUrl}/Export`, {
      params,
      responseType: 'blob',
    });
  }

  exportExcelDetail(param: SortSumDeltailDTOParam) {
    let params = new HttpParams().appendAll({ ...param });
    return this.http.get(`${this.apiUrl}/ExportExcelDetail`, {
      params,
      responseType: 'blob',
    });
  }
}
