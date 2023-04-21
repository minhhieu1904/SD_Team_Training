import { Component, OnInit } from '@angular/core';
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
          MessageConstants.SYSTEM_ERROR_MSG,
          CaptionConstants.ERROR
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
        CaptionConstants.ERROR
      );

    this.spinnerService.show();
    this.service.update(this.user).subscribe({
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
      error: () => {
        this.snotifyService.error(
          MessageConstants.SYSTEM_ERROR_MSG,
          CaptionConstants.ERROR
        );
      },
    });
  }
  //#endregion
}
