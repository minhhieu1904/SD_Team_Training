import { OperationResult } from './../../utilities/operation-result';
import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class SortNewreportByMidasProvideService {
  baseUrl: string = `${environment.apiUrl}C_SortNewReportByMidasProvide/`
  constructor(
    private http: HttpClient
  ) { }

  exportExcel(sortDate: string) {
    return this.http.get<OperationResult>(`${this.baseUrl}ExportExcel`, { params: { sortDate } });
  }
}
