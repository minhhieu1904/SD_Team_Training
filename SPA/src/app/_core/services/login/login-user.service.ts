import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { UserLogin } from '../../models/login/userLogin';
import { environment } from '../../../../environments/environment';
@Injectable({
  providedIn: 'root',
})
export class LoginUserService {
  constructor(private http: HttpClient) {}
  apiUrl: string = environment.apiUrl + 'C_Login';

  loginUser(userLogin: UserLogin) {
    return this.http.post<any>(`${this.apiUrl}/login`, userLogin);
  }
}
