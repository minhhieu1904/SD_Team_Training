import { Injectable } from '@angular/core';
import { environment } from '@env/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import {
  PaginationParam,
  PaginationResult,
} from '@utilities/pagination-utility';
import { OperationResult } from '@utilities/operation-result';

import { StandardPackingQuantityParam } from '@models/maintain/standardPackingQuantityParam';
import { MsPackage } from '@models/maintain/msPackage';

@Injectable({
  providedIn: 'root',
})
export class StandardPackingQuantityService {
  baseUrl: string = environment.apiUrl + 'C_StandardPackingQuantity';

  constructor(private http: HttpClient) {}

  getData(pagination: PaginationParam, param: StandardPackingQuantityParam) {
    let params = new HttpParams().appendAll({ ...pagination, ...param });

    return this.http.get<PaginationResult<MsPackage>>(
      `${this.baseUrl}/GetData`,
      { params }
    );
  }

  getDataOnly(manuf: string, packageNo: string) {
    let params = new HttpParams()
      .set('manuf', manuf)
      .set('packageNo', packageNo);

    return this.http.get<MsPackage>(`${this.baseUrl}/GetDataOnly`, { params });
  }

  add(model: StandardPackingQuantityParam) {
    return this.http.post<OperationResult>(`${this.baseUrl}/Add`, model);
  }

  update(model: StandardPackingQuantityParam) {
    return this.http.put<OperationResult>(`${this.baseUrl}/Update`, model);
  }

  delete(packageNo: string) {
    return this.http.post<OperationResult>(`${this.baseUrl}/Delete`, {
      params: { packageNo },
    });
  }
}
