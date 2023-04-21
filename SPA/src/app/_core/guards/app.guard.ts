import { Injectable } from '@angular/core';
import { CanLoad, Route, Router, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { RoleInformation } from '@models/auth/auth';
import { LocalStorageConstant } from '@constants/localStorge.constants';

@Injectable({
  providedIn: 'root'
})

export class AppGuard implements CanLoad {
  constructor(private router: Router) { }

  canLoad(route: Route): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    debugger
    let role = route.data['role'];
    let roleOfUser: RoleInformation[] = JSON.parse(localStorage.getItem(LocalStorageConstant.Role));
    let checkRole = roleOfUser.map(x => x.unique).some(x => x.trim() === role?.trim());

    if (checkRole) {
      return true;
    } else {
      this.router.navigate(['/']);
      return false;
    }
  }

}
