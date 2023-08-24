import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs';
import { environment } from '@env/environment';
import { LocalStorageConstants } from '@constants/local-storage.constants';
import { UserForLogged, UserLoginParam } from '@models/auth/application-user';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Router } from '@angular/router';
import { LangConstants } from '@constants/lang-constants';


@Injectable({
  providedIn: 'root'
})
export class AuthService {
  apiUrl = environment.apiUrl;
  jwtHelper = new JwtHelperService();
  currentUser: UserForLogged | null = <UserForLogged>{};
  decodedToken: any;
  constructor(private http: HttpClient,
    private router: Router) { }

  login(param: UserLoginParam) {
    return this.http.post(this.apiUrl + "Auth/login", param).pipe(
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

  logout() {
    const lang: string = localStorage.getItem(LocalStorageConstants.LANG);
    localStorage.clear();
    localStorage.setItem(LocalStorageConstants.LANG, lang ?? LangConstants.VI);
    this.router.navigate(['/login']);
  }

  loggedIn() {
    const token: string = localStorage.getItem(LocalStorageConstants.TOKEN);
    const user: UserForLogged = JSON.parse(localStorage.getItem(LocalStorageConstants.USER));
    const roles: string[] = JSON.parse(localStorage.getItem(LocalStorageConstants.ROLES));
    return !(!user || !roles) || !this.jwtHelper.isTokenExpired(token);
  }
}
