import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Pagination, PaginationResult } from '@utilities/pagination-utility';
import {MSLocationParam, MS_Location} from '../models/mS_Location'
@Injectable({
  providedIn: 'root'
})
export class MsLocationService {
  baseUrl: string = 'https://localhost:5001/MSLocation/';
  constructor(private _htp: HttpClient) { }

  getData(pagination: Pagination, param: MSLocationParam) {
    let params = new HttpParams().appendAll({...pagination, ...param});
    return this._htp.get<PaginationResult<MS_Location>>(this.baseUrl + 'GetDataPagination', {params});
  }

}
