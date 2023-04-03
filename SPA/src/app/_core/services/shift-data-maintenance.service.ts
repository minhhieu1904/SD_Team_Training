import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { MS_Shift, ShiftDataMaintainParam} from '../models/maintain/msshift'
import { Pagination, PaginationResult} from '../utilities/pagination-utility'
import { OperationResult } from '../utilities/operation-result'
@Injectable({
  providedIn: 'root'
})
export class ShiftDataMaintenanceService {

  apiUrl: string = environment.apiUrl;
  dataSource = new BehaviorSubject<ShiftDataMaintainParam>(null)
  currentDataSource = this.dataSource.asObservable();


  constructor(private http: HttpClient) { }

  getAll(pagination: Pagination, param: ShiftDataMaintainParam) {
    let params = new HttpParams().appendAll({...pagination, ...param});
    return this.http.get<PaginationResult<MS_Shift>>(this.apiUrl + "ShiftDataMaintain/GetAll", { params });
  }

  create(model: MS_Shift): Observable<OperationResult> {
    return this.http.post<OperationResult>(this.apiUrl + "ShiftDataMaintain/Create", model);
  }

  update(model: MS_Shift): Observable<OperationResult>{
    return this.http.put<OperationResult>(this.apiUrl + "ShiftDataMaintain/Update", model);
  }

}
