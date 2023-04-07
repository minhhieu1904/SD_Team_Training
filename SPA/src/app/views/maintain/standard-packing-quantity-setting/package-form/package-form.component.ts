import { Component, OnInit } from '@angular/core';
import { IconButton } from '@constants/common.constants';
import { CaptionConstants, MessageConstants } from '@constants/message.enum';
import { MS_Package } from '@models/maintain/mspackage';
import { StandardPackingQuantitySettingService } from '@services/standard-packing-quantity-setting.service';
import { InjectBase } from '@utilities/inject-base-app';

@Component({
  selector: 'app-package-form',
  templateUrl: './package-form.component.html',
  styleUrls: ['./package-form.component.scss'],
})
export class PackageFormComponent extends InjectBase implements OnInit {
  constructor(private service: StandardPackingQuantitySettingService) {
    super();
  }
  title: string;
  icon = IconButton;
  param = <MS_Package>{
    manuf: 'N',
    packageNo: '',
    packageQty: 0,
  };
  ngOnInit() {
    if (
      this.router.url === '/maintain/standard-packing-quantity-setting/edit'
    ) {
      this.title = 'Edit';
      this.spinnerService.show();
      this.service.dataSource.subscribe({
        next: (result) => {
          this.spinnerService.hide();
          console.log(result);
          if (result) {
            this.param.packageNo = result.packageNo;
            this.param.packageQty = result.packageQty;
          }
        },
      });
    } else {
      this.title = 'Add';
    }
  }
  back() {
    this.router.navigate(['/maintain/standard-packing-quantity-setting']);
  }
  save() {
    if (this.router.url === '/maintain/standard-packing-quantity-setting/edit') {
      this.spinnerService.show();
      this.service.update(this.param).subscribe({
        next: result => {
          this.spinnerService.hide();
          if (result.isSuccess) {
            this.snotifyService.success(MessageConstants.UPDATED_OK_MSG, CaptionConstants.SUCCESS);
            this.back();
          }
          else {
            this.snotifyService.error(MessageConstants.UPDATED_ERROR_MSG, CaptionConstants.ERROR);
          }
        },
        error: err => {
          this.snotifyService.error(MessageConstants.UN_KNOWN_ERROR, CaptionConstants.ERROR);
        }
      })
    }
    else {
      this.spinnerService.show();
      this.service.create(this.param).subscribe({
        next: result => {
          this.spinnerService.show();
          if (result.isSuccess) {
            this.snotifyService.success(MessageConstants.CREATED_OK_MSG, CaptionConstants.SUCCESS);
            this.back();
          }
          else {
            this.snotifyService.error(MessageConstants.CREATED_ERROR_MSG, CaptionConstants.ERROR);
          }
        },
        error: err => {
          this.snotifyService.error(MessageConstants.UN_KNOWN_ERROR, CaptionConstants.ERROR);
        }
      })
    }
  }
}
