import { Component, OnInit } from '@angular/core';
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
          MessageConstants.SYSTEM_ERROR_MSG,
          CaptionConstants.ERROR
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
        CaptionConstants.ERROR
      );

    this.spinnerService.show();
    this.service.update(this.msLocation).subscribe({
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
