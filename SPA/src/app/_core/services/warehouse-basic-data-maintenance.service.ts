import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import {
  Pagination,
  PaginationResult,
} from './../utilities/pagination-utility';
import {
  MS_WareHouse,
  MS_WareHouseParam,
} from './../models/maintain/mswarehouse';
import { BehaviorSubject, Observable } from 'rxjs';
import { OperationResult } from '@utilities/operation-result';

@Injectable({
  providedIn: 'root',
})
export class WarehouseBasicDataMaintenanceService {
  constructor(private http: HttpClient) { }
  apiUrl: string = environment.apiUrl;

  dataSource = new BehaviorSubject<MS_WareHouseParam>(null);
  currentDataSource = this.dataSource.asObservable();

  getAll(pagination: Pagination, param: MS_WareHouseParam) {
    let params = new HttpParams().appendAll({ ...pagination, ...param });

    return this.http.get<PaginationResult<MS_WareHouse>>(
      this.apiUrl + 'WarehouseBasicDataMain/GetAll',
      { params }
    );
  }

  create(model: MS_WareHouse): Observable<OperationResult> {
    return this.http.post<OperationResult>(
      this.apiUrl + 'WarehouseBasicDataMain/Create',
      model
    );
  }

  update(model: MS_WareHouse): Observable<OperationResult> {
    return this.http.put<OperationResult>(
      this.apiUrl + 'WarehouseBasicDataMain/Update',
      model
    );
  }

  search(pageNumber?, pageSize?, text?: string) {
    let params = new HttpParams();
    if (pageNumber != null && pageSize != null) {
      params = params.append('pageNumber', pageNumber);
      params = params.append('pageSize', pageSize);
    }
    params = params.append('text', text);
    return this.http.get<PaginationResult<MS_WareHouse>>(
      this.apiUrl + 'WarehouseBasicDataMain/Search',
      { params }
    );
  }
}
