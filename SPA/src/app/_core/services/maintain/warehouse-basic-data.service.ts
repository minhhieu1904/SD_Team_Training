import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@env/environment';
import { OperationResult } from '@utilities/operation-result';
import { Pagination, PaginationParam, PaginationResult } from '@utilities/pagination-utility';
import { Observable } from 'rxjs';
import { MsLocation, WareHouseBasicDataParam } from '../../models/maintain/msLocation'

@Injectable({
  providedIn: 'root'
})
export class WareHouseBasicDataService {
  baseUrl: string = environment.apiUrl + 'C_WareHouseBasicData';
  constructor(private http: HttpClient) {}

  getData(
    pagination: Pagination,
    param: WareHouseBasicDataParam
  ): Observable<PaginationResult<MsLocation>>{
    let params = new HttpParams().appendAll({...pagination, ...param});
    return this.http.get<PaginationResult<MsLocation>>
    (`${this.baseUrl}/GetData`, { params });
  }

  getDataOnly(
    manuf: string,
    storeH: string
  ): Observable<MsLocation>{
    let params = new HttpParams().set('manuf', manuf).set('storeH', storeH);
    return this.http.get<MsLocation>(`${this.baseUrl}/GetDataOnly`,{params});
  }

  add(model: MsLocation): Observable<OperationResult>{
    return this.http.post<OperationResult>
    (`${this.baseUrl}/Add`, model);
  }
  update(model: MsLocation): Observable<OperationResult>{
    return this.http.put<OperationResult>
    (`${this.baseUrl}/Update`, model);
  }
  delete(storeH: string): Observable<OperationResult>{
    return this.http.delete<OperationResult>
    (`${this.baseUrl}/Delete`, {params: {storeH}});
  }
}

