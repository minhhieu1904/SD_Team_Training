import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@env/environment';
import { CheckSumMissDetailDTO } from '@models/report/qRCodeDTO';
import { QRCodeParam } from '@models/report/qRCodeParam';
import { PaginationParam, PaginationResult } from '@utilities/pagination-utility';

@Injectable({
  providedIn: 'root'
})
export class QrcodeWipReportService {
baseUrl: string = environment.apiUrl + 'C_QRCodeWIPReport'
constructor(private http: HttpClient) { }
getData(pagination: PaginationParam, param: QRCodeParam){
  let params = new HttpParams().appendAll({...pagination,...param})
  return this.http.get<PaginationResult<CheckSumMissDetailDTO>>
  (this.baseUrl + '/GetData', {params})
}
exportExcel(param: QRCodeParam){
  let params = new HttpParams().appendAll({...param});
  return this.http.get(this.baseUrl + '/ExportExcel', {params, responseType: 'blob'})
}
}
