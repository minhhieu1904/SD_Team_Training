import { Component, OnInit } from '@angular/core';
import { IconButton } from '@constants/common.constants';
import { CaptionConstants, MessageConstants } from '@constants/message.enum';
import { url } from '@constants/url.constants';
import { MsLocation } from '@models/maintain/msLocation';
import { WarehouseBasicDataService } from '@services/maintain/warehouse-basic-data.service';
import { InjectBase } from '@utilities/inject-base-app';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.scss'],
})
export class EditComponent extends InjectBase implements OnInit {
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

  ngOnInit(): void {
    let manuf = '';
    let storeH = '';

    this.route.params.subscribe((params) => {
      manuf = params['manuf'];
      storeH = params['storeH'];
      this.getMsLocation(manuf, storeH);
    });
  }

  //#region function
  getMsLocation(manuf: string, storeH: string) {
    this.service.getDataOnly(manuf, storeH).subscribe({
      next: (result) => {
        this.msLocation = result;
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

  update() {
    if (this.vadidate())
      return this.snotifyService.error(
        MessageConstants.PLEASE_FILL_REQUIRED,
        this.translateService.instant('System.Caption.Error')
      );

    this.spinnerService.show();
    this.service.update(this.msLocation).subscribe({
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
