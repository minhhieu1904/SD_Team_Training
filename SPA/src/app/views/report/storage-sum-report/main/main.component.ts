import { Component, OnInit } from '@angular/core';
import { IconButton } from '@constants/common.constants';
import { StorageSumReportDTO } from '@models/report/storageSumReportDTO';
import {
  StorageSumDetailReportParam,
  StorageSumReportParam,
} from '@models/report/storageSumReportParam';
import { CommonService } from '@services/common/common.service';
import { StorageSumReportService } from '@services/report/storage-sum-report.service';
import { InjectBase } from '@utilities/inject-base-app';
import { KeyValuePair } from '@utilities/key-value-pair';
import { Pagination } from '@utilities/pagination-utility';
import { BsDatepickerConfig } from 'ngx-bootstrap/datepicker';
@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss'],
})
export class MainComponent extends InjectBase implements OnInit {
  //#region attribute
  iconButton = IconButton;
  data: StorageSumReportDTO[] = [];
  listBrandName: KeyValuePair[] = [];
  actives: KeyValuePair[] = [
    { key: 'sdat', value: 'Sdat Date' },
    { key: 'mdat', value: 'Production_Date' },
  ];
  pagination: Pagination = <Pagination>{
    pageNumber: 1,
    pageSize: 10,
  };

  param: StorageSumReportParam;
  paramDetail: StorageSumDetailReportParam = <StorageSumDetailReportParam>{
    purno: '',
    size: '',
    manno: '',
  };

  bsConfig?: Partial<BsDatepickerConfig>;
  dateFrom_mdat;
  dateTo_mdat;
  dateFrom_eta;
  dateTo_eta;
  selectedIndex: number;
  //#endregion

  constructor(
    private service: StorageSumReportService,
    private commonService: CommonService
  ) {
    super();
  }

  ngOnInit(): void {
    this.getBrand();
    this.createParam();
    this.getData();
  }

  //#region function
  getData() {
    this.checkDate();
    this.spinnerService.show();
    this.service.getData(this.pagination, this.param).subscribe({
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
    this.service.exportExcel(this.param).subscribe({
      next: (result) => {
        this.spinnerService.hide();
        const curDate = new Date();
        let fileName =
          'StorageSumReport' +
          curDate.getFullYear().toString() +
          (curDate.getMonth() + 1) +
          curDate.getDate();
        this.functionUtility.exportExcel(result, fileName);
      },
      error: () => {
        this.spinnerService.hide();
        this.snotifyService.error(
          this.translateService.instant('System.Message.SystemError'),
          this.translateService.instant('System.Caption.Error')
        );
      },
    });
  }

  exportDetailExcel() {
    this.paramDetail.manno = this.data[this.selectedIndex].manno;
    this.paramDetail.purno = this.data[this.selectedIndex].purno;
    this.paramDetail.size = this.data[this.selectedIndex].size;
    this.spinnerService.show();
    this.service.exportDetailExcel(this.paramDetail).subscribe({
      next: (result) => {
        this.spinnerService.hide();
        const curDate = new Date();
        let fileName =
          'StorageDetailReport' +
          curDate.getFullYear().toString() +
          (curDate.getMonth() + 1) +
          curDate.getDate();
        this.functionUtility.exportExcel(result, fileName);
      },
      error: () => {
        this.spinnerService.hide();
        this.snotifyService.error(
          this.translateService.instant('System.Message.SystemError'),
          this.translateService.instant('System.Caption.Error')
        );
      },
    });
  }

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
    this.pagination.pageNumber === 1
      ? this.getData()
      : (this.pagination.pageNumber = 1);
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
      brandname: '',
      cusid: '',
      manno: '',
      rmodel: '',
      tolcls: '',
      purno: '',
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
    this.commonService.getListBrandName().subscribe({
      next: (result) => {
        this.listBrandName = result;
      },
      error: () => {
        this.snotifyService.error(
          this.translateService.instant('System.Message.SystemError'),
          this.translateService.instant('System.Caption.Error')
        );
      },
    });
  }
  //#endregion
}
