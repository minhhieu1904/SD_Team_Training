import { HttpClient } from '@angular/common/http';
import { environment } from '@env/environment';
import { Injectable } from '@angular/core';
import { ApplicationUser, userLogin } from '../models/userLogin';

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  apiUrl:string = environment.apiUrl ;
  constructor(private http: HttpClient) {}
  login(param : any){
    return this.http.post<ApplicationUser>(this.apiUrl + 'C_Login/Login', param)
  }
}
