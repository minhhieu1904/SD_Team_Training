import { Injectable } from '@angular/core';
import { environment } from './../../../environments/environment';
import { FunctionUtility } from './../utilities/function-utility';
import { HttpClient, HttpParams } from "@angular/common/http";
import { KeyValueUtility } from './../utilities/key-value-utility';
import { Pagination, PaginationResult } from './../utilities/pagination-utility';
import { SearchForOrderDataParam } from './../models/transaction/searchForOrderDataParam';
import { SearchForOrderDataViewModel, OrderDataPrint, OrderPrintResult } from './../models/transaction/search-for-order-data';
import { BehaviorSubject } from 'rxjs';
import { OperationResult } from './../utilities/operation-result';

@Injectable({
  providedIn: 'root'
})
export class SearchForOrderDataService {
  apiUrl: string = environment.apiUrl;
  printQrCodeSource = new BehaviorSubject<OrderPrintResult[]>([]);
  currentPrintQrCode = this.printQrCodeSource.asObservable();

  constructor(
    private http: HttpClient,
  ) { }

  getListPackage() {
    return this.http.get<KeyValueUtility[]>(`${this.apiUrl}SearchForOrderData/GetListPackage`);
  }

  getDataPagination(pagination: Pagination, param: SearchForOrderDataParam) {
    let params = new HttpParams().appendAll({...param}).set('PageNumber', pagination.pageNumber).set('PageSize', pagination.pageSize);
    return this.http.get<PaginationResult<SearchForOrderDataViewModel>>(`${this.apiUrl}SearchForOrderData/GetDataPagination`, { params: params });
  }

  orderPrint(orderDataPrint: OrderDataPrint) {
    return this.http.post<OperationResult>(`${this.apiUrl}SearchForOrderData/OrderPrint`, orderDataPrint);
  }

  getListBrandname() {
    return this.http.get<KeyValueUtility[]>(`${this.apiUrl}SearchForOrderData/GetListBrandname`);
  }
}
