import { Component, OnInit } from '@angular/core';
import { UserLoginParam } from '@models/auth/application-user';
import { AuthService } from './../../_core/services/auth/auth.service';
import { InjectBase } from '@utilities/inject-base-app';
import { CaptionConstants, MessageConstants } from '@constants/message.enum';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent extends InjectBase implements OnInit {
  user: UserLoginParam = <UserLoginParam>{};
  isDisabled: boolean = false;

  constructor(private service: AuthService) {
    super();
  }

  ngOnInit() {
    if (this.service.loggedIn) this.router.navigate(['/dashboard']);
  }

  login() {
    this.spinnerService.show();
    this.service.login(this.user).subscribe({
      next: (result) => {
        this.spinnerService.hide();
        this.snotifyService.success(
          MessageConstants.LOGGED_IN,
          CaptionConstants.SUCCESS
        );
      },
      error: (err) => {
        this.snotifyService.error(
          MessageConstants.LOGIN_FAILED,
          CaptionConstants.ERROR
        );
      },
      complete: () => {
        this.router.navigate(['/dashboard']);
        this.spinnerService.hide();
      },
    });
  }
}
