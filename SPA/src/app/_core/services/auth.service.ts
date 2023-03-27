import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { userLogin } from '../models/userLogin';
import { OperationResult } from '../utilities/operation-result';
@Injectable({
  providedIn: 'root'
})
export class AuthService {
  apiUrl:string = environment.apiUrl;
constructor(private http:HttpClient) { }
  login(userLogin: userLogin){
    return this.http.post<any>(this.apiUrl+"C_Author/login",userLogin);
  }

}
