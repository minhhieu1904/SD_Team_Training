import { Component, OnInit } from '@angular/core';
import { InjectBase } from '@utilities/inject-base-app';
import { LoginUserService } from '@services/login/login-user.service';
import { CaptionConstants, MessageConstants } from '@constants/message.enum';
import { LocalStorageConstant } from '@constants/localStorge.constants';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent extends InjectBase implements OnInit {
  user: any = {};

  constructor(private service: LoginUserService) {
    super();
  }

  ngOnInit(): void {}

  login() {
    this.spinnerService.show();
    this.service.loginUser(this.user).subscribe({
      next: (result) => {
        console.log(this.user)
        localStorage.setItem(LocalStorageConstant.Token, result.token);
        localStorage.setItem(
          LocalStorageConstant.User,
          JSON.stringify(result.user)
        );
        localStorage.setItem(
          LocalStorageConstant.Role,
          JSON.stringify(result.user.roles)
        );
        this.snotifyService.success(
          MessageConstants.LOGGED_IN,
          CaptionConstants.SUCCESS
        );
        
        this.router.navigate(['default']);
        this.spinnerService.hide();
      },
      error: () => {
        this.snotifyService.error(
          MessageConstants.LOGIN_FAILED,
          CaptionConstants.ERROR
        );
        this.spinnerService.hide();
      },
    });
  }
}
