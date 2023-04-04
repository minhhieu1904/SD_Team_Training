import { Component, OnInit } from '@angular/core';
import { WarehouseBasicDataMaintenanceService } from '@services/warehouse-basic-data-maintenance.service';
import { Router } from '@angular/router'
import { InjectBase } from '@utilities/inject-base-app';
import { MS_WareHouse } from '@models/maintain/mswarehouse';
import { IconButton } from './../../../../_core/constants/common.constants'
import { CaptionConstants, MessageConstants } from '@constants/message.enum';

@Component({
  selector: 'app-warehouse-form',
  templateUrl: './warehouse-form.component.html',
  styleUrls: ['./warehouse-form.component.scss']
})
export class WarehouseFormComponent extends InjectBase implements OnInit {
  param = <MS_WareHouse>{
    warehouseID: '',
    warehouse: 'W',
    warehouseName: '',
  }
  icon = IconButton;

  constructor(private service: WarehouseBasicDataMaintenanceService) { super() }

  title: string;
  ngOnInit() {
    if (this.router.url === '/maintain/warehouse-basic-data-maintenance/edit') {
      this.title = 'Edit';
      this.service.dataSource.subscribe({
        next: result => {
          if (result) {
            this.param.warehouseID = result.warehouseID;
            this.param.warehouseName = result.warehouseName;
          }
        }
      })
    }
    else {
      this.title = "AddNew"
    }
  }

  back() {
    this.router.navigate(['/maintain/warehouse-basic-data-maintenance/'])
  }

  save() {
    if (this.router.url === '/maintain/warehouse-basic-data-maintenance/edit') {
      this.spinnerService.show();
      this.service.update(this.param).subscribe({
        next: result => {
          this.spinnerService.hide();
          if (result.isSuccess) {
            this.snotifyService.success(MessageConstants.UPDATED_OK_MSG, CaptionConstants.SUCCESS);
            this.back();
          }
          else {
            this.snotifyService.error(MessageConstants.UPDATED_ERROR_MSG, CaptionConstants.ERROR)
          }
        },
        error: (err) => {
          this.snotifyService.error(
            MessageConstants.UN_KNOWN_ERROR,
            CaptionConstants.ERROR
          );
          this.spinnerService.hide();
        },
      })
    }
    else {
      this.spinnerService.show();
      this.service.create(this.param).subscribe({
        next: result => {
          this.spinnerService.hide();
          if (result.isSuccess) {
            this.snotifyService.success(MessageConstants.CREATED_OK_MSG, CaptionConstants.SUCCESS);
            this.back();
          }
          else {
            this.snotifyService.warning(MessageConstants.CREATED_ERROR_MSG, CaptionConstants.WARNING);
          }
        },

        error: (err) => {
          this.snotifyService.error(MessageConstants.UN_KNOWN_ERROR, CaptionConstants.ERROR);
          this.spinnerService.hide();
        }
      });
    }
  }
}
