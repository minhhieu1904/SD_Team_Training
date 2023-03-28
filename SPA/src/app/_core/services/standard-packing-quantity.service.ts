import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@env/environment';
import { Pagination, PaginationResult } from '@utilities/pagination-utility';
import { MS_Package, standardPackingQuantityParam } from '../models/MS_Package';
import { OperationResult } from '@utilities/operation-result';

@Injectable({
  providedIn: 'root'
})
export class StandardPackingQuantityService {
  apiUrl: string = environment.apiUrl;
  constructor(private http: HttpClient) { }
  
  getData(pagination: Pagination, param: standardPackingQuantityParam) { 
    let params = new HttpParams().appendAll({ ...pagination, ...param});
    return this.http.get<PaginationResult<MS_Package>>(this.apiUrl + "C_StandardPackingQuantity/getData", { params: params});
  }
  addNew(ms_Package : MS_Package) { 
    return this.http.post<OperationResult>(this.apiUrl + "C_StandardPackingQuantity/add", ms_Package);
  }

  getItem(manuf:string, packageNo:string){
    //khai bao bien
    let params = new HttpParams().set('manuf', manuf).set('packageNo',packageNo);
    return this.http.get<MS_Package>(this.apiUrl + "C_StandardPackingQuantity/getDataOnly" , { params });
  }
  update(ms_Package: MS_Package){
    return this.http.put<OperationResult>(this.apiUrl + "C_StandardPackingQuantity/update", ms_Package);
  }
}
