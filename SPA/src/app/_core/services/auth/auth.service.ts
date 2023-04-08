
import { environment } from '@env/environment';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {  UserLoginParam, Userparam } from '@models/auth/application-user';
import { LocalStorageConstants } from '@constants/local-storage.constants';
import { map } from 'rxjs';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  baseUrl = environment.apiUrl + 'C_Auth/';

  jwtHelper = new JwtHelperService();
  constructor(private http: HttpClient
            ) { }
  decodedToken : any;
  login(model: Userparam) {
    return this.http.post(this.baseUrl +'login', model).pipe(
      map((response : any) =>{
        const user = response;
        if(user){
          localStorage.setItem(LocalStorageConstants.TOKEN, user.token);
          localStorage.setItem(LocalStorageConstants.USER, JSON.stringify(user.user));
          localStorage.setItem(LocalStorageConstants.ROLES, JSON.stringify(user.user.roles));
          this.decodedToken = this.jwtHelper.decodeToken(user.token);
        }
      } )
    );
  }
  getUser():UserLoginParam {
    return JSON.parse(localStorage.getItem(LocalStorageConstants.USER));
  }


  loggedIn() {
    const token : string = localStorage.getItem(LocalStorageConstants.TOKEN);
    const roles: string[] = JSON.parse(localStorage.getItem(LocalStorageConstants.ROLES));
    const user: UserLoginParam =  JSON.parse(localStorage.getItem(LocalStorageConstants.USER));
    return !(!user || !roles) || !this.jwtHelper.isTokenExpired(token);
  }
  logout() {
    localStorage.clear();
    window.location.href = '/#/login';
    window.location.reload();
  }


}
