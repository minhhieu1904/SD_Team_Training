import { ReprintPackingScanModel, ReprintPackingScanParam } from '@models/transaction/reprint-packing-scan';
import { Pagination, PaginationResult } from '@utilities/pagination-utility';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '@env/environment';
import { PackingScanExportDto } from '@models/transaction/packing-scan';


import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ReprintPackingScanService {
  baseUrl: string = environment.apiUrl + 'C_ReprintPackingScan/'
  constructor(
    private http: HttpClient,
  ) { }

  getData(pagination: Pagination, filterParam: ReprintPackingScanParam): Observable<PaginationResult<ReprintPackingScanModel>> {
    let params = new HttpParams().appendAll({ ...pagination, ...filterParam });
    return this.http.get<PaginationResult<ReprintPackingScanModel>>(`${this.baseUrl}GetData`, { params: params });
  }

  getDataReprint(listTrNo: ReprintPackingScanModel[]): Observable<PackingScanExportDto[]> {
    return this.http.post<PackingScanExportDto[]>(`${this.baseUrl}getDataReprint`, listTrNo);
  }
}
