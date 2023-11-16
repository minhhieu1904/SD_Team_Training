import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@env/environment';
import { Pagination } from '@utilities/pagination-utility';
import { OrderDataStatusAdjustParam, OrderDataStatusAdjustResponse, OrderDataStatusAdjustUpdate } from '@models/transaction/order-data-status-adjust'
import { OperationResult } from '@utilities/operation-result';

@Injectable({
  providedIn: 'root'
})
export class OrderDataStatusAdjustService {
  baseUrl: string = `${environment.apiUrl}C_OrderDataStatusAdjust/`;
  constructor(private http: HttpClient) { }

  getData(pagination: Pagination, filter: OrderDataStatusAdjustParam) {
    let params = new HttpParams().appendAll({ ...pagination, ...filter });
    return this.http.get<OrderDataStatusAdjustResponse>(`${this.baseUrl}GetDataPagination`, { params });
  }

  updateCancelOrReOpen(model: OrderDataStatusAdjustUpdate) {
    return this.http.put<OperationResult>(`${this.baseUrl}/Update`, model);
  }
}
