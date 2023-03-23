import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseUrl = environment.checkTokenUrl;

constructor(private http: HttpClient) { }

checkLogin(token: string) {
  return this.http.get(this.baseUrl + token)
}

}


