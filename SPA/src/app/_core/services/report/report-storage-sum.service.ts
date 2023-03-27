import { EnvironmentInjector, Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Brand, MSQRStorageDto, ReportStorageSum, ReportStorageSumDetail, ReportStorageSumDetailParam } from '../../models/report/report_Storage_Sum';
import { Pagination, PaginationResult } from '../../utilities/pagination-utility'
@Injectable({
  providedIn: 'root'
})
export class ReportStorageSumService {
  apiUrl: string = `${environment.apiUrl}C_ReportStorageSum`;
  constructor(private http: HttpClient) { }

  getBrand() {
    return this.http.get<Brand[]>(this.apiUrl + '/GetBrand');
  }

  getData(pagination: Pagination, param: ReportStorageSum) {    
    let params = new HttpParams().appendAll({ ...pagination, ...param });
    return this.http.get<PaginationResult<MSQRStorageDto>>(this.apiUrl + '/GetDataPaing', { params: params });
  }

  getExcelDetail(param: ReportStorageSumDetailParam) {
    let params = new HttpParams().appendAll({ ...param });
    return this.http.get(this.apiUrl + '/ExportExcelDetail', { params, responseType: 'blob' });
  }

  getExcel(param: ReportStorageSum) {
    let params = new HttpParams().appendAll({ ...param });
    return this.http.get(this.apiUrl + '/ExportExcel', { params, responseType: 'blob' });
  }
}
