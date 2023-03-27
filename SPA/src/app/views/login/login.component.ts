import { Component, OnInit } from "@angular/core";
import { UserLoginParam } from '@models/auth/application-user'
import { AuthorService } from "../../_core/services/auth/auth.service";
import { InjectBase } from "@utilities/inject-base-app";
@Component({
  selector: "app-dashboard",
  templateUrl: "login.component.html",
  styleUrls: ["./login.component.scss"],
})
export class LoginComponent extends InjectBase implements OnInit {
  user: UserLoginParam = <UserLoginParam>{};

  constructor(
    private authService: AuthorService
  ) {
    super();
  }

  ngOnInit() {
    // if (this.authService.loggedIn) this.router.navigate(["/dashboard"]);
  }

  login() {
    this.spinnerService.show();
    this.authService.login(this.user).subscribe({
      next: () => {
        this.snotifyService.success(
          this.translateService.instant('System.Message.LogIn'),
          this.translateService.instant('System.Caption.Success'));
        this.spinnerService.hide();
      },
      error: () => {
        this.snotifyService.error(
          this.translateService.instant('System.Message.LogInFailed'),
          this.translateService.instant('System.Caption.Error'));
        this.spinnerService.hide();
      },
      complete: () => {
        this.router.navigate(["/dashboard"]);
        this.spinnerService.hide();
      }
    })
  }
}
