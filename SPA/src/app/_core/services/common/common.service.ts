import { KeyValueUtility } from './../../utilities/key-value-utility';
import { HttpClient } from '@angular/common/http';
import { environment } from './../../../../environments/environment';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class CommonService {
  apiUrl: string = environment.apiUrl;
  constructor(
    private http: HttpClient
  ) { }

  getListBrandName () {
    return this.http.get<KeyValueUtility[]>(`${this.apiUrl}Common/GetListBrandName`);
  }
}
