import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {environment } from '../../../../environments/environment'
import { PaginationParam, PaginationResult } from '@utilities/pagination-utility';
import { BrandDTO, MsQrOrder, SortSumReportParam, SortSumDetailReportParam } from '@models/msQrOrder';

@Injectable({
  providedIn: 'root'
})
export class SortSumReportService {
baseUrl: string = environment.apiUrl + 'C_SortSumReport'
constructor(private http: HttpClient) { }

getData(pagination: PaginationParam, param: SortSumReportParam){
  let params = new HttpParams().appendAll({...pagination,...param});
  return this.http.get<PaginationResult<MsQrOrder>>(this.baseUrl + '/GetData',{params})
}
getBrand() {
  return this.http.get<BrandDTO[]>(this.baseUrl+'/GetBrand');
}
exportExcel(param: SortSumReportParam){
  let params = new HttpParams().appendAll({...param});
  return this.http.get(this.baseUrl + '/ExportExcel', {params, responseType: 'blob'})
}
exportDetailExcel(param: SortSumDetailReportParam){
  let params = new HttpParams().appendAll({...param});
  return this.http.get(this.baseUrl + '/ExportDetailExcel', {params, responseType: 'blob'})
}
}
