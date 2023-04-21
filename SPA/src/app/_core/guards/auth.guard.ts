import { Injectable } from '@angular/core';

import { CanLoad, Route, Router, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { RoleInformation } from '@models/auth/auth';
import { LocalStorageConstant } from '@constants/localStorge.constants';
import { AuthService } from '@services/auth/auth.service';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanLoad {
  constructor(private authService: AuthService, private router: Router) {}

  canLoad(
    route: Route
  ):
    | Observable<boolean | UrlTree>
    | Promise<boolean | UrlTree>
    | boolean
    | UrlTree {
    let role = route.data['role'];
    let roleOfUser: RoleInformation[] = JSON.parse(
      localStorage.getItem(LocalStorageConstant.Role)
    );
    let checkRole = roleOfUser
      .map((x) => x.unique)
      .some((x) => x.trim() === role?.trim());
    console.log(role);

    if (checkRole) {
      return true;
    } else {
      this.router.navigate(['/login']);
      return false;
    }
  }
  canActivate(): boolean {
    if (this.authService.loggedIn()) {
      return true;
    } else {
      this.router.navigate(['/login']);
      return false;
    }
  }
}
