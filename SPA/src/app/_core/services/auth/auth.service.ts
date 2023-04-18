import { Injectable } from '@angular/core';
import { environment } from './../../../../environments/environment';
import { JwtHelperService } from '@auth0/angular-jwt';
import { HttpClient } from '@angular/common/http';
import {
  UserLoginParam,
  UserForLogged,
} from './../../models/auth/application-user';
import { map } from 'rxjs';
import { LocalStorageConstants } from './../../constants/local-storage.constants';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  apiUrl = environment.apiUrl;
  jwtHelper = new JwtHelperService();
  currentUser: UserForLogged | null = <UserForLogged>{};

  constructor(private http: HttpClient) {}
  decodedToken: any;

  login(model: UserLoginParam) {
    return this.http.post(this.apiUrl + 'Author/Login', model).pipe(
      map((reponse: any) => {
        const user = reponse;
        if (user) {
          localStorage.setItem(LocalStorageConstants.TOKEN, user.token);
          localStorage.setItem(
            LocalStorageConstants.USER,
            JSON.stringify(user.user)
          );
          localStorage.setItem(
            LocalStorageConstants.ROLES,
            JSON.stringify(user.user.roles)
          );
          localStorage.setItem(
            LocalStorageConstants.ROLE_ALL,
            JSON.stringify(user.user.roleAll)
          );
          this.decodedToken = this.jwtHelper.decodeToken(user.token);
        }
      })
    );
  }

  // getUser(): UserLoginParam {
  //   return JSON.parse(localStorage.getItem(LocalStorageConstants.USER));
  // }

  loggedIn() {
    const token: string = localStorage.getItem(LocalStorageConstants.TOKEN);
    const roles: string[] = JSON.parse(
      localStorage.getItem(LocalStorageConstants.ROLES)
    );
    const user: UserForLogged = JSON.parse(
      localStorage.getItem(LocalStorageConstants.USER)
    );
    return !(!user || !roles) || !this.jwtHelper.isTokenExpired(token);
  }

  logout() {
    localStorage.clear();
    window.location.href = '/#/login';
    window.location.reload();
  }
}
