import { environment } from './../../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
@Injectable({
  providedIn: 'root'
})
export class CommonService {
  apiUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }
}
