import { ReprintStickerModel, ReprintStickerParam } from './../../models/transaction/reprint-sticker';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@env/environment';
import { Pagination, PaginationParam, PaginationResult } from '@utilities/pagination-utility';

@Injectable({
  providedIn: 'root'
})
export class ReprintStickerService {
  apiUrl: string = environment.apiUrl;
  constructor(private http: HttpClient) { }

  getData(pagination: PaginationParam, filterParam: ReprintStickerParam) {
    let params = new HttpParams().appendAll({ ...pagination, ...filterParam });
    return this.http.post<PaginationResult<ReprintStickerModel>>(`${this.apiUrl}C_ReprintSticker/GetData`, { params })
  }

  updateData(model: ReprintStickerModel[]) {
    return this.http.post<boolean>(`${this.apiUrl}C_ReprintSticker/UpdateData`, model);
  }

  getDataByScan(model: ReprintStickerModel[]) {
    return this.http.post<ReprintStickerModel[]>(`${this.apiUrl}C_ReprintSticker/GetDataByScan`, model);
  }
}
