import { Injectable } from '@angular/core';
import { Resolve, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import { Pagination, PaginationResult } from '@utilities/pagination-utility';
import { WkshSumReport, MsQrOrder } from "@models/report/msQrOrder";
import { WkshSumReportService } from "@services/report/wksh-sum-report.service";
import { NgxSpinnerService } from 'ngx-spinner';

@Injectable({
  providedIn: 'root'
})
export class WkshSumReportResolver implements Resolve<PaginationResult<MsQrOrder>> {
    
    constructor(
        private service: WkshSumReportService,
        private spinnerService: NgxSpinnerService
        ) { }
    param: WkshSumReport = <WkshSumReport>{
    }
    pagination: Pagination = <Pagination>{
        pageNumber: 1,
        pageSize: 10
      };
    resolve(route: ActivatedRouteSnapshot): Observable<PaginationResult<MsQrOrder>> {
        this.spinnerService.show();
        return this.service.getData(this.pagination, this.param);
    }
}
