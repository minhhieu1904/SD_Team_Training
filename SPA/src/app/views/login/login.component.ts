import { LocalStorageConstants } from '@constants/local-storage.constants';
import { AuthService } from './../../_core/services/auth/auth.service';
import { CaptionConstants, MessageConstants } from './../../_core/constants/message.enum';
import { UserLoginParam } from './../../_core/models/auth/application-user';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgSnotifyService } from '@services/common/ng-snotify.service';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent  implements OnInit {
  user: any = {};
  appUser: UserLoginParam = <UserLoginParam>{};
  isDisabled: boolean = false;
  constructor(
    private authService: AuthService,
    private router: Router,
    private snotifyService: NgSnotifyService,
    private spinnerService: NgxSpinnerService,
  ) {}

  ngOnInit() {
    if (this.authService.loggedIn) this.router.navigate(["/dashboard"]);
  }

  login() {
    this.spinnerService.show();
    this.authService.login(this.user).subscribe({
      next: () => {

          this.snotifyService.success(
            MessageConstants.LOGGED_IN,CaptionConstants.SUCCESS);
          this.spinnerService.hide();
      },
      error: (res) => {
        console.log("err: ",res);
        this.snotifyService.error(MessageConstants.LOGIN_FAILED, CaptionConstants.ERROR);
        this.spinnerService.hide();
      },
      complete : () => {
        this.router.navigate(["/dashboard"])
      }
    });
  }
}
