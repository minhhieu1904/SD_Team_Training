import { Component, OnInit } from '@angular/core';
import { IconButton } from '@constants/common.constants';
import { CaptionConstants, MessageConstants } from '@constants/message.enum';
import { url } from '@constants/url.constants';
import { MsPackage } from '@models/maintain/msPackage';
import { StandardPackingQuantityService } from '@services/maintain/standard-packing-quantity.service';
import { InjectBase } from '@utilities/inject-base-app';

@Component({
  selector: 'app-add',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.scss'],
})
export class AddComponent extends InjectBase implements OnInit {
  //#region attribute
  iconButton = IconButton;
  msPackage: MsPackage = <MsPackage>{
    manuf: 'N',
    packageNo: '',
    packageQty: 0,
  };
  //#endregion

  constructor(private service: StandardPackingQuantityService) {
    super();
  }

  ngOnInit(): void {}

  //#region function
  back() {
    this.router.navigate([url.maintain.standard_packing_quantity]);
  }
  vadidate() {
    if (this.functionUtility.checkEmpty(this.msPackage.packageNo)) return true;
    return false;
  }

  add() {
    if (this.vadidate())
      return this.snotifyService.error(
        MessageConstants.PLEASE_FILL_REQUIRED,
        this.translateService.instant('System.Caption.Error')
      );

    this.spinnerService.show();
    this.service.add(this.msPackage).subscribe({
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
