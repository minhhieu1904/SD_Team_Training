import { Component, OnInit } from '@angular/core';
import { IconButton } from '@constants/sd-team.utility';
import { KeyValuePair } from '@utilities/key-value-pair';
import { BsDatepickerConfig } from 'ngx-bootstrap/datepicker';
import { WkshSumReportService } from "../../../../_core/services/report/wksh-sum-report.service";
import { Pagination } from '@utilities/pagination-utility';
import { Brand, MS_QR_Order, WkshSumReport } from '@models/report/MS_QR_Order';
import { InjectBase } from '@utilities/inject-base-app';
import { NgSnotifyService } from '@services/ng-snotify.service';
import { CaptionConstants, MessageConstants } from '@constants/message.enum';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss']
})
export class MainComponent extends InjectBase implements OnInit {
  actives: KeyValuePair[] = [
    { key: true, value: 'Y' },
    { key: false, value: 'N' },
  ]
  brand: Brand[] = []
  data: MS_QR_Order[] = [];
  pagination: Pagination = <Pagination>{
    pageNumber: 1,
    pageSize: 10
  };
  param: WkshSumReport = <WkshSumReport>{
    mdat_start: '',
    mdat_end: '',
    close_status: '',
    brandname: '',
    cusna: '',
    manno: '',
    rmodel: '',
    tolcls: '',
    purno: '',
    bitnbr: '',
    kind: '',
    eta_start: '',
    eta_end: '',
    size: ''
  }
  bsConfig?: Partial<BsDatepickerConfig>;
  dateFrom_mdat = "";
  dateTo_mdat = "";
  dateFrom_eta = "";
  dateTo_eta = "";
  iconButton = IconButton;
  constructor(
    private service: WkshSumReportService) { super() }


  ngOnInit(): void {
    this.getData();
    this.getBrand();

  }
  pageChanged(e: any) {
    this.pagination.pageNumber = e.page;
    this.getData()


  }
  search() {
    this.pagination.pageNumber = 1;
    this.getData()
  }
  getData() {
    this.param.mdat_start = !this.functionUtility.checkEmpty(this.dateFrom_mdat) ? this.functionUtility.getDateFormat(new Date(this.dateFrom_mdat)) : "";
    this.param.mdat_end = !this.functionUtility.checkEmpty(this.dateTo_mdat) ? this.functionUtility.getDateFormat(new Date(this.dateTo_mdat)) : "";
    this.param.eta_start = !this.functionUtility.checkEmpty(this.dateFrom_eta) ? this.functionUtility.getDateFormat(new Date(this.dateFrom_eta)) : "";
    this.param.eta_end = !this.functionUtility.checkEmpty(this.dateTo_eta) ? this.functionUtility.getDateFormat(new Date(this.dateTo_eta)) : "";

    this.spinnerService.show();
    this.service.getData(this.pagination, this.param).subscribe({
      next: res => {
        this.data = res.result;
        this.pagination = res.pagination;
        this.spinnerService.hide();
      },
      error: () => {
      }
    })
  }
  eportExcel() {
    this.param.mdat_start = !this.functionUtility.checkEmpty(this.dateFrom_mdat) ? this.functionUtility.getDateFormat(new Date(this.dateFrom_mdat)) : "";
    this.param.mdat_end = !this.functionUtility.checkEmpty(this.dateTo_mdat) ? this.functionUtility.getDateFormat(new Date(this.dateTo_mdat)) : "";
    this.param.eta_start = !this.functionUtility.checkEmpty(this.dateFrom_eta) ? this.functionUtility.getDateFormat(new Date(this.dateFrom_eta)) : "";
    this.param.eta_end = !this.functionUtility.checkEmpty(this.dateTo_eta) ? this.functionUtility.getDateFormat(new Date(this.dateTo_eta)) : "";

    this.spinnerService.show();
    this.service.exportExcel(this.pagination, this.param).subscribe({
      next: (res: Blob) => {
        if (res) {
          // console.log(res);
          this.functionUtility.exportExcel(res, 'DevelopmentOrder')
          this.spinnerService.hide();
        }
      },
      error: () => {
        this.snotifyService.error(
          MessageConstants.UN_KNOWN_ERROR,
          CaptionConstants.ERROR
        );
      }
    })
  }
  getBrand() {
    this.spinnerService.show();
    this.service.getBrand().subscribe({
      next: res => {
        this.brand = res
        // console.log('abc', this.brand )
        this.spinnerService.hide();
      },
      error: () => {
      }
    })
  }
  clear() {
    this.param.bitnbr = '';
    this.param.brandname = '';
    this.param.close_status = '';
    this.param.cusna = '';
    this.param.eta_end = '';
    this.param.eta_start = '';
    this.param.kind = '';
    this.param.manno = '';
    this.param.mdat_end = '';
    this.param.mdat_start = '';
    this.param.purno = '';
    this.param.rmodel = '';
    this.dateFrom_mdat = "";
    this.dateTo_mdat = "";
    this.dateFrom_eta = "";
    this.dateTo_eta = "";
    this.getData();
  }

}
