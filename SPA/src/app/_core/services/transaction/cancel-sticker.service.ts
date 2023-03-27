import { CancelStickerParam } from './../../_helpers/params/transaction/cancelStickerParam';
import { CancelStickerViewModel } from './../../models/transaction/cancel-sticker';
import { OperationResult } from './../../utilities/operation-result';
import { FunctionUtility } from './../../utilities/function-utility';
import { HttpClient } from '@angular/common/http';
import { environment } from './../../../../environments/environment';
import { Injectable } from '@angular/core';
import { Pagination, PaginationResult } from '@utilities/pagination-utility';

@Injectable({
  providedIn: 'root'
})
export class CancelStickerService {
  baseUrl: string = environment.apiUrl;

  constructor(private http: HttpClient,
    private functionUtility: FunctionUtility) { }

  checkRecordScan(qrCodeValue: string) {
    return this.http.get<OperationResult>(`${this.baseUrl}C_CancelSticker/CheckRecordScan`, { params: { qrCodeValue } });
  }

  cancelStickerAction(datas: string[]) {
    return this.http.put<OperationResult>(`${this.baseUrl}C_CancelSticker/CancelStickerAction`, datas);
  }

  cancelStickerScan(datas: string[]) {
    return this.http.put<OperationResult>(`${this.baseUrl}C_CancelSticker/CancelStickerScan`, datas);
  }

  getDataPagination(pagination: Pagination, param: CancelStickerParam) {
    if (!param.serial)
      delete (param.serial);

    let params = this.functionUtility.ToParams(param).set('PageNumber', pagination.pageNumber).set('PageSize', pagination.pageSize);
    return this.http.get<PaginationResult<CancelStickerViewModel>>(`${this.baseUrl}C_CancelSticker/GetDataPagination`, { params: params });
  }
}
