import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment'
import { HttpClient } from '@angular/common/http'
import { KeyValueUtility } from '@utilities/key-value-utility';
@Injectable({
  providedIn: 'root'
})
export class CommonService {
  apiUrl: string = environment.apiUrl + "C_Common/";
  constructor(private http: HttpClient) { }

  getListBrandName() {
    return this.http.get<KeyValueUtility[]>(`${this.apiUrl}GetListBrandName`);
  }

  getListLocationName() {
    return this.http.get<KeyValueUtility[]>(`${this.apiUrl}GetListLocationName`);
  }

  getListDepartmentName() {
    return this.http.get<KeyValueUtility[]>(`${this.apiUrl}GetListDepartmentName`);
  }

  getListShift() {
    return this.http.get<KeyValueUtility[]>(`${this.apiUrl}GetListShift`);
  }

  getListTolcls() {
    return this.http.get<KeyValueUtility[]>(`${this.apiUrl}GetListTolcls`);
  }

}
