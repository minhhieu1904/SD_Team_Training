import { KeyValueUtility } from '@utilities/key-value-utility';
import { SearchForOrderDataViewModel } from '@models/transaction/search-for-order-data';
import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment'
import { HttpClient, HttpParams } from '@angular/common/http';
import { Pagination, PaginationResult } from '../../utilities/pagination-utility';
import { OrderDataPrint } from '../../models/transaction/search-for-order-data';
import { OperationResult } from '@utilities/operation-result';
import { SearchForOrderDataParam } from '../../_helpers/transaction/searchForOrderDataParam';

@Injectable({
  providedIn: 'root'
})
export class SearchForOrderDataService {
  baseUrl: string = environment.apiUrl + 'C_SearchForOrderData/';
  constructor(private http: HttpClient) { }

  getData(pagination: Pagination, param: SearchForOrderDataParam) {
    let params = new HttpParams().appendAll({ ...pagination, ...param });
    return this.http.get<PaginationResult<SearchForOrderDataViewModel>>(this.baseUrl + 'GetDataPaing', { params });
  }

  print(dataPrint: OrderDataPrint) {
    return this.http.post<OperationResult>(this.baseUrl + 'Print', dataPrint);
  }

  GetListPackage() {
    return this.http.get<KeyValueUtility[]>(this.baseUrl + 'GetListPackage')
  }

}
