import { OperationResult } from './../utilities/operation-result';
import {
  Pagination,
  PaginationResult,
} from './../utilities/pagination-utility';
import { HttpClient, HttpParams } from '@angular/common/http';
import { MS_Location } from './../models/mS_Location_DTO';
import { BehaviorSubject, Observable } from 'rxjs';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class LocationDataMaintenanceService {
  baseUrl: string =
    'https://localhost:5001/api/Warehouse_Basic_Data_Maintenance/';
  msLocation = new BehaviorSubject<MS_Location>(null);
  msLocationCurrent = this.msLocation.asObservable();
  constructor(private http: HttpClient) {}

  getData(
    param: MS_Location,
    pagination: Pagination
  ): Observable<PaginationResult<MS_Location>> {
    let params = new HttpParams()
      .set('PageSize', pagination.pageSize ?? '')
      .set('PageNumber', pagination.pageNumber ?? '')
      .set('StoreH', param.storeH ?? '')
      .set('LocationName', param.locationName ?? '')
      .set('Manuf', 'N');
    console.log(params);

    return this.http.get<PaginationResult<MS_Location>>(
      this.baseUrl + 'Search',
      { params: params }
    );
  }

  addNew(model: MS_Location): Observable<OperationResult> {
    return this.http.post<OperationResult>(this.baseUrl + 'Create', model);
  }

  update(model: MS_Location): Observable<OperationResult> {
    return this.http.put<OperationResult>(this.baseUrl + 'Update', model);
  }
}
