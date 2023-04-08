import { environment } from '@env/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BehaviorSubject } from 'rxjs';
import { Injectable } from '@angular/core';
import { MS_Department, MS_DepartmentParam } from '@models/maintain/department';
import { Pagination, PaginationResult } from '@utilities/pagination-utility';
import { OperationResult } from '@utilities/operation-result';

@Injectable({
  providedIn: 'root'
})
export class DepartmentdataService {
BaseUrl = environment.apiUrl;
dataSources = new BehaviorSubject<MS_DepartmentParam>(null);
currentDataSource = this.dataSources.asObservable();
constructor(private http: HttpClient) { }
getData(searchParam : MS_DepartmentParam, pagination: Pagination,){
  let params = new HttpParams().appendAll({...searchParam, ...pagination});
  return this.http.get<PaginationResult<MS_Department>>(this.BaseUrl + 'C_Department/getData',{ params});
}
getItem(manuf: string, parNo: string) {
  // cach 2 ... la` lay tat ca ben trong models
   let params = new HttpParams().set('manuf', manuf).set('parNo', parNo);
  return this.http.get<MS_Department>(this.BaseUrl + 'C_Department/getData', {params} );
}

add( parmas : MS_DepartmentParam)
{
  return this.http.post<OperationResult>(this.BaseUrl+ 'C_Department/add', parmas);
}
upDate( parmas : MS_DepartmentParam){
  return this.http.put<OperationResult>(this.BaseUrl+ 'C_Department/upDate', parmas);
}

}
