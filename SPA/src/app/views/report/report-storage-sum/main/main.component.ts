import { MessageConstants, CaptionConstants } from './../../../../_core/constants/message.enum';
import { IconButton } from '@constants/common.constants';
import { BsDatepickerConfig } from 'ngx-bootstrap/datepicker';
import { Pagination } from '@utilities/pagination-utility';
import { InjectBase } from '@utilities/inject-base-app';

import { ReportStorageSumService } from '@services/Report/Report-Storage-Sum.service';
import { Report_Storage_SumParam,Brand,StorageSumDeltailDTOParam,Storage_sumDTO } from '@models/report/storage-Sum';
import { KeyValuePair } from '@utilities/key-value-pair';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss']
})
export class MainComponent  extends InjectBase implements OnInit {
  dateKind: KeyValuePair[] = [
    { key: 'sdat', value: 'sdat_Date' },
    { key: 'mdat', value: 'Production_Date' },
  ];
  ischeckItem: boolean = false;
  dataChecked: Report_Storage_SumParam;

  constructor(private service : ReportStorageSumService) {super(); }

  ngOnInit(): void {
    this.search();
    this.getBrand();
  }
  brand: Brand[] = [];
  paramsExportDetail : StorageSumDeltailDTOParam = <StorageSumDeltailDTOParam>{
    manno: '',
    purno: '',
    size: '',
  };
paramsExport : Report_Storage_SumParam = <Report_Storage_SumParam>{};
data: Report_Storage_SumParam[] = [];
pagination: Pagination = <Pagination>{
  pageNumber: 1,
  pageSize: 10,
};
param: Storage_sumDTO = <Storage_sumDTO>{
  crDay: '',
  date_kind: '',
  date_start: '',
  date_end: '',
  brandname: '',
  cusna: '',
  manno: '',
  purno: '',
  rmodel: '',
  tolcls: '',
  bitnbr: '',
  kind: '',
  eta_start: '',
  eta_end: '',
  size: '',
};
bsConfig?: Partial<BsDatepickerConfig>;
dateFrom_mdat = '';
dateTo_mdat = '';
dateFrom_eta = '';
dateTo_eta = '';
crDay= '';
iconButton = IconButton;
pageChanged(e: any) {
  this.pagination.pageNumber = e.page;
  this.getData();
}
search() {
  this.pagination.pageNumber === 1
    ? this.getData()
    : (this.pagination.pageNumber = 1);
}
getBrand() {
  this.spinnerService.show();
  this.service.getBrand().subscribe({
    next: (res) => {
      this.brand = res;
      this.spinnerService.hide();
    },
    error: () => this.spinnerService.hide(),
  });
}
getData() {
  this.param.date_start = !this.functionUtility.checkEmpty(this.dateFrom_mdat)
    ? this.functionUtility.getDateFormat(new Date(this.dateFrom_mdat))
    : '';
  this.param.date_end = !this.functionUtility.checkEmpty(this.dateTo_mdat)
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
clear() {
  this.param.crDay = '',
  this.param.date_start = '',
  this.param.date_kind = '',
  this.param.date_end = '',
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
  this.dateFrom_mdat = '';
  this.dateTo_mdat = '';
  this.dateFrom_eta = '';
  this.dateTo_eta = '';
  this.getData();
}

export() {
  this.param.date_start = !this.functionUtility.checkEmpty(this.dateFrom_mdat)
    ? this.functionUtility.getDateFormat(new Date(this.dateFrom_mdat))
    : '';
  this.param.date_end = !this.functionUtility.checkEmpty(this.dateTo_mdat)
    ? this.functionUtility.getDateFormat(new Date(this.dateTo_mdat))
    : '';
  this.param.eta_start = !this.functionUtility.checkEmpty(this.dateFrom_eta)
    ? this.functionUtility.getDateFormat(new Date(this.dateFrom_eta))
    : '';
  this.param.eta_end = !this.functionUtility.checkEmpty(this.dateTo_eta)
    ? this.functionUtility.getDateFormat(new Date(this.dateTo_eta))
    : '';
  this.spinnerService.show();
  this.service
    .exportExcel(this.paramsExport)
    .subscribe({
      next: (result: Blob) => {
        this.functionUtility.exportExcel(result, 'Report Sort Sum');
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
exportDetails() {

  this.crDay = !this.functionUtility.checkEmpty(this.crDay)
  ? this.functionUtility.getDateFormat(new Date(this.crDay))
  : '';

this.spinnerService.show();
this.service
  .exportExcelDetail(this.paramsExportDetail)
  .subscribe({
    next: (result: Blob) => {
      this.functionUtility.exportExcel(result, 'Report Sort Sum Detail');
    },
    error: () => {
      this.spinnerService.hide();
      this.snotifyService.error(
        MessageConstants.UN_KNOWN_ERROR,
        CaptionConstants.ERROR
      );
    },
  })
}

checkRecord(item: Report_Storage_SumParam) {
item.ischeck = !item.ischeck;
this.ischeckItem = item.ischeck;
this.data.filter(x => x != item).forEach(x => { x.ischeck = false });
this.dataChecked = item;

this.paramsExportDetail.manno = item.manno;
this.paramsExportDetail.purno = item.purno;
this.paramsExportDetail.size = item.size;
}

}
