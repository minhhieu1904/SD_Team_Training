import { KeyValueUtility } from '@utilities/key-value-utility';
import { OperationResult } from '@utilities/operation-result';
import { SearchForOrderDataParam, SearchForOrderDataViewModel, OrderDataPrint } from '@models/Transaction/SearchForOrderData/Search_For_Order_Data';
import { Pagination, PaginationResult } from '@utilities/pagination-utility';
import { FunctionUtility } from '@utilities/function-utility';
import { HttpClient } from '@angular/common/http';
import { environment } from '@env/environment';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class SearchForOrderDataService {
  apiUrl: string = `${environment.apiUrl}C_SearchForOrderData`;

constructor(  private http: HttpClient,
  private functionUtility: FunctionUtility) { }

  getListPackage() {
    return this.http.get<KeyValueUtility[]>(`${this.apiUrl}/GetListPackage`);
  }

  getDataPagination(pagination: Pagination, param: SearchForOrderDataParam) {
    let params = this.functionUtility.toParams(param).set('PageNumber', pagination.pageNumber).set('PageSize', pagination.pageSize);
    return this.http.get<PaginationResult<SearchForOrderDataViewModel>>(`${this.apiUrl}/GetDataPagination`, { params: params });
  }

  orderPrint(orderDataPrint: OrderDataPrint) {
    return this.http.post<OperationResult>(`${this.apiUrl}/OrderPrint`, orderDataPrint);
  }
  getListBrandname() {
    return this.http.get<KeyValueUtility[]>(`${this.apiUrl}/GetListBrandname`);
  }
}
