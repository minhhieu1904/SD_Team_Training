import { Component, Inject, OnInit } from '@angular/core';
import { MsDepartment } from '@models/maintain/msDepartment';
import { DepartmentDataMaintainService } from '@services/maintain/department-data-maintain.service';
import { InjectBase } from '@utilities/inject-base-app';
import { url } from '@constants/url.constants';
import { CaptionConstants, MessageConstants } from '@constants/message.enum';
import { IconButton } from '@constants/common.constants';
@Component({
  selector: 'app-add',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.scss'],
})
export class AddComponent extends InjectBase implements OnInit {
  //#region attribute
  iconButton = IconButton;
  msDepartment: MsDepartment = <MsDepartment>{
    manuf: 'N',
    parNo: '',
    parName: '',
  };
  //#endregion

  constructor(private service: DepartmentDataMaintainService) {
    super();
  }

  ngOnInit() {}

  //#region function
  back() {
    this.router.navigate([url.maintain.department_data_maintain]);
  }

  vadidate() {
    if (
      this.functionUtility.checkEmpty(this.msDepartment.parName) ||
      this.functionUtility.checkEmpty(this.msDepartment.parNo)
    )
      return true;
    return false;
  }

  add() {
    if (this.vadidate())
      return this.snotifyService.error(
        MessageConstants.PLEASE_FILL_REQUIRED,
        this.translateService.instant('System.Caption.Error')
      );

    this.spinnerService.show();
    this.service.add(this.msDepartment).subscribe({
      next: (result) => {
        this.spinnerService.hide();
        if (result.isSuccess) {
          this.snotifyService.success(
            this.translateService.instant('System.Message.CreateOKMsg'),
            this.translateService.instant('System.Caption.Success')
          );
          this.back();
        } else {
          if (!result.error)
            this.snotifyService.error(
              this.translateService.instant('System.Message.CreateErrorMsg'),
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
