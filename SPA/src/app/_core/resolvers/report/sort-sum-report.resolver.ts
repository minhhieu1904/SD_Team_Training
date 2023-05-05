import { Injectable } from '@angular/core';
import { Resolve, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import { Pagination, PaginationResult } from './../../utilities/pagination-utility';
import { SortSumReportParam, MsQrOrder } from "./../../models/report/msQrOrder";
import { SortSumReportService } from '@services/report/sort-sum-report.service';
import { NgxSpinnerService } from 'ngx-spinner';

@Injectable({
  providedIn: 'root'
})
export class SortSumReportResolver implements Resolve<PaginationResult<MsQrOrder>> {

    constructor(private service: SortSumReportService, private spinnerService: NgxSpinnerService) { }
    param: SortSumReportParam = <SortSumReportParam>{}
    pagination: Pagination = <Pagination>{
        pageNumber: 1,
        pageSize: 10
      };

    resolve(route: ActivatedRouteSnapshot): Observable<PaginationResult<MsQrOrder>> {
      this.spinnerService.show();
        return this.service.getData(this.pagination,this.param);
    }
}
