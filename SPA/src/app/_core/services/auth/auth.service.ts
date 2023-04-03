import { OperationResult } from './../../utilities/operation-result';
import { ChangePassword } from './../../models/auth/change-password';

import { environment } from '@env/environment';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {  updateUsers, UserLoginParam, Userparam } from '../../models/auth/application-user';
import { LocalStorageConstants } from '@constants/local-storage.constants';
import { BehaviorSubject, map } from 'rxjs';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  baseUrl = environment.apiUrl + 'C_Auth/';
  dataSources = new BehaviorSubject<updateUsers>(null);
  currentDataSource = this.dataSources.asObservable();

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

  changePassword(model: ChangePassword) {
    return this.http.put<OperationResult>(this.baseUrl +'changespassword', model);
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

  upDate(params: updateUsers) {
    return this.http.put<OperationResult>(
      this.baseUrl + 'updateuser',
      params
    );
  }
}
