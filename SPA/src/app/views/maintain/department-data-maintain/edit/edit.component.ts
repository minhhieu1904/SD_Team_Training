import { Component, OnInit } from '@angular/core';
import { IconButton } from '@constants/common.constants';
import { CaptionConstants, MessageConstants } from '@constants/message.enum';
import { url } from '@constants/url.constants';
import { MsDepartment } from '@models/maintain/msDepartment';
import { DepartmentDataMaintainService } from '@services/maintain/department-data-maintain.service';
import { InjectBase } from '@utilities/inject-base-app';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.scss'],
})
export class EditComponent extends InjectBase implements OnInit {
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

  ngOnInit() {
    let manuf = '';
    let parNo = '';

    this.route.params.subscribe((params) => {
      manuf = params['manuf'];
      parNo = params['parNo'];
      this.getMsDepartment(manuf, parNo);
    });
  }
  //#region function
  getMsDepartment(manuf: string, parNo: string) {
    this.service.getDataOnly(manuf, parNo).subscribe({
      next: (result) => {
        this.msDepartment = result;
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
  update() {
    if (this.vadidate())
      return this.snotifyService.error(
        MessageConstants.PLEASE_FILL_REQUIRED,
        this.translateService.instant('System.Caption.Error')
      );

    this.spinnerService.show();
    this.service.update(this.msDepartment).subscribe({
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
