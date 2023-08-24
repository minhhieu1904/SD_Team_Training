import { MS_Shift } from '@models/common/mS_Shift_DTO';
import { OperationResult } from '@utilities/operation-result';
import { Pagination, PaginationResult } from '@utilities/pagination-utility';
import { environment } from '@env/environment';
import { BehaviorSubject } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ShiftParam, ShiftType } from '@models/maintain/shift-data-maintenance';

@Injectable({
  providedIn: 'root'
})
export class ShiftDataMaintainanceService {
  //Lưu trữ Url của API
  apiUrl = `${environment.apiUrl}C_1_1_ShiftDataMaintenance/`;
  layoutSource = new BehaviorSubject<ShiftType>(null);
  currentLayout = this.layoutSource.asObservable();
  constructor(private http: HttpClient) { }

  //Search
  getData(param: ShiftParam, pagination: Pagination) {
    return this.http.get<PaginationResult<MS_Shift>>(`${this.apiUrl}GetData`, { params: { ...param, ...pagination } });
  }

  //Add New
  addNew(model: MS_Shift) {
    return this.http.post<OperationResult>(`${this.apiUrl}AddNew`, model);
  }

  //Edit
  edit(model: MS_Shift) {
    return this.http.put<OperationResult>(`${this.apiUrl}Edit`, model);
  }
}
