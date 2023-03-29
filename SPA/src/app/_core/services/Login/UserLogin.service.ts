import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { userLogin } from "../../../_core/models/userLogin";
@Injectable({
  providedIn: 'root'
})
export class UserLoginService {
  apiUrl: string = environment.apiUrl;
  constructor(private http: HttpClient) { }
  login(userLogin: userLogin) {
    return this.http.post<any>(this.apiUrl + "C_LoginUser/login", userLogin);
  }
  logout() {
    localStorage.clear();
    window.location.href = '/#/login';
    window.location.reload();
  }
}
