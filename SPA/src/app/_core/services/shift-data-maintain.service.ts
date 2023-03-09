import { Pagination, PaginationResult } from './../utilities/pagination-utility';
import { Observable } from 'rxjs';
import { OperationResult } from './../utilities/operation-result';
import { HttpParams } from '@angular/common/http';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { MS_Shift } from '@models/mS_Shift_DTO';

@Injectable({
  providedIn: 'root'
})
export class ShiftDataMaintainService {
baseUrl: string = 'https://localhost:5001/api/Shift_Data_Maintain/'
  constructor(
    private http:HttpClient
  ) { }
  getData(param: MS_Shift, pagination:Pagination):Observable<PaginationResult<MS_Shift>>{
    // {...pagination } này là set lại giá trị cho nó, ban đầu không có thì nó cứ gửi mặc định là 1, giờ nó có thì mình set lại giá trị cho nó
    let params = new HttpParams().appendAll({...pagination })
    .set('Shift', param.shift ?? "")
    .set('ShiftName', param.shiftName ?? "")
    .set('Manuf', null)
  return this.http.get<PaginationResult<MS_Shift>>(this.baseUrl +'Search',{params:params});
  }
}
