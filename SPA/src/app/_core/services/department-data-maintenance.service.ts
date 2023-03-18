import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@env/environment';
import { Pagination, PaginationResult } from '@utilities/pagination-utility';
import { MS_Department, DepartmentDataMaintenanceParam } from '../models/department-data-maintenance';
import { OperationResult } from '@utilities/operation-result';
@Injectable({
  providedIn: 'root'
})
export class DepartmentDataMaintenanceService {
  apiUrl: string = environment.apiUrl;
  constructor(private http: HttpClient) { }
  getData(pagination: Pagination, param: DepartmentDataMaintenanceParam) { 
    let params = new HttpParams().appendAll({ ...pagination, ...param});
    return this.http.get<PaginationResult<MS_Department>>(this.apiUrl + "C_DepartmentDataMaintenance/getData", { params: params});
  }
  addNew(msdepartment : MS_Department) { 
    return this.http.post<OperationResult>(this.apiUrl + "C_DepartmentDataMaintenance/add", msdepartment);
  }

  getItem(manuf:string, parNo:string){
    //khai bao bien
    let params = new HttpParams().set('manuf', manuf).set('parNo',parNo);
    return this.http.get<MS_Department>(this.apiUrl + "C_DepartmentDataMaintenance/getDataOnly" , { params });
  }
  updateShift(msdepartment: MS_Department){
    return this.http.post<OperationResult>(this.apiUrl + "C_DepartmentDataMaintenance/update", msdepartment);
  }
}
