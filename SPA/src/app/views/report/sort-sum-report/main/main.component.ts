import { Component, OnInit } from '@angular/core';
import { InjectBase } from '@utilities/inject-base-app';
import { SortSumReportService } from '@services/report/sort-sum-report.service';
import { Pagination } from '@utilities/pagination-utility';
import {
  SortSumDetailReportParam,
  SortSumReportParam,
} from '@models/report/sortSumReportParam';
import { SortSumReportDTO } from '@models/report/sortSumReportDTO';
import { IconButton } from '@constants/common.constants';
import { BsDatepickerConfig } from 'ngx-bootstrap/datepicker';
import { KeyValuePair } from '@utilities/key-value-pair';
import { BrandDTO } from '@models/report/brandDTO';
import { CaptionConstants, MessageConstants } from '@constants/message.enum';
@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss'],
})
export class MainComponent extends InjectBase implements OnInit {
  data: SortSumReportDTO[] = [];
  brand: BrandDTO[] = [];
  actives: KeyValuePair[] = [
    { key: 'sdat', value: 'Sdat Date' },
    { key: 'mdat', value: 'Production_Date' },
  ];
  pagination: Pagination = <Pagination>{
    pageNumber: 1,
    pageSize: 10,
  };

  param: SortSumReportParam;
  paramDetail: SortSumDetailReportParam = <SortSumDetailReportParam>{
    purno: '',
    size: '',
    manno: '',
  };

  bsConfig?: Partial<BsDatepickerConfig>;
  dateFrom_mdat;
  dateTo_mdat;
  dateFrom_eta;
  dateTo_eta;
  iconButton = IconButton;
  constructor(private _service: SortSumReportService) {
    super();
  }

  ngOnInit(): void {
    this.getBrand();
    this.createParam();
    this.getData();
  }

  getData() {
    this.checkDate();
    this.spinnerService.show();
    this._service.getData(this.pagination, this.param).subscribe({
      next: (result) => {
        this.spinnerService.hide();
        this.pagination = result.pagination;
        this.data = result.result;
      },
      error: () => {
        this.spinnerService.hide();
      },
    });
  }

  exportExcel() {
    this.checkDate();
    this.spinnerService.show();
    this._service.exportExcel(this.param).subscribe({
      next: (result) => {
        this.spinnerService.hide();
        const curDate = new Date();
        let fileName =
          'SortSumReport' +
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

  exportDetailExcel() {
    this.paramDetail.manno = this.data[this.selectedIndex].manno;
    this.paramDetail.purno = this.data[this.selectedIndex].purno;
    this.paramDetail.size = this.data[this.selectedIndex].size;
    console.log(this.paramDetail)
    this.spinnerService.show();
    this._service.exportDetailExcel(this.paramDetail).subscribe({
      next: (result) => {
        this.spinnerService.hide();
        const curDate = new Date();
        let fileName =
          'SortSumDetailReport' +
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
  selectedIndex: number;

  changeSelection(event: any, index: number) {
    this.selectedIndex = event.target.checked ? index : undefined;

    let btn = document.getElementById(
      'btnDetailExcel'
    ) as HTMLInputElement | null;
    if (typeof this.selectedIndex === 'undefined') {
      btn.setAttribute('disabled', '');
    } else {
      btn.removeAttribute('disabled');
    }
  }
  search() {
    this.pagination.pageNumber = 1;
    console.log(this.param);
    this.getData();
  }

  clear() {
    this.createParam();
    this.getData();
  }

  pageChanged(e: any) {
    this.pagination.pageNumber = e.page;
    this.getData();
  }

  createParam() {
    this.param = {
      date_kind: '',
      date_start: '',
      date_end: '',
      brand: '',
      cusid: '',
      manno: '',
      rmodel: '',
      tolcls: '',
      purchase_no: '',
      material: '',
      kind: '',
      etd_start: '',
      etd_end: '',
      size: '',
    };

    this.dateFrom_mdat = '';
    this.dateTo_mdat = '';
    this.dateFrom_eta = '';
    this.dateTo_eta = '';
  }

  checkDate() {
    this.param.date_start = !this.functionUtility.checkEmpty(this.dateFrom_mdat)
      ? this.functionUtility.getDateFormat(new Date(this.dateFrom_mdat))
      : '';
    this.param.date_end = !this.functionUtility.checkEmpty(this.dateTo_mdat)
      ? this.functionUtility.getDateFormat(new Date(this.dateTo_mdat))
      : '';
    this.param.etd_start = !this.functionUtility.checkEmpty(this.dateFrom_eta)
      ? this.functionUtility.getDateFormat(new Date(this.dateFrom_eta))
      : '';
    this.param.etd_end = !this.functionUtility.checkEmpty(this.dateTo_eta)
      ? this.functionUtility.getDateFormat(new Date(this.dateTo_eta))
      : '';
  }

  getBrand() {
    this._service.getBrand().subscribe({
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
