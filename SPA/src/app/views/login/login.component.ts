import { LocalStorageConstants } from '@constants/localStorge.constants';
import { UserLoginService } from '@services/login/user-login.service';
import {
  CaptionConstants,
  MessageConstants,
} from '@constants/message.enum';
import { User } from '@models/auth/auth';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgSnotifyService } from '@services/common/ng-snotify.service';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit {
  user: any = {};
  appUser: User = <User>{};
  isDisabled: boolean = false;
  constructor(
    private service: UserLoginService,
    private router: Router,
    private snotifyService: NgSnotifyService,
    private spinnerService: NgxSpinnerService
  ) {}

  ngOnInit() {
    // if (this.service.loggedIn) this.router.navigate(["/dashboard"]);
  }

  login() {
    this.spinnerService.show();
    this.service.login(this.user).subscribe({
      next: () => {
        this.snotifyService.success(MessageConstants.LOGGED_IN, CaptionConstants.SUCCESS);
        this.spinnerService.hide();
      },
      error: () => {
        this.snotifyService.error(MessageConstants.LOGIN_FAILED, CaptionConstants.ERROR);
        this.spinnerService.hide();
      },
      complete: () => {
        this.router.navigate(['/dashboard']);
        this.spinnerService.hide();
      },
    });
  }
}
