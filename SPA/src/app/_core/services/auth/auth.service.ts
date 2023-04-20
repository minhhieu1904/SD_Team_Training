
import { environment } from '../../../../environments/environment';
import {JwtHelperService} from '@auth0/angular-jwt';
import {UserForLogged, UserLoginParam} from '../../../_core/models/auth/auth';
import {HttpClient} from '@angular/common/http';
import { Router } from '@angular/router';
import {map} from 'rxjs/operators';
import {LocalStorageConstant} from '../../constants/localStorge.constants';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  apiUrl = environment.apiUrl;
  jwtHelper = new JwtHelperService();
  decodedToken: any;
  constructor(private  http: HttpClient,
              private  router: Router) { }
  login(param: UserLoginParam) {
    return this.http.post(this.apiUrl + 'auth/login', param).pipe(
      map((response: any) => {
        const user = response;
        if (user) {
          localStorage.setItem(LocalStorageConstant.Token, user.token);
          localStorage.setItem(LocalStorageConstant.User, JSON.stringify(user.user));
          localStorage.setItem(LocalStorageConstant.Role, JSON.stringify(user.user.roles));
          localStorage.setItem(LocalStorageConstant.Role_All, JSON.stringify(user.user.roleAll));
          this.decodedToken = this.jwtHelper.decodeToken(user.token);
        }
      })
    );
  }

  logout() {
    localStorage.clear();
    this.router.navigate(['/login']);
  }

  loggedIn() {
    const token: string = localStorage.getItem(LocalStorageConstant.Token);
    const user: UserForLogged = JSON.parse(localStorage.getItem(LocalStorageConstant.User));
    const roles: string[] = JSON.parse(localStorage.getItem(LocalStorageConstant.Role));
    return !(!user || !roles) || !this.jwtHelper.isTokenExpired(token);
  }
}
