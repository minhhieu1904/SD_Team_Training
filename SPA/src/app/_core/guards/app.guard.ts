import { Injectable } from '@angular/core';
import { CanLoad, Route, Router, UrlTree } from '@angular/router';
import {LocalStorageConstant} from "@constants/localStorge.constants";
import {RoleInfomation} from "@models/auth/auth";
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AppGuard implements CanLoad {
  constructor(private router: Router) { }
  canLoad(route: Route): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    let unique = route.data['unique'];
    let roleOfUser: RoleInfomation[] = JSON.parse(localStorage.getItem(LocalStorageConstant.Role));
    let checkRole = roleOfUser.map(x => x.unique).some(x => x.trim() === unique?.trim());
    if (checkRole) {
      return true;
    } else {
      this.router.navigate(['/default']);
      return false;
    }
  }
}
