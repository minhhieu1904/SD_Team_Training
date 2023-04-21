import { Component, OnInit } from '@angular/core';
import { IconButton } from '@constants/common.constants';
import { CaptionConstants, MessageConstants } from '@constants/message.enum';
import { url } from '@constants/url.constants';
import { User } from '@models/maintain/user';
import { AuthorizationSettingService } from '@services/maintain/authorization-setting.service';
import { InjectBase } from '@utilities/inject-base-app';
import { KeyValuePair } from '@utilities/key-value-pair';
@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.scss'],
})
export class EditComponent extends InjectBase implements OnInit {
  //#region attribute
  iconButton = IconButton;
  actives: KeyValuePair[] = [
    { key: false, value: 'N' },
    { key: true, value: 'Y' },
  ];

  user: User = <User>{
    account: '',
    email: '',
    name: '',
    password: '',
    isActive: true,
  };
  //#endregion

  constructor(private service: AuthorizationSettingService) {
    super();
  }

  ngOnInit(): void {
    let account = '';
    this.route.params.subscribe((param) => {
      account = param['account'];
      this.getUser(account);
    });
  }

  //#region function
  back() {
    this.router.navigate([url.maintain.authorization_setting]);
  }

  getUser(account: string) {
    this.spinnerService.show();
    this.service.getDataOnly(account).subscribe({
      next: (result) => {
        this.spinnerService.hide();
        this.user = result;
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

  vadidate() {
    if (
      this.functionUtility.checkEmpty(this.user.account) ||
      this.functionUtility.checkEmpty(this.user.password) ||
      this.functionUtility.checkEmpty(this.user.email) ||
      this.functionUtility.checkEmpty(this.user.name)
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
    this.service.update(this.user).subscribe({
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
        this.snotifyService.error(
          this.translateService.instant('System.Message.SystemError'),
          this.translateService.instant('System.Caption.Error')
        );
      },
    });
  }
  //#endregion
}
