import { Component, OnInit } from '@angular/core';
import { CaptionConstants, MessageConstants } from '@constants/message.enum';
import { url } from '@constants/url.constants';
import { MsPackage } from '@models/maintain/msPackage';
import { StandardPackingQuantityService } from '@services/maintain/standard-packing-quantity.service';
import { InjectBase } from '@utilities/inject-base-app';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.scss'],
})
export class EditComponent extends InjectBase implements OnInit {
  msPackage: MsPackage = <MsPackage>{
    manuf: 'N',
    packageNo: '',
    packageQty: 0,
  };
  constructor(private service: StandardPackingQuantityService) {
    super();
  }

  ngOnInit(): void {
    let manuf = '';
    let packageNo = '';

    this.route.params.subscribe((params) => {
      manuf = params['manuf'];
      packageNo = params['packageNo'];
      this.getMsPackage(manuf, packageNo);
    });
  }

  getMsPackage(manuf: string, packageNo: string) {
    this.service.getDataOnly(manuf, packageNo).subscribe({
      next: (result) => {
        this.msPackage = result;
      },
      error: () => {
        this.snotifyService.error(
          MessageConstants.SYSTEM_ERROR_MSG,
          CaptionConstants.ERROR
        );
      },
    });
  }

  update() {
    this.spinnerService.show();
    this.service.update(this.msPackage).subscribe({
      next: (result) => {
        this.spinnerService.hide();
        if (result.isSuccess) {
          this.snotifyService.error(
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
  back() {
    this.router.navigate([url.maintain.department_data_maintain]);
  }
}
