import { WarehouseBasicDataMaintenanceService } from '@services/maintain/warehouse-basic-data-maintenance.service';
import { Component, EventEmitter, OnInit } from '@angular/core';
import { IconButton } from '@constants/common.constants';
import { WarehouseType } from '@models/maintain/warehouse-basic-data-maintenance';
import { InjectBase } from '@utilities/inject-base-app';
import { BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.scss']
})
export class FormComponent extends InjectBase implements OnInit {
  warehouseEmitter: EventEmitter<boolean> = new EventEmitter();
  iconButton = IconButton;
  warehouseType: WarehouseType = <WarehouseType>{
    ms_Location: {
      storeH: '',
      locationName: ''
    }
  };

  constructor(
    private service: WarehouseBasicDataMaintenanceService,
    public modal: BsModalRef,
  ) {
    super();
  }

  ngOnInit(): void {
  }

  saveChange(type?: string) {
    this.spinnerService.show();
    if (this.warehouseType.type === 'add') {
      this.service.addNew(this.warehouseType.ms_Location).subscribe({
        next: (res) => {
          if (res.isSuccess) {
            this.snotifyService.success(this.translateService.instant('System.Message.CreateOKMsg'), this.translateService.instant('System.Caption.Success'));
            this.closeModal(true);
            if (type == 'next') {
              this.warehouseType.ms_Location.storeH = '';
              this.warehouseType.ms_Location.locationName = '';
            }
          }
          else this.snotifyService.error(this.translateService.instant('System.Message.CreateErrorMsg'), this.translateService.instant('System.Caption.Error'));
          this.spinnerService.hide();
        },
        error: () => {
          this.snotifyService.error(this.translateService.instant('System.Message.CreateErrorMsg'), this.translateService.instant('System.Caption.Error'));
          this.spinnerService.hide();
        }
      })
    }
    else {
      this.spinnerService.show();
      this.service.edit(this.warehouseType.ms_Location).subscribe({
        next: (res) => {
          if (res.isSuccess) {
            this.snotifyService.success(this.translateService.instant('System.Message.UpdateOKMsg'), this.translateService.instant('System.Caption.Success'));
            this.closeModal(true);
          }
          else this.snotifyService.error(this.translateService.instant('System.Message.UpdateErrorMsg'), this.translateService.instant('System.Caption.Error'));
          this.spinnerService.hide();
        },
        error: () => {
          this.snotifyService.error(this.translateService.instant('System.Message.UpdateErrorMsg'), this.translateService.instant('System.Caption.Error'));
          this.spinnerService.hide();
        }
      });
    }
  }

  close() {
    this.closeModal(false);
  }

  closeModal(isSuccess: boolean) {
    if (isSuccess == true)
      this.warehouseEmitter.emit(isSuccess);
    this.modal.hide();
  }
}
