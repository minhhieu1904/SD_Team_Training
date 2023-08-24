import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {environment} from "@env/environment";
import {Factory} from "@models/common/factory";

@Injectable({
  providedIn: 'root',
})
export class CommonService {
  apiUrl = environment.apiUrl;
  constructor(private http: HttpClient) {}
  getFactory() {
    return this.http.get<Factory>(`${this.apiUrl}Common/getFactory`);
  }
}
