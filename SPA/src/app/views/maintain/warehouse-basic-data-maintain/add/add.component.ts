import { Component, OnInit } from '@angular/core';
import { MsLocation } from '@models/maintain/msLocation';
import { WarehouseBasicDataService } from '@services/maintain/warehouse-basic-data.service';
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
  msLocation: MsLocation = <MsLocation>{
    manuf: 'N',
    storeH: '',
    locationName: '',
  };
  //#endregion

  constructor(private service: WarehouseBasicDataService) {
    super();
  }

  ngOnInit(): void {}

  //#region function
  back() {
    this.router.navigate([url.maintain.warehouse_basic_data_maintain]);
  }
  vadidate() {
    if (
      this.functionUtility.checkEmpty(this.msLocation.locationName) ||
      this.functionUtility.checkEmpty(this.msLocation.storeH)
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
    this.service.add(this.msLocation).subscribe({
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
