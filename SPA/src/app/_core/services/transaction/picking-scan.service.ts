import { OperationResult } from '@utilities/operation-result';
import { GetScanPickingMainDto, PickingMainDto, PickingUpdate } from './../../models/transaction/picking-scan';
import { PaginationParam, PaginationResult } from './../../utilities/pagination-utility';
import { HttpClient, HttpParams } from '@angular/common/http';
import { PickingScanParam, PickingScanSource } from '../../_helpers/params/transaction/pickingScanParam';
import { environment } from '@env/environment';
import { Injectable, signal } from '@angular/core';
import { toObservable } from '@angular/core/rxjs-interop';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PickingScanService {
  baseUrl = `${environment.apiUrl}C_PickingScan/`;
  scanPickingSource = new BehaviorSubject<PickingScanSource>(null);
  currentPrintQrCode = this.scanPickingSource.asObservable();
  constructor(
    private http: HttpClient
  ) { }

  getData(pagination: PaginationParam, filterParam: PickingScanParam): Observable<PaginationResult<PickingMainDto>> {
    let params = new HttpParams().appendAll({ ...pagination, ...filterParam });
    return this.http.get<PaginationResult<PickingMainDto>>(`${this.baseUrl}GetData`, { params })
  }

  getDataByScan(scanText: string): Observable<GetScanPickingMainDto> {
    let params = new HttpParams().set('qrCodeValue', scanText)
    return this.http.get<GetScanPickingMainDto>(`${this.baseUrl}GetDataByScan`, { params })
  }

  getDataDetail(param: PickingScanParam): Observable<GetScanPickingMainDto> {
    let params = new HttpParams().appendAll({ ...param });
    return this.http.get<GetScanPickingMainDto>(`${this.baseUrl}GetDataDetail`, { params })
  }

  getDataByScanCode(param: PickingScanParam, scanCode: string): Observable<GetScanPickingMainDto> {
    return this.http.get<GetScanPickingMainDto>(`${this.baseUrl}GetDataByScanCode`)
  }

  checkPickingScanCode(param: PickingScanParam, scanCode: string) {
    return this.http.post<OperationResult>(`${this.baseUrl}CheckPickingScanCode`, param, { params: { scanCode } });
  }

  updatePickingQrCode(model: PickingUpdate) {
    return this.http.put<OperationResult>(`${this.baseUrl}UpdatePickingQrCode`, model);
  }

  update(data: GetScanPickingMainDto): Observable<OperationResult> {
    return this.http.post<OperationResult>(`${this.baseUrl}Update`, data);
  }

  release(data: PickingMainDto[]): Observable<OperationResult> {
    return this.http.post<OperationResult>(`${this.baseUrl}Release`, data);
  }

  exportExcel(data: PickingMainDto[]) {
    return this.http.post(`${this.baseUrl}ExportExcel`, data);
  }
}
