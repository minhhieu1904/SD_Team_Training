import { HttpParams } from '@angular/common/http';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { MS_Shift_DTO } from '../models/mS_Shift_DTO';

@Injectable({
  providedIn: 'root'
})
export class ShiftDataMaintainService {
baseUrl: string = 'https://localhost:5001/api/Shift_Data_Maintain/'
  constructor(
    private http:HttpClient
  ) { }
  getData(dto: MS_Shift_DTO){
    let param = new HttpParams().set('Shift', dto.shift).set('ShiftName', dto.shiftName)
  //  return this.http.get(this.baseUrl +'Search',{param})
  }
}
