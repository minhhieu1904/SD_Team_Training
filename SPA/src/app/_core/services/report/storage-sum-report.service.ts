import { Injectable } from '@angular/core';
import {environment } from '@env/environment'
import { HttpClient, HttpParams } from '@angular/common/http';
import { PaginationParam, PaginationResult} from '@utilities/pagination-utility';
import { StorageSumReportParam } from '@models/report/storageSumReportParam';
import { MsQrOrder, StorageSumDetailReportParam } from '@models/report/msQrOrder';
import { BrandDTO } from '@models/report/brandDTO';

@Injectable({
  providedIn: 'root'
})
export class StorageSumReportService {
baseUrl: string = environment.apiUrl + 'C_StorageSumReport/'
constructor(private http: HttpClient) { }

getData(pagination: PaginationParam, param: StorageSumReportParam){
  let params = new HttpParams().appendAll({...pagination,...param});
  return this.http.get<PaginationResult<MsQrOrder>>(this.baseUrl + 'GetData',{params})
}
exportExcel(param: StorageSumReportParam){
  let params = new HttpParams().appendAll({...param});
  return this.http.get(this.baseUrl + 'ExportExcel',{params, responseType: 'blob'})
}
exportDetailExcel(param: StorageSumDetailReportParam){
  let params = new HttpParams().appendAll({...param});
  return this.http.get(this.baseUrl + 'ExportDetailExcel',{params, responseType: 'blob'})
}
getBrand(){
  return this.http.get<BrandDTO[]>(this.baseUrl+'GetBrand')
}
}
