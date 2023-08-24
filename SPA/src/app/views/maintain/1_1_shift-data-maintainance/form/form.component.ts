import { Component, EventEmitter, OnInit } from '@angular/core';
import { IconButton } from '@constants/common.constants';
import { ShiftType } from '@models/maintain/shift-data-maintenance';
import { ShiftDataMaintainanceService } from '@services/maintain/shift-data-maintainance.service';
import { InjectBase } from '@utilities/inject-base-app';
import { BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.scss']
})
export class FormComponent extends InjectBase implements OnInit {
  shiftEmitter: EventEmitter<boolean> = new EventEmitter();
  iconButton = IconButton;
  shiftType: ShiftType = <ShiftType>{
    ms_Shift: {
      shift: '',
      shiftName: ''
    }
  };

  constructor(
    private service: ShiftDataMaintainanceService,
    public modal: BsModalRef,
  ) {
    super();
  }

  ngOnInit(): void {
  }

  saveChange(type?: string) {
    this.spinnerService.show();
    if (this.shiftType.type === 'add') {
      this.service.addNew(this.shiftType.ms_Shift).subscribe({
        next: (res) => {
          if (res.isSuccess) {
            this.snotifyService.success(this.translateService.instant('System.Message.CreateOKMsg'), this.translateService.instant('System.Caption.Success'));
            this.closeModal(true);
            if (type == 'next') {
              this.shiftType.ms_Shift.shift = '';
              this.shiftType.ms_Shift.shiftName = '';
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
      this.service.edit(this.shiftType.ms_Shift).subscribe({
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
      this.shiftEmitter.emit(isSuccess);
    this.modal.hide();
  }
}
