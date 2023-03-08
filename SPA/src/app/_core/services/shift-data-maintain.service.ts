import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { MS_Shift } from '../models/shift-data-maintain';
@Injectable({
  providedIn: 'root'
})
export class ShiftDataMaintainService {
  apiUrl: string = environment.apiUrl;
  constructor(private http: HttpClient) { }

  getData() { 
    return this.http.get<MS_Shift[]>(this.apiUrl + "C_ShiftDataMaintain/get-data");
  }

}
