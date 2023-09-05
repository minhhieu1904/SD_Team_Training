import { OperationResult } from '@utilities/operation-result';
import { Pagination, PaginationResult } from '@utilities/pagination-utility';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject } from 'rxjs';
import { WarehouseParam, WarehouseType } from '@models/maintain/warehouse-basic-data-maintenance';
import { environment } from '@env/environment';

import { Injectable } from '@angular/core';
import { MS_Location } from '@models/common/mS_Location_DTO';

@Injectable({
  providedIn: 'root'
})
export class WarehouseBasicDataMaintenanceService {
  //Lưu trữ Url của API
  apiUrl = `${environment.apiUrl}C_1_2_WarehouseBasicDataMaintenance/`;
  layoutSource = new BehaviorSubject<WarehouseType>(null);
  currentLayout = this.layoutSource.asObservable();
  constructor(private http: HttpClient) { }

  //Search
  getData(param: WarehouseParam, pagination: Pagination) {
    return this.http.get<PaginationResult<MS_Location>>(`${this.apiUrl}GetData`, { params: { ...param, ...pagination } });
  }

  //Add New
  addNew(model: MS_Location) {
    return this.http.post<OperationResult>(`${this.apiUrl}AddNew`, model);
  }

  //Edit
  edit(model: MS_Location) {
    return this.http.put<OperationResult>(`${this.apiUrl}Edit`, model);
  }
}
