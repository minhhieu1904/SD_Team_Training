import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from './../../../../environments/environment';
import { KeyValueUtility } from './../../utilities/key-value-utility';


@Injectable({
  providedIn: 'root',
})
export class TransactionCommonService {
  apiUrl = environment.apiUrl;
  constructor(private http: HttpClient) {}

  getListShift() {
    return this.http.get<KeyValueUtility[]>(this.apiUrl+ 'TransactionCommon/GetListShift');
  }
}
