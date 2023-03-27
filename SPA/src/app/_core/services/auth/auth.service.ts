import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApplicationUser } from '@models/auth/application-user'
import { LocalStorageConstants } from '@constants/local-storage.constants'
import { environment } from '@env/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthorService {
  baseUrl: string =  environment.apiUrl +'Auth/';

  constructor(private _http: HttpClient) { }

  login(model: any) {
    return this._http.post<ApplicationUser>(this.baseUrl + 'login', model);
  }

  loggedIn() {
    const user: ApplicationUser = JSON.parse(localStorage.getItem(LocalStorageConstants.USER));
    return user ? true : false;
  }


}
