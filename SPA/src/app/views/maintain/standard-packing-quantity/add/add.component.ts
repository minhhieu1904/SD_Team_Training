import { Component, OnInit } from '@angular/core';
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
  msPackage: MsPackage = <MsPackage>{
    manuf: 'N',
    packageNo: '',
    packageQty: 0,
  };

  constructor(private service: StandardPackingQuantityService) {
    super();
  }

  ngOnInit(): void {}

  add() {
    this.spinnerService.show();
    this.service.add(this.msPackage).subscribe({
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

  back() {
    this.router.navigate([url.maintain.department_data_maintain]);
  }
}
