import { Component, OnInit } from '@angular/core';
import { InjectBase } from '@utilities/inject-base-app';
import { CaptionConstants, MessageConstants } from '@constants/message.enum';
import { AuthService } from "@services/auth/auth.service";
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent extends InjectBase implements OnInit {
  user: any = {};

  constructor(private authService: AuthService) {
    super();
  }

  ngOnInit(): void {
    // Nếu đã đăng nhập thì tự động chuyển về dashboard
    if (this.authService.loggedIn) this.router.navigate(['/dashboard']);
  }

  login() {
    this.spinnerService.show();
    this.authService.login(this.user).subscribe({
      next: () => {
        this.spinnerService.hide();
        this.snotifyService.success(
          MessageConstants.LOGGED_IN,
          CaptionConstants.SUCCESS
        );
      },
      error: () => {
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
