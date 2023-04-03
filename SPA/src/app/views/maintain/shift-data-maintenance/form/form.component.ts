import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { Route, Router } from '@angular/router';
import { IconButton } from '@constants/common.constants';
import { CaptionConstants, MessageConstants } from '@constants/message.enum';
import { MS_Shift } from '@models/maintain/msshift';
import { ShiftDataMaintenanceService } from '@services/shift-data-maintenance.service';
import { InjectBase } from '@utilities/inject-base-app';

@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.scss'],
})
export class FormComponent extends InjectBase implements OnInit {
  icon = IconButton;
  title: string;

  paramData: MS_Shift = <MS_Shift>{
    nanuf: 'N',
    shift: '',
    shiftName: '',
  };

  constructor(
    private service: ShiftDataMaintenanceService,
  ) {super()}

  ngOnInit() {
    if (this.router.url === '/maintain/shift-data-maintenance/edit') {
      this.title = 'Edit';
      this.service.currentDataSource.subscribe((res) => {
        if (res) {
          (this.paramData.shift = res.shift),
            (this.paramData.shiftName = res.shiftName);
        }
      });
    } else {
      this.title = 'AddNew';
    }
  }

  save() {
    if (this.router.url === '/maintain/shift-data-maintenance/edit') {
      this.spinnerService.show();

      this.service.update(this.paramData).subscribe({
        next: (res) => {
          this.spinnerService.hide();
          if(res.isSuccess) {
            this.snotifyService.success(MessageConstants.UPDATED_OK_MSG, CaptionConstants.SUCCESS);
            this.router.navigate(['maintain/shift-data-maintenance']);
          }else {
            this.snotifyService.error(MessageConstants.UPDATED_ERROR_MSG, CaptionConstants.ERROR);
          }
        },
        error: (err) => {
          this.snotifyService.error(MessageConstants.UN_KNOWN_ERROR, CaptionConstants.ERROR);
          this.spinnerService.hide();
        },
      });
    }

    if (this.router.url === '/maintain/shift-data-maintenance/add') {
      this.spinnerService.show();

      this.service.create(this.paramData).subscribe({
        next: (res) => {
          this.spinnerService.hide();
          if(res.isSuccess) {
            this.snotifyService.success(MessageConstants.CREATED_OK_MSG, CaptionConstants.SUCCESS);
            this.router.navigate(['maintain/shift-data-maintenance']);
          }else {
            this.snotifyService.error(MessageConstants.CREATED_ERROR_MSG, CaptionConstants.ERROR);
          }
        },
        error: (err) => {
          this.snotifyService.error(MessageConstants.UN_KNOWN_ERROR, CaptionConstants.ERROR);
          this.spinnerService.hide();
        },
      });
    }
  }

  back() {
    this.router.navigate(['maintain/shift-data-maintenance']);
  }
}
