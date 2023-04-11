import { Component, OnInit } from '@angular/core';
import { IconButton } from '@constants/common.constants';
import { CaptionConstants, MessageConstants } from '@constants/message.enum';
import { UserParam } from '@models/maintain/user';
import { AuthorizationSettingService } from '@services/authorization-setting.service';
import { InjectBase } from '@utilities/inject-base-app';

@Component({
  selector: 'app-user-form',
  templateUrl: './user-form.component.html',
  styleUrls: ['./user-form.component.scss'],
})
export class UserFormComponent extends InjectBase implements OnInit {
  constructor(private service: AuthorizationSettingService) {
    super();
  }
  title: string;
  icon = IconButton;
  param = <UserParam>{
    account: '',
    password: '',
    email: '',
    name: '',
    update_by: 'Admin',
    is_active: 0,
  };

  ngOnInit() {
    if (this.router.url === '/maintain/authorization-setting/edit') {
      this.title = 'Edit';
      this.spinnerService.show();

      this.service.dataSource.subscribe({
        next: (result) => {
          this.spinnerService.hide();
          this.param = result;
          if (result.is_active) {
            this.param.is_active = 1;
          }
          if (!result.is_active) {
            this.param.is_active = 0;
          }
        },
      });
    } else {
      this.title = 'Add';
    }
  }

  back() {
    this.router.navigate(['/maintain/authorization-setting/']);
  }

  save() {
    if (this.router.url === '/maintain/authorization-setting/edit') {
      this.spinnerService.show();

      this.service.update(this.param).subscribe({
        next: (result) => {
          this.spinnerService.hide();

          if (result.isSuccess) {
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
        error: (err) => {
          this.snotifyService.error(
            MessageConstants.UN_KNOWN_ERROR,
            CaptionConstants.ERROR
          );
        },
      });
    } else {
      this.spinnerService.show();
      this.service.create(this.param).subscribe({
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
        error: (err) => {
          this.snotifyService.error(
            MessageConstants.UN_KNOWN_ERROR,
            CaptionConstants.ERROR
          );
        },
      });
    }
  }
}
