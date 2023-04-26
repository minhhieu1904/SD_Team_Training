import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {environment} from '@env/environment';
import { KeyValueUtility } from '@utilities/key-value-utility';


@Injectable({
  providedIn: 'root'
})
export class CommonService {
  apiUrl: string = `${environment.apiUrl}C_PackingScan`;
  constructor(private http: HttpClient) { }

  getListShift() {
    return this.http.get<KeyValueUtility[]>(`${this.apiUrl}/getListShift`);
  }
}
