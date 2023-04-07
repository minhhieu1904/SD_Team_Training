import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@env/environment';
import { MS_DepartmentParam } from '@models/maintain/msdepartment';
import { MS_Package, MS_PackageParam } from '@models/maintain/mspackage';
import { OperationResult } from '@utilities/operation-result';
import { Pagination, PaginationResult } from '@utilities/pagination-utility';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class StandardPackingQuantitySettingService {
  apiUrl = environment.apiUrl;
  dataSource = new BehaviorSubject<MS_PackageParam>(null);
  currentDataSource = this.dataSource.asObservable();

  constructor(private http: HttpClient) { }
  getAll(pagination: Pagination, param: MS_Package) {
    let params = new HttpParams().appendAll({ ...pagination, ...param });

    return this.http.get<PaginationResult<MS_Package>>(
      this.apiUrl + 'StandardPackingQuantitySetting/GetAll',
      { params }
    );
  }
  create(model: MS_Package): Observable<OperationResult> {
    return this.http.post<OperationResult>(
      this.apiUrl + 'StandardPackingQuantitySetting/Create',
      model
    );
  }
  update(model: MS_Package): Observable<OperationResult> {
    return this.http.put<OperationResult>(
      this.apiUrl + 'StandardPackingQuantitySetting/Update',
      model
    );
  }
  search(pageNumber, pageSize, text: string) {
    let params = new HttpParams();
    if (pageNumber != null && pageSize != null) {
      params = params.append('pageNumber', pageNumber);
      params = params.append('pageSize', pageSize);
    }
    params = params.append('text', text);
    return this.http.get<PaginationResult<MS_Package>>(this.apiUrl + 'StandardPackingQuantitySetting/Search', { params });
  }
}
