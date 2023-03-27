import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { UserForLogged, UserLoginParam } from '@models/auth/application-user'
import { LocalStorageConstants } from '@constants/local-storage.constants'
import { environment } from '@env/environment';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Router } from '@angular/router';
import { map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthorService {
  baseUrl: string = environment.apiUrl + 'Auth/';
  jwtHelper = new JwtHelperService();
  currentUser: UserForLogged | null = <UserForLogged>{};
  decodedToken: any;
  constructor(private _http: HttpClient,
    private router: Router) { }

  login(param: UserLoginParam) {
    return this._http.post(this.baseUrl + 'login', param).pipe(
      map((response: any) => {
        const user = response;
        if (user) {
          localStorage.setItem(LocalStorageConstants.TOKEN, user.token);
          localStorage.setItem(LocalStorageConstants.USER, JSON.stringify(user.user));
          localStorage.setItem(LocalStorageConstants.ROLES, JSON.stringify(user.user.roles));
          this.decodedToken = this.jwtHelper.decodeToken(user.token);
          this.currentUser = user.user;
        }
      })
    );
  }

  loggedIn() {
    const token: string = localStorage.getItem(LocalStorageConstants.TOKEN);
    const roles: string[] = JSON.parse(localStorage.getItem(LocalStorageConstants.ROLES));
    const user: UserLoginParam = JSON.parse(localStorage.getItem(LocalStorageConstants.USER));
    return !(!user || !roles) || !this.jwtHelper.isTokenExpired(token);
  }

  logout() {
    localStorage.clear();
    window.location.href = '/#/login';
    window.location.reload();
  }

}
