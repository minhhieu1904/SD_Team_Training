import { Injectable } from '@angular/core';
import {
  Pagination,
  PaginationResult,
} from '../../utilities/pagination-utility';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '../../../../environments/environment';
import { SearchForPackingScanParam } from '../../models/report/searchForPackingScanParam';
import { MsQrOrder } from '../../models/report/msQrOrder';
import { BrandDTO } from "../../models/report/brandDTO";
@Injectable({
  providedIn: 'root',
})
export class SearchForPackingScanService {
  apiUrl = environment.apiUrl + 'C_SearchForPackingScan';
  constructor(private http: HttpClient) {}

  getData(pagination: Pagination, param: SearchForPackingScanParam) {
    let params = new HttpParams().appendAll({ ...pagination, ...param });
    return this.http.get<PaginationResult<MsQrOrder>>(
      `${this.apiUrl}/GetData`,
      { params }
    );
  }

  getBrand(){
    return this.http.get<BrandDTO[]>(`${this.apiUrl}/GetBrand`);
  }

  exportExcel(param: SearchForPackingScanParam){
    let params = new HttpParams().appendAll({...param});
    return this.http.get(`${this.apiUrl}/ExportExcel`, {params, responseType: 'blob'})
  }
}
