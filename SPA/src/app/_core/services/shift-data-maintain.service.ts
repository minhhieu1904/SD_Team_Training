import { Pagination, PaginationResult } from './../utilities/pagination-utility';
import { BehaviorSubject, Observable } from 'rxjs';
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
  // BehaviorSubject là khi mình gửi giá trị vào nó khi chuyển qua trang update mình hứng nhận giá trị nó lại, này có trên gg có gì a tìm hiểu thêm nha
  msShift = new BehaviorSubject<MS_Shift>(null);
  msShiftCurrent = this.msShift.asObservable();
  constructor(
    private http:HttpClient
  ) { }

  // lay data dung http.get, them moi data dung http.post, update dung http.put, xóa data dung http.delete

  getData(param: MS_Shift, pagination:Pagination):Observable<PaginationResult<MS_Shift>>{
    // {...pagination } này là set lại giá trị cho nó, ban đầu không có thì nó cứ gửi mặc định là 1, giờ nó có thì mình set lại giá trị cho nó
    let params = new HttpParams().appendAll({...pagination })
    .set('Shift', param.shift ?? "")
    .set('ShiftName', param.shiftName ?? "")
    .set('Manuf', null)
  return this.http.get<PaginationResult<MS_Shift>>(this.baseUrl +'Search',{params:params});
  }

  // service add new
  addNew(model: MS_Shift):Observable<OperationResult>{
    return this.http.post<OperationResult>(this.baseUrl + 'Create',model);
  }

  // Service Update
  update(model:MS_Shift):Observable<OperationResult>{
    return this.http.put<OperationResult>(this.baseUrl + 'Update',model);
  }
}
