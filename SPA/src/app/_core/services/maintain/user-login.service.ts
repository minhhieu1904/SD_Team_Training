import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { userLogin } from '../../models/maintain/userLogin';
import { UserLoginParam } from '../../models/maintain/application-user';
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
  decodedToken : any;
  login(model: userLogin) {
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
