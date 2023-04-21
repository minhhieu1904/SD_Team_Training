import { Injectable } from '@angular/core';
import { environment } from '@env/environment';
import { JwtHelperService } from '@auth0/angular-jwt';
import { map } from 'rxjs/operators';
import { LocalStorageConstant } from '@constants/localStorge.constants';
import { HttpClient } from '@angular/common/http';
import { User, UserLogin } from '@models/auth/auth';
import { Router } from '@angular/router';
import { LangConstant } from "@constants/lang.constants";
@Injectable({
  providedIn: 'root',
})
export class AuthService {
  apiUrl = environment.apiUrl;
  jwtHelper = new JwtHelperService();
  decodedToken: any;
  constructor(private http: HttpClient, private router: Router) {}

  login(param: UserLogin) {
    return this.http.post(this.apiUrl + 'C_Login/login', param).pipe(
      map((response: any) => {
        const user = response;
        if (user) {
          localStorage.setItem(LocalStorageConstant.Token, user.token);
          localStorage.setItem(
            LocalStorageConstant.User,
            JSON.stringify(user.user)
          );
          localStorage.setItem(
            LocalStorageConstant.Role,
            JSON.stringify(user.user.roles)
          );
          localStorage.setItem(
            LocalStorageConstant.RoleAll,
            JSON.stringify(user.user.roleAll)
          );
          localStorage.setItem(
            LocalStorageConstant.Lang,
            LangConstant.EN
          )
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
    const user: User = JSON.parse(
      localStorage.getItem(LocalStorageConstant.User)
    );
    const roles: string[] = JSON.parse(
      localStorage.getItem(LocalStorageConstant.Role)
    );
    return !(!user || !roles) || !this.jwtHelper.isTokenExpired(token);
  }
}
