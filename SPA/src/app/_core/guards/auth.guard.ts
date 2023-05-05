import { Injectable } from '@angular/core';
import { UserLoginService } from '@services/login/user-login.service';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard {
  constructor(private service: UserLoginService, private router: Router) { }
  canActivate(): boolean {
    if (this.service.loggedIn()) {
      return true;
    }
    this.router.navigate(["/login"]);
    return false;
  }
}

