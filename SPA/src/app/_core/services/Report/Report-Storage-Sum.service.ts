
import { Pagination,PaginationResult } from '@utilities/pagination-utility';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '@env/environment';
import { Injectable } from '@angular/core';
import { Storage_sumDTO ,Report_Storage_SumParam,Brand,StorageSumDeltailDTOParam} from '@models/report/storage-Sum';

@Injectable({
  providedIn: 'root'
})
export class ReportStorageSumService {
  apiUrl: string = `${environment.apiUrl}C_Report_Storage_Sum`;

constructor(private http: HttpClient) { }
getData(pagination : Pagination, param: Storage_sumDTO){
  let params = new HttpParams().appendAll({...pagination, ... param});
  return this.http.get<PaginationResult<Report_Storage_SumParam>>(this.apiUrl + "/getData", {params: params});
}
getBrand() {
  return this.http.get<Brand[]>(this.apiUrl +"/GetBrand");
}
exportExcel(param: Report_Storage_SumParam){
  let params =  new HttpParams().appendAll({...param});
  return this.http.get(`${this.apiUrl}/Export` , {params, responseType : 'blob',});
}
exportExcelDetail(param: StorageSumDeltailDTOParam) {
  let params = new HttpParams().appendAll({ ...param });
  return this.http.get(` ${this.apiUrl}/ExportExcelDetail`, {
    params,
    responseType: 'blob',
  });
}
}
