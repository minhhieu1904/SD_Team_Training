import { OperationResult } from './../../../../_core/utilities/operation-result';
import { NgSnotifyService } from './../../../../_core/services/ng-snotify.service';
import { Router } from '@angular/router';
import { ShiftDataMaintainService } from './../../../../_core/services/shift-data-maintain.service';
import { MS_Shift } from '@models/mS_Shift_DTO';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-update',
  templateUrl: './update.component.html',
  styleUrls: ['./update.component.scss']
})
export class UpdateComponent implements OnInit {

  model:MS_Shift = <MS_Shift>{};//mà thường ít dùng kiểu any lắm, mình biết rõ kiểu dữ liệu thì khai báo ra cho nó luôn

  constructor(
    private shiftDataMainService: ShiftDataMaintainService,
    private route: Router,
    private snotify: NgSnotifyService
  ) { }

  //mình muốn làm triển khai các hàm ra thì phải khai báo service ở contructor trước.
  // thường e sẽ đặt tên nhu sau, tại sau này 1 component có nhiều service nên mình ko thể dat985 mỗi tên service dc
  // có cách để update: 1 là dùng behavi, 2 là truyền theo route e vd: http://localhost:4200/#/maintain/shift-data-maintenance/update/id=1 nó sẽ nhận theo dạng id
  // cách này xíu e nói sau
   ngOnInit() {
    this.loadData();
  }

  loadData(){
    this.shiftDataMainService.msShiftCurrent.subscribe({
     next:(res)=>{
      //Nếu mà ko có data truyền vào thì sẽ quay lại trang main luôn
      if(!res){
        this.back();
      }
        this.model = res;
     },
     error:()=>{},
     complete:()=>{},
    })
  }

  //quay lại trang main
  back(){
    this.route.navigate(["maintain/shift-data-maintenance"])
  }

  //lưu dữ liệu vào database
  //Save này thì mình vẫn làm như add new thôi
  //Khi update mình gửi model vừa thay đổi qua api để cập nhật,
  saveChange(){
    this.snotify.confirm("Bạn có muốn cập nhật không?", "Cập nhật", () => {
      this.shiftDataMainService.update(this.model).subscribe({
        next:(res:OperationResult)=>{
          if(res.isSuccess){
            this.back();
            this.snotify.success("Upadte success", "Success");
          }else{
            this.snotify.error("Update fail", "Error");
          }
        },
        error:()=>{},
        complete:()=>{},
      })
    })
  }
}
