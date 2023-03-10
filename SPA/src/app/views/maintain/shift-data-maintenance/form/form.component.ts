import { SnotifyModule } from 'ng-snotify';
import { routes } from './../../../../app.routing';
import { Router } from '@angular/router';
import { OperationResult } from './../../../../_core/utilities/operation-result';
import { ShiftDataMaintainService } from './../../../../_core/services/shift-data-maintain.service';
import { MS_Shift } from './../../../../_core/models/mS_Shift_DTO';
import { Component, OnInit } from '@angular/core';
import {NgSnotifyService} from '../../../../_core/services/ng-snotify.service';

@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.scss']
})
export class FormComponent implements OnInit {

  model: MS_Shift = {} as MS_Shift;

  // constructor(
  //   private service: ShiftDataMaintainService,
  //   private route: Router
  // ) { }

  //Khai báo service ở contructor
  constructor(
    private service: ShiftDataMaintainService,
    private route: Router,
    private snotify: NgSnotifyService
  ) { }

  ngOnInit(): void {
  }

  // saveChange(){
  //   this.service.addNew(this.model).subscribe({
  //     next:(res:OperationResult)=>{
  //       if(res.isSuccess){
  //         this.back();
  //       }else{

  //       }
  //     },
  //     error:()=>{},
  //     complete:()=>{}
  //   })
  // }

  saveChange(){
    //con confirm dung nhu nay ne, minh phai khi bao cai minh config

    this.snotify.confirm("Ban co muon them moi khong ?","Them moi", () => {
      this.service.addNew(this.model).subscribe({
        next:(res:OperationResult)=>{
          if(res.isSuccess){
            this.back();
            //Title la cai nam tren, con body la o duoi
            this.snotify.success("Add success", "Success");
          }else{
            this.snotify.error("Add error","Error");
          }
        },
        error:()=>{this.snotify.error("Add error","Error");},
        complete:()=>{}
      })
    })




  }

  back(){
    // quay lại trang main search
    this.route.navigate(["maintain/shift-data-maintenance"])
  }

}
