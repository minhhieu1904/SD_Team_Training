
import { OperationResult } from './../utilities/operation-result';
import { environment } from './../../../environments/environment';
import { HttpParams, HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { MS_Shift, MS_ShiftParam } from '../_models/shift_data/shift_Data';
import { BehaviorSubject, Observable } from 'rxjs';
import { Pagination, PaginationResult } from '../utilities/pagination-utility';
@Injectable({
  providedIn: 'root'
})
export class Shift_dataService  {

baseUrl = environment.apiUrl;
dataSource = new BehaviorSubject<MS_ShiftParam>(null)
currentDataSource = this.dataSource.asObservable();

constructor(private http: HttpClient) { }

// search param truoc roi mới pagination
getData(searchParam: MS_ShiftParam, pagination : Pagination ){

  // let params = new HttpParams()
  // .set("shift", searchParam.shift)
  // .set("shiftName", searchParam.shiftName)

  // cach 2 ... la` lay tat ca ben trong models
  let params = new HttpParams().appendAll({...searchParam, ...pagination})
  return this.http.get<PaginationResult<MS_Shift>>(this.baseUrl + 'C_ShiftDataMaintenance/getdata', {params})
}
add(params: MS_ShiftParam){
  return this.http.post<OperationResult>(this.baseUrl + 'C_ShiftDataMaintenance/add', params)
}

upDate(params: MS_ShiftParam){
  return this.http.put<OperationResult>(this.baseUrl + 'C_ShiftDataMaintenance/update', params)
}
getItem(manuf: string, shift : string ){
  // cach 2 ... la` lay tat ca ben trong models
  let params = new HttpParams().set('manuf', manuf).set('shift',shift);
  return this.http.get<MS_Shift>(this.baseUrl + 'C_ShiftDataMaintenance/getdata', {params})
}
}
