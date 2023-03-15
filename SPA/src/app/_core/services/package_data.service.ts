import { OperationResult } from './../utilities/operation-result';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Pagination, PaginationResult } from './../utilities/pagination-utility';
import { MS_Package, MS_PackageParam } from './../_models/Package-data/Package-data';
import { environment } from './../../../environments/environment';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class Package_dataService {
  baseUrl = environment.apiUrl;
  dataSources = new BehaviorSubject<MS_PackageParam>(null);
  currentDataSource = this.dataSources.asObservable();
  constructor(private http: HttpClient) { }

  getData(searchParam: MS_PackageParam, pagination: Pagination) {
    let params = new HttpParams().appendAll({ ...searchParam, ...pagination });
    return this.http.get<PaginationResult<MS_Package>>(this.baseUrl + 'C_PackageData/getData', { params });

  }
  add(params: MS_PackageParam) {
    return this.http.post<OperationResult>(this.baseUrl + 'C_PackageData/add', params);

  }
  upDate(params: MS_PackageParam) {
    return this.http.put<OperationResult>(this.baseUrl + 'C_PackageData/upDate', params);
  }
  getItem(manuf: string, location: string) {
    // cach 2 ... la` lay tat ca ben trong models
     let params = new HttpParams().set('manuf', manuf).set('location', location);
    return this.http.get<MS_Package>(this.baseUrl + 'C_PackageData/getData', {params} );
  }


}
