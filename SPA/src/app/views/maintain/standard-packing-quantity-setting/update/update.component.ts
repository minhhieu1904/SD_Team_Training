import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { IconButton } from "@constants/common.constants";
import { MS_Package } from "@models/mS_Package_DTO";
import { NgSnotifyService } from "@services/ng-snotify.service";
import { StandardPackingQuantitySettingService } from "@services/standard-packing-quantity-setting.service";
import { OperationResult } from "@utilities/operation-result";


@Component({
  selector: 'app-update',
  templateUrl: './update.component.html',
  styleUrls: ['./update.component.scss']
})
export class UpdateComponent implements OnInit {

  model:MS_Package = <MS_Package>{
    packageQty:0
  };
  iconButton = IconButton

  constructor(
    private standardPackingQuantitySettingService: StandardPackingQuantitySettingService,
    private route: Router,
    private snotify: NgSnotifyService
  ) { }

   ngOnInit() {
    this.loadData();
  }

  loadData(){
    this.standardPackingQuantitySettingService.msPackageCurrent.subscribe({
     next:(res)=>{
      if(!res){
        this.back();
      }
        this.model = res;
     },
     error:()=>{},
     complete:()=>{},
    })
  }

  back(){
    this.route.navigate(["maintain/standard-parking-quantity-setting"])
  }

  saveChange(){
    this.snotify.confirm("Bạn có muốn cập nhật không?", "Cập nhật", () => {
      this.standardPackingQuantitySettingService.update(this.model).subscribe({
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
      });
    });
  }
}
