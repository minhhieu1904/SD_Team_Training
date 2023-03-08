import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { MS_Shift, ShiftDataMaintainParam } from '../models/shift-data-maintain';
import { Pagination, PaginationResult } from '@utilities/pagination-utility';
import { OperationResult } from '@utilities/operation-result';

@Injectable({
  providedIn: 'root'
})
export class ShiftDataMaintainService {
  apiUrl: string = environment.apiUrl;
  constructor(private http: HttpClient) { }

  getData(pagination: Pagination, param: ShiftDataMaintainParam) { 
    let params = new HttpParams().appendAll({ ...pagination, ...param});
    return this.http.get<PaginationResult<MS_Shift>>(this.apiUrl + "C_ShiftDataMaintain/get-data", { params: params});
  }

  addNew(msshift : MS_Shift) { 
    return this.http.post<OperationResult>(this.apiUrl + "C_ShiftDataMaintain/add", msshift);
  }

  getItem(manuf:string, shift:string){
    //khai bao bien
    let params = new HttpParams().set('manuf', manuf).set('shift',shift);
    return this.http.get<MS_Shift>(this.apiUrl + "C_ShiftDataMaintain/getDataOnly" , { params });
  }
  updateShift(msshift: MS_Shift){
    return this.http.post<OperationResult>(this.apiUrl + "C_ShiftDataMaintain/update", msshift);
  }



}
