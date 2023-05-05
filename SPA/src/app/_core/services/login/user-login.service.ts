import { Injectable } from '@angular/core';
import { environment } from '@env/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { UserLogin, User } from '@models/auth/auth';
import { JwtHelperService } from '@auth0/angular-jwt';
import { LocalStorageConstants } from '@constants/localStorge.constants';
import { map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserLoginService {
  apiUrl: string = environment.apiUrl + 'C_Login/';
  jwtHelper = new JwtHelperService();
  constructor(private http: HttpClient
            ) { }
  decodedToken : any
  login(model: UserLogin) {
    return this.http.post(this.apiUrl + 'Login', model).pipe(
      map((response : any) =>{
        const user = response;
        if(user){

          localStorage.setItem(LocalStorageConstants.TOKEN, user.token);
          localStorage.setItem(LocalStorageConstants.USER, JSON.stringify(user.user));
          localStorage.setItem(LocalStorageConstants.ROLES, JSON.stringify(user.user.roles));
          localStorage.setItem(LocalStorageConstants.ROLE_ALL, JSON.stringify(user.user.roleAll));
          this.decodedToken = this.jwtHelper.decodeToken(user.token);
        }
      } )
    );
  }
  getUser():User {
    return JSON.parse(localStorage.getItem(LocalStorageConstants.USER));
  }
  loggedIn() {
    const token : string = localStorage.getItem(LocalStorageConstants.TOKEN);
    const roles: string[] = JSON.parse(localStorage.getItem(LocalStorageConstants.ROLES));
    const user: User =  JSON.parse(localStorage.getItem(LocalStorageConstants.USER));
    return !(!user || !roles) || !this.jwtHelper.isTokenExpired(token);
  }
  logout() {
    localStorage.clear();
    window.location.href = '/#/login';
    window.location.reload();
  }

}
