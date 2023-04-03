import { HttpClient } from '@angular/common/http';
import { environment } from './../../../environments/environment';
import { Injectable } from '@angular/core';
import { ApplicationUser, userLogin } from '../models/userLogin';

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  baseUrl: string = 'https://localhost:5001/C_Login/' ;
  constructor(private http: HttpClient) {}
  login(param : any){
    return this.http.post<ApplicationUser>(this.baseUrl + 'Login', param)
  }
}
