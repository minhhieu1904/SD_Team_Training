import { AuthService } from '@services/auth/auth.service';
import { CaptionConstants, MessageConstants } from '@constants/message.enum';
import { UserparamLogin } from '@models/auth/application-user';
import { Component, OnInit } from '@angular/core';
import { InjectBase } from '@utilities/inject-base-app';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent extends InjectBase  implements OnInit {
  user: UserparamLogin = <UserparamLogin>{};
  isDisabled: boolean = false;
  constructor(
    private authService: AuthService,
  ) { super();}

  ngOnInit() {
    if (this.authService.loggedIn) this.router.navigate(["/dashboard"]);
  }

  login() {
    this.spinnerService.show();
    this.authService.login(this.user).subscribe({
      next: () => {
        console.log(this.user);

        this.snotifyService.success(
          MessageConstants.LOGGED_IN,
          CaptionConstants.SUCCESS);
        this.spinnerService.hide();
      },
      error: () => {
        this.snotifyService.error(MessageConstants.LOGIN_FAILED, CaptionConstants.ERROR);
        this.spinnerService.hide();
      },
      complete: () => {
        this.router.navigate(["/dashboard"]);
        this.spinnerService.hide();
      }
    });
  }
}
