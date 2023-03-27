import { Component, OnInit } from '@angular/core';
import { IconButton } from '@constants/sd-team.utility';
import { Users } from '@models/common/users';
import { AuthorizationSettingService } from '../../../../_core/services/main/authorization-setting.service';
import { InjectBase } from '@utilities/inject-base-app';

@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.scss']
})
export class FormComponent extends InjectBase implements OnInit {
  user: Users = <Users>{ is_active: true };
  iconButton = IconButton;

  constructor(
    private authorSettingService: AuthorizationSettingService
  ) {
    super();
  }

  ngOnInit() {
    this.authorSettingService.shareDataSubject.subscribe(receiveddata => {
      if (receiveddata) {
        this.user = { ...receiveddata };
      }
      else this.back();
    });
  }

  back() {
    this.router.navigate(['maintain/authorization-setting']);
  }

  save(type?: string) {
    if (this.user.type == 'add') {
      this.authorSettingService.createUser(this.user).subscribe({
        next: res => {
          if (res.isSuccess) {
            this.snotifyService.success(
              this.translateService.instant('System.Message.CreateOKMsg'),
              this.translateService.instant('System.Caption.Success')
            );
            if (type == 'next') {
              this.user.account = '';
              this.user.password = '';
              this.user.email = '';
              this.user.name = '';
              this.user.is_active = true;
            } else this.back();
          } else {
            this.snotifyService.error(
              this.translateService.instant('System.Message.CreateErrorMsg'),
              this.translateService.instant('System.Caption.Error')
            );
          }
        }, error: error => {
          this.snotifyService.error(
            this.translateService.instant('System.Message.UnknowError'),
            this.translateService.instant('System.Caption.Error')
          );
        }
      });
    } else {
      this.authorSettingService.updateUser(this.user).subscribe({
        next: res => {
          if (res.isSuccess) {
            this.snotifyService.success(
              this.translateService.instant('System.Message.UpdateOKMsg'),
              this.translateService.instant('System.Caption.Success')
            );
            this.back();
          } else this.snotifyService.error(
            this.translateService.instant('System.Message.UpdateErrorMsg'),
            this.translateService.instant('System.Caption.Error')
          );
        }, error: error => {
          this.snotifyService.error(
            this.translateService.instant('System.Message.UnknowError'),
            this.translateService.instant('System.Caption.Error')
          );
        }
      });
    }
  }
}

