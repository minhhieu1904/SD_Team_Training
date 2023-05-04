import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { KeyValuePair } from '@utilities/key-value-pair';
import { environment } from '@env/environment';

@Injectable({
  providedIn: 'root',
})
export class CommonService {
  apiUrl = environment.apiUrl + 'C_Common';
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
}
