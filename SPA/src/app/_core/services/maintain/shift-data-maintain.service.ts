import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import {
  PaginationParam,
  PaginationResult,
} from '../../utilities/pagination-utility';

import { MsShift, ShiftDataMaintainParam } from '../../models/maintain/msShift';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class ShiftDataMaintainService {
  baseUrl: string = environment.apiUrl + 'C_ShiftDataMaintain';
  constructor(private http: HttpClient) {}

  getData(
    pagination: PaginationParam,
    param: ShiftDataMaintainParam
  ): Observable<PaginationResult<MsShift>> {
    // let params = new HttpParams()
    // .set('pageNumber', pagination.pageNumber)
    // .set('pageSize', pagination.pageSize)
    let params = new HttpParams().appendAll({ ...pagination, ...param });

    return this.http.get<PaginationResult<MsShift>>(
      `${this.baseUrl}/get-data`,
      { params }
    );
  }

  getDataOnly(manuf: string, shift: string): Observable<MsShift> {
    let params = new HttpParams().set('manuf', manuf).set('shift', shift);

    return this.http.get<MsShift>(`${this.baseUrl}/GetDataOnly`, { params });

    // return this.http.get<MsShift>(`${this.baseUrl}/GetDataOnly`, {
    //   params: { manuf, shift },
    // });
  }

  add() {}

  update() {}

  delete() {}
}
