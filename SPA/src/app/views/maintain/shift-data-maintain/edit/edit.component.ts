import { Component, OnInit } from '@angular/core';
import { IconButton } from '@constants/common.constants';
import { CaptionConstants } from '@constants/message.enum';
import { MessageConstants } from '@constants/message.enum';
import { url } from '@constants/url.constants';
import { MsShift } from '@models/maintain/msShift';
import { ShiftDataMaintainService } from '@services/maintain/shift-data-maintain.service';
import { InjectBase } from '@utilities/inject-base-app';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.scss'],
})
export class EditComponent extends InjectBase implements OnInit {
  //#region attribute
  iconButton = IconButton;
  msShift: MsShift = <MsShift>{
    manuf: 'N',
    shift: '',
    shiftName: '',
  };
  //#endregion

  constructor(private service: ShiftDataMaintainService) {
    super();
  }

  ngOnInit(): void {
    let manuf = '';
    let shift = '';

    this.route.params.subscribe((params) => {
      manuf = params['manuf'];
      shift = params['shift'];
      this.getMsShift(manuf, shift);
    });
  }

  //#region function
  getMsShift(manuf: string, shift: string) {
    this.service.getDataOnly(manuf, shift).subscribe({
      next: (result) => {
        this.msShift = result;
      },
      error: () => {
        this.snotifyService.error(
          this.translateService.instant('System.Message.SystemError'),
          this.translateService.instant('System.Caption.Error')
        );
      },
    });
  }

  back() {
    this.router.navigate([url.maintain.shift_data_maitain]);
  }

  vadidate() {
    if (
      this.functionUtility.checkEmpty(this.msShift.shift) ||
      this.functionUtility.checkEmpty(this.msShift.shiftName)
    )
      return true;
    return false;
  }

  update() {
    if (this.vadidate())
      return this.snotifyService.error(
        MessageConstants.PLEASE_FILL_REQUIRED,
        this.translateService.instant('System.Caption.Error')
      );

    this.spinnerService.show();
    this.service.update(this.msShift).subscribe({
      next: (result) => {
        this.spinnerService.hide();
        if (result.isSuccess) {
          this.snotifyService.success(
            this.translateService.instant('System.Message.UpdateOKMsg'),
            this.translateService.instant('System.Caption.Success')
          );
          this.back();
        } else {
          if (!result.error)
            this.snotifyService.error(
              this.translateService.instant('System.Message.UpdateErrorMsg'),
              this.translateService.instant('System.Caption.Error')
            );
          else
            this.snotifyService.error(
              result.error,
              this.translateService.instant('System.Caption.Error')
            );
        }
      },
      error: () => {
        this.spinnerService.hide();
        this.snotifyService.error(
          this.translateService.instant('System.Message.SystemError'),
          this.translateService.instant('System.Caption.Error')
        );
      },
    });
  }
  //#endregion
}
