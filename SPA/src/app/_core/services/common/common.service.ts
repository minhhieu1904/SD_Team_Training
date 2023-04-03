import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {environment} from "@env/environment";


@Injectable({
  providedIn: 'root'
})
export class CommonService {
  apiUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

}
