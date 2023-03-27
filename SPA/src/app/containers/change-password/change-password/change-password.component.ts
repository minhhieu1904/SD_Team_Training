import { AuthService } from '@services/auth/auth.service';
import { NgSnotifyService } from './../../../_core/services/common/ng-snotify.service';
import { ChangePassword } from './../../../_core/models/auth/change-password';

import { CaptionConstants } from './../../../_core/constants/message.enum';

import { UserLoginParam } from './../../../_core/models/auth/application-user';
import { IconButton } from './../../../_core/constants/common.constants';


import { Component, OnInit } from '@angular/core';

import { BsModalRef } from 'ngx-bootstrap/modal';

import { NgxSpinnerService } from 'ngx-spinner';
import { LocalStorageConstants } from '@constants/local-storage.constants';


@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.scss']
})
export class ChangePasswordComponent implements OnInit {
  iconButton: typeof IconButton = IconButton;
  user: UserLoginParam = JSON.parse(localStorage.getItem(LocalStorageConstants.USER)) ?? '{}';
  param: ChangePassword = <ChangePassword>{};
  confirmPassword: string = ''

  constructor(
    public bsModalRef: BsModalRef,
    private snotifyService: NgSnotifyService,
    private spinnerService: NgxSpinnerService,
    private authService: AuthService,
  ) { }

  ngOnInit() {
  }

  clearForm() {
    this.param.account = '',
    this.param.old_password = '',
    this.param.new_password = '',
    this.confirmPassword = ''
  }

  changePassword() {
    debugger;
    this.param.account = this.user.account;
    this.spinnerService.show();
    this.authService.changePassword(this.param).subscribe({
      next: res => {
        this.spinnerService.hide();
        if (res.isSuccess) {
          this.snotifyService.success(res.data, CaptionConstants.SUCCESS);
          this.clearForm();
          this.bsModalRef.hide();

        } else this.snotifyService.error(res.data, CaptionConstants.ERROR);

      },
      error: () => {
        this.snotifyService.error('Change Password Failed! Please try again', 'Error');
        this.spinnerService.hide();
      },
      complete: () => this.spinnerService.hide()
    });
  }

  cancel() {
    this.bsModalRef.hide();
  }
}
