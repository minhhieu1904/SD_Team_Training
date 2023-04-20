import { Component, OnInit } from '@angular/core';
import { CaptionConstants, MessageConstants } from '@constants/message.enum';
import { url } from '@constants/url.constants';
import { MsShift } from '@models/maintain/msShift';
import { ShiftDataMaintainService } from '@services/maintain/shift-data-maintain.service';
import { InjectBase } from '@utilities/inject-base-app';

@Component({
  selector: 'app-add',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.scss'],
})
export class AddComponent extends InjectBase implements OnInit {
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

  ngOnInit(): void {}
  
  //#region function
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
  add() {
    if (this.vadidate())
      return this.snotifyService.error(
        MessageConstants.PLEASE_FILL_REQUIRED,
        CaptionConstants.ERROR
      );

    this.spinnerService.show();
    this.service.add(this.msShift).subscribe({
      next: (result) => {
        this.spinnerService.hide();
        if (result.isSuccess) {
          this.snotifyService.success(
            MessageConstants.CREATED_OK_MSG,
            CaptionConstants.SUCCESS
          );
          this.back();
        } else {
          this.snotifyService.error(
            MessageConstants.CREATED_ERROR_MSG,
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
