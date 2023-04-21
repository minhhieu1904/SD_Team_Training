import { Injectable } from '@angular/core';
import { environment } from '@env/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Pagination, PaginationResult } from '@utilities/pagination-utility';
import {
StorageSumDetailReportParam,
  StorageSumReportParam,
} from '@models/report/storageSumReportParam';
import { StorageSumReportDTO } from "@models/report/storageSumReportDTO";


@Injectable({
  providedIn: 'root'
})
export class StorageSumReportService {

    apiUrl = environment.apiUrl + 'C_StorageSumReport';

    constructor(private http: HttpClient) {}
  
    getData(pagination: Pagination, param: StorageSumReportParam) {
      let params = new HttpParams().appendAll({ ...pagination, ...param });
      return this.http.get<PaginationResult<StorageSumReportDTO>>(
        `${this.apiUrl}/GetData`,
        { params }
      );
    }
  
    exportExcel(param: StorageSumReportParam) {
      let params = new HttpParams().appendAll({ ...param });
      return this.http.get(`${this.apiUrl}/ExportExcel`, {
        params,
        responseType: 'blob',
      });
    }
  
    exportDetailExcel(param: StorageSumDetailReportParam) {
      let params = new HttpParams().appendAll({ ...param });
      return this.http.get(`${this.apiUrl}/ExportDetailExcel`, {
        params,
        responseType: 'blob',
      });
    }
}
