import { Component, OnInit } from '@angular/core';
import { IconButton } from '@constants/common.constants';
import { LocalStorageConstants } from '@constants/local-storage.constants';
import { CaptionConstants } from '@constants/message.enum';
import { updateUsers, UserLoginParam } from '@models/auth/application-user';
import { AuthService } from '@services/auth/auth.service';
import { NgSnotifyService } from '@services/common/ng-snotify.service';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-update-info',
  templateUrl: './update-info.component.html',
  styleUrls: ['./update-info.component.scss']
})
export class UpdateInfoComponent implements OnInit {

  iconButton: typeof IconButton = IconButton;
  user: UserLoginParam = JSON.parse(localStorage.getItem(LocalStorageConstants.USER)) ?? '{}';
  param: updateUsers = <updateUsers>{};


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
    this.param.email = '',
    this.param.name = ''
  }

  updateUser() {
    this.param.account = this.user.account;
    this.spinnerService.show();
    this.authService.upDate(this.param).subscribe({
      next: res => {
        this.spinnerService.hide();
        if (res.isSuccess) {
          this.snotifyService.success(res.data, CaptionConstants.SUCCESS);
          this.clearForm();
          this.bsModalRef.hide();
        } else this.snotifyService.error(res.data, CaptionConstants.ERROR);

      },
      error: () => {
        this.snotifyService.error('Change information Failed! Please try again', 'Error');
        this.spinnerService.hide();
      },
      complete: () => this.spinnerService.hide()
    });
  }

  cancel() {
    this.bsModalRef.hide();
  }

}
