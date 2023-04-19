import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@env/environment';
import { OrderDataPrint, SearchForOrderDataDTO } from '@models/transaction/searchForOrderDataDTO';
import { SearchForOrderDataParam } from '@models/transaction/searchForOrderDataParam';
import { KeyValuePair } from '@utilities/key-value-pair';
import { OperationResult } from '@utilities/operation-result';
import { Pagination, PaginationResult } from '@utilities/pagination-utility';
@Injectable({
  providedIn: 'root',
})
export class SearchForOrderDataService {
  apiUrl = environment.apiUrl + 'C_SearchForOrderData';
  constructor(private http: HttpClient) {}

  getListBrandName() {
    return this.http.get<KeyValuePair[]>(`${this.apiUrl}/GetListBrandName`);
  }

  getListStatus() {
    return this.http.get<KeyValuePair[]>(`${this.apiUrl}/GetListStatus`);
  }

  getListPackage() {
    return this.http.get<KeyValuePair[]>(`${this.apiUrl}/GetListPackage`);
  }

  getData(pagination: Pagination, param: SearchForOrderDataParam) {
    let params = new HttpParams().appendAll({ ...pagination, ...param });
    return this.http.get<PaginationResult<SearchForOrderDataDTO>>(
      `${this.apiUrl}/GetData`,
      { params }
    );
  }

  print(dataPrint: OrderDataPrint) {
    return this.http.post<OperationResult>(`${this.apiUrl}/Print`, dataPrint);
  }

  
}
