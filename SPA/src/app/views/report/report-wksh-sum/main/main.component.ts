import { IconButton } from '@constants/common.constants';
import { Pagination } from '@utilities/pagination-utility';
import {
  Brand,
  MS_QR_Order,
  WkshSumReport,
} from '@models/report/wksh-Sum';
import { KeyValuePair } from '@utilities/key-value-pair';
import { InjectBase } from '@utilities/inject-base-app';
import { Component, OnInit } from '@angular/core';
import { ReportWkshSumService } from '@services/Report/Report-wksh-Sum.service';
import { BsDatepickerConfig } from 'ngx-bootstrap/datepicker';
import { CaptionConstants, MessageConstants } from '@constants/message.enum';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss'],
})
export class MainComponent extends InjectBase implements OnInit {
  actives: KeyValuePair[] = [
    { key: false, value: 'N' },
    { key: true, value: 'Y' },
  ];

  brand: Brand[] = []
  data: MS_QR_Order[] = [];
  pagination: Pagination = <Pagination>{
    pageNumber: 1,
    pageSize: 10,
  };
  param: WkshSumReport = <WkshSumReport>{
    mdat_start: "",
    mdat_end: "",
    close_status: '',
    brandname: '',
    cusna: '',
    manno: '',
    rmodel: '',
    tolcls: '',
    purno: '',
    bitnbr: '',
    kind: '',
    eta_start: "",
    eta_end: "",
    size: '',
  };
  params: MS_QR_Order = <MS_QR_Order>{};

  bsConfig?: Partial<BsDatepickerConfig>;
  dateFrom_mdat = '';
  dateTo_mdat = '';
  dateFrom_eta = '';
  dateTo_eta = '';
  iconButton = IconButton;
  constructor(private service: ReportWkshSumService) {
    super();
  }

  ngOnInit(): void {
    this.search();
    this.getBrand();
    this.getData();
  }
  pageChanged(e: any) {
    this.pagination.pageNumber = e.page;
    this.getData();
  }
  getBrand() {
    this.spinnerService.show();
    this.service.getBrand().subscribe({
      next: res => {
        this.brand = res
        this.spinnerService.hide();
      }
    })
  }
  clear() {
      this.param.mdat_start = '',
      this.param.mdat_end = '',
      this.param.close_status = '',
      this.param.brandname = '',
      this.param.cusna = '',
      this.param.manno = '',
      this.param.rmodel = '',
      this.param.tolcls = '',
      this.param.purno = '',
      this.param.bitnbr = '',
      this.param.kind = '',
      this.param.eta_start = '',
      this.param.eta_end = '',
      this.param.size = '',
      this.dateFrom_mdat = "";
      this.dateTo_mdat = "";
      this.dateFrom_eta = "";
      this.dateTo_eta = "";
      this.getData()
  }
  getData() {
    this.param.mdat_start = !this.functionUtility.checkEmpty(this.dateFrom_mdat)
      ? this.functionUtility.getDateFormat(new Date(this.dateFrom_mdat))
      : '';
    this.param.mdat_end = !this.functionUtility.checkEmpty(this.dateTo_mdat)
      ? this.functionUtility.getDateFormat(new Date(this.dateTo_mdat))
      : '';
    this.param.eta_start = !this.functionUtility.checkEmpty(this.dateFrom_eta)
      ? this.functionUtility.getDateFormat(new Date(this.dateFrom_eta))
      : '';
    this.param.eta_end = !this.functionUtility.checkEmpty(this.dateTo_eta)
      ? this.functionUtility.getDateFormat(new Date(this.dateTo_eta))
      : '';
    this.spinnerService.show();

    this.service.getData(this.pagination, this.param).subscribe({
      next: (res) => {
        res.result;
        res.result.map((ngay) =>
          this.functionUtility.getDateFormat(new Date(ngay.mdat))
        );
        this.data = res.result;
        this.pagination = res.pagination;
        this.spinnerService.hide();
      },
      error: () => {},
    });
  }
  search() {
    this.pagination.pageNumber === 1
    ? this.getData()
    : (this.pagination.pageNumber = 1);
  }
  export() {
    this.param.mdat_start = !this.functionUtility.checkEmpty(this.dateFrom_mdat)
    ? this.functionUtility.getDateFormat(new Date(this.dateFrom_mdat))
    : '';
  this.param.mdat_end = !this.functionUtility.checkEmpty(this.dateTo_mdat)
    ? this.functionUtility.getDateFormat(new Date(this.dateTo_mdat))
    : '';
  this.param.eta_start = !this.functionUtility.checkEmpty(this.dateFrom_eta)
    ? this.functionUtility.getDateFormat(new Date(this.dateFrom_eta))
    : '';
  this.param.eta_end = !this.functionUtility.checkEmpty(this.dateTo_eta)
    ? this.functionUtility.getDateFormat(new Date(this.dateTo_eta))
    : '';
    this.spinnerService.show();
    this.service.exportExcel(this.params).subscribe({
        next: (result: Blob) => {
          this.functionUtility.exportExcel(result, 'Report wksh Sum');
        },
        error: () => {
          this.snotifyService.error(
            MessageConstants.UN_KNOWN_ERROR,
            CaptionConstants.ERROR
          );
        },
      })
      .add(() => this.spinnerService.hide());
  }

}
