import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { MSShift } from '../models/ms-shift';
import { OperationResult } from "../utilities/operation-result";

@Injectable({
  providedIn: 'root'
})
export class MsShiftService {
  //Khai báo đường dẫn API
  baseUrl: string = 'https://localhost:5001/api/MSShift/';
  constructor(private _htp: HttpClient) { }

  //Lay du lieu
  getData() {
    return this._htp.get<MSShift[]>(this.baseUrl + 'GetData');
  }

  // Lấy 1 item để chỉnh sửa
  getItem(manuf: string, shift: string) {
    // khai báo param truyền vào 
    let params = new HttpParams().set('manuf', manuf).set('shift', shift);
    return this._htp.get<MSShift>(this.baseUrl + 'get-item', { params });
  }

  //Them moi
  addShift(msshift: MSShift) {
    return this._htp.post<OperationResult>(this.baseUrl + 'AddNew', msshift);
  }
  //Cap nhat
  updateShift(msshift: MSShift) {
    return this._htp.put<OperationResult>(this.baseUrl + 'Edit', msshift);
  }

}
