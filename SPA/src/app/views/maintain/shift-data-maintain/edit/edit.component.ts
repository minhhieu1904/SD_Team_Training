import { Component, OnInit } from '@angular/core';
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
          MessageConstants.SYSTEM_ERROR_MSG,
          CaptionConstants.ERROR
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
        CaptionConstants.ERROR
      );

    this.spinnerService.show();
    this.service.update(this.msShift).subscribe({
      next: (res) => {
        this.spinnerService.hide();
        if (res.isSuccess) {
          this.snotifyService.success(
            MessageConstants.UPDATED_OK_MSG,
            CaptionConstants.SUCCESS
          );
          this.back();
        } else {
          this.snotifyService.error(
            MessageConstants.UPDATED_ERROR_MSG,
            CaptionConstants.ERROR
          );
        }
      },
      error: () => {
        this.spinnerService.hide();
        this.snotifyService.error(
          MessageConstants.SYSTEM_ERROR_MSG,
          CaptionConstants.ERROR
        );
      },
    });
  }
  //#endregion
}
