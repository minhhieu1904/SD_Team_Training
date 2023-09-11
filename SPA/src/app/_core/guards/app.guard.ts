import { Injectable } from '@angular/core';
import { Route, Router, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { RoleInformation } from '../models/auth/application-user';
import { LocalStorageConstants } from '../constants/local-storage.constants';

@Injectable({
  providedIn: 'root'
})

export class AppGuard  {
  constructor(private router: Router) { }

  canLoad(route: Route): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    let role = route.data['role'];
    let roleOfUser: RoleInformation[] = JSON.parse(localStorage.getItem(LocalStorageConstants.ROLES));
    let checkRole = roleOfUser.map(x => x.unique).some(x => x.trim() === role?.trim());

    if (checkRole) {
      return true;
    } else {
      this.router.navigate(['/dashboard']);
      return false;
    }
  }

}
