import { Component, OnInit } from '@angular/core';
import { InjectBase } from '@utilities/inject-base-app';
import { SearchForPackingScanService } from '@services/report/search-for-packing-scan.service';
import { MsQrOrder } from '@models/report/msQrOrder';
import { KeyValuePair } from '@utilities/key-value-pair';
import { Pagination } from '@utilities/pagination-utility';
import { SearchForPackingScanParam } from '@models/report/searchForPackingScanParam';
import { IconButton } from '@constants/sd-team.utility';
import { BsDatepickerConfig } from 'ngx-bootstrap/datepicker';

import { BrandDTO } from '@models/report/brandDTO';
import { CaptionConstants, MessageConstants } from '@constants/message.enum';
@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss'],
})
export class MainComponent extends InjectBase implements OnInit {
  brand: BrandDTO[] = [];
  data: MsQrOrder[] = [];
  actives: KeyValuePair[] = [
    { key: false, value: 'N' },
    { key: true, value: 'Y' },
  ];

  pagination: Pagination = <Pagination>{
    pageNumber: 1,
    pageSize: 10,
  };

  param: SearchForPackingScanParam;

  bsConfig?: Partial<BsDatepickerConfig>;
  dateFrom_mdat;
  dateTo_mdat;
  dateFrom_eta;
  dateTo_eta;
  iconButton = IconButton;
  constructor(private service: SearchForPackingScanService) {
    super();
  }

  ngOnInit(): void {
    this.createParam();
    this.getBrand();
    this.getData();
  }

  getData() {
    this.checkDate();
    this.spinnerService.show();

    this.service.getData(this.pagination, this.param).subscribe({
      next: (result) => {
        this.spinnerService.hide();
        this.data = result.result;
        this.pagination = result.pagination;
      },
      error: () => {
        this.spinnerService.hide();
        this.snotifyService.error(
          MessageConstants.SYSTEM_ERROR_MSG,
          CaptionConstants.ERROR
        );
      },
    });
  }

  search() {
    this.pagination.pageNumber = 1;
    this.getData();
  }

  checkDate() {
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
  }

  createParam() {
    this.param = <SearchForPackingScanParam>{
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
      size: '',
    };

    this.dateFrom_mdat = '';
    this.dateTo_mdat = '';
    this.dateFrom_eta = '';
    this.dateTo_eta = '';
  }

  clear() {
    this.createParam();
    this.getData();
  }

  pageChanged(e: any) {
    this.pagination.pageNumber = e.page;
    this.getData();
  }

  exportExcel() {
    this.checkDate();
    this.spinnerService.show();
    this.service.exportExcel(this.param).subscribe({
      next: (result) => {
        const curDate = new Date();
        let fileName =
          'DevelopmentOrder' +
          curDate.getFullYear().toString() +
          (curDate.getMonth() + 1) +
          curDate.getDate();
        this.functionUtility.exportExcel(result, fileName);
      },
      error: () => {
        this.spinnerService.hide();
        this.snotifyService.error(
          MessageConstants.SYSTEM_ERROR_MSG,
          CaptionConstants.ERROR
        );
      },
    });
  }

  getBrand() {
    this.service.getBrand().subscribe({
      next: (result) => {
        this.brand = result;
      },
      error: () => {
        this.snotifyService.error(
          MessageConstants.SYSTEM_ERROR_MSG,
          CaptionConstants.ERROR
        );
      },
    });
  }
}
