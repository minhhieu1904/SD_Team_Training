import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { LocalStorageConstants } from '@constants/local-storage.constants';
import { User } from '@models/auth/application-user';
import { AuthorService } from '@services/auth/auth.service'
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  user: any = {};
  appUser: User = <User>{};
  isDisable: boolean = false;
  constructor(
    private services: AuthorService,
    private router: Router,
  ) { }


  ngOnInit(): void {
    if (this.services.loggedIn) this.router.navigate(["/dashboard"]);
  }

  login() {
    console.log(this.user);
    this.isDisable = true;
    this.services.login(this.user).subscribe({
      next: result => {
        debugger
        console.log("Res: ", result);
        if (result != null) {
          localStorage.setItem(LocalStorageConstants.USER, JSON.stringify(result.user.account));
          localStorage.setItem(LocalStorageConstants.ROLES, JSON.stringify(result.user.roles));
          this.router.navigate(['/']);
        } else {
          console.log('login');
        }
      },
      error: err => {
        console.log(err);
      }
    });
  }

}
