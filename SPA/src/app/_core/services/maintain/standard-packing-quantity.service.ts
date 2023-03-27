import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@env/environment';
import { Pagination, PaginationResult, PaginationParam } from '@utilities/pagination-utility';
import { Observable } from 'rxjs';
import { MsPackage } from '../../models/msPackage';
import { OperationResult } from '../../utilities/operation-result';



@Injectable({
  providedIn: 'root'
})
export class StandardPackingQuantityService {
  baseUrl: string = environment.apiUrl + 'C_StandardPackingQuantity';

  constructor(private http: HttpClient) {}

  getData(
    pagination: Pagination,
    param: MsPackage,
  ): Observable<PaginationResult<MsPackage>>{
    let params = new HttpParams().appendAll({...pagination, ...param});
    return this.http.get<PaginationResult<MsPackage>>(
      `${this.baseUrl}/GetData`, {params}
    );
  }
  getDataOnly(manuf:string, packageNo:string): Observable<MsPackage>{
    let params = new HttpParams()
    .set('manuf', manuf).set('packageNo', packageNo);
    return this.http.get<MsPackage>(`${this.baseUrl}/GetDataOnly`,{params})
  }
  add(model: MsPackage): Observable<OperationResult>{
    return this.http.post<OperationResult>(`${this.baseUrl}/Add`, model)
  }
  update(model: MsPackage): Observable<OperationResult>{
    return this.http.put<OperationResult>(`${this.baseUrl}/Update`, model)
  }

}
