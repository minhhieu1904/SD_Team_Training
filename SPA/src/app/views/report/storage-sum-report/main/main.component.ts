import { Component, OnInit } from '@angular/core';
import { IconButton } from '@constants/common.constants';
import { Brand, StorageSumDetailReportParam, StorageSumReportParam, StorageSumReportResult } from '@models/report/Storage_Sum_Report';
import { InjectBase } from '@utilities/inject-base-app';
import { KeyValuePair } from '@utilities/key-value-pair';
import { Pagination } from '@utilities/pagination-utility';
import { BsDatepickerConfig } from 'ngx-bootstrap/datepicker';
import { StorageSumReportService } from "../../../../_core/services/report/storage-sum-report.service";
import { CaptionConstants, MessageConstants } from '@constants/message.enum';
@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss']
})
export class MainComponent extends InjectBase implements OnInit {

  actives: KeyValuePair[] = [
    { key: "mdat", value: 'Production_Date' },
    { key: "sdat", value: 'Sdat Date' },
  ]
  brand: Brand[] = [];
  data: StorageSumReportResult[] = [];
  pagination: Pagination = <Pagination>{
    pageNumber: 1,
    pageSize: 10
  };
  selectedIndex: number;
  param: StorageSumReportParam = <StorageSumReportParam>{
    date_kind: '',
    date_start: '',
    date_end: '',
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
  paramDetail: StorageSumDetailReportParam = <StorageSumDetailReportParam>{
    manno: '',
    purno: '',
    size: ''

  }

  bsConfig?: Partial<BsDatepickerConfig>;
  dateFrom_date = "";
  dateTo_date = "";
  dateFrom_eta = "";
  dateTo_eta = "";
  iconButton = IconButton;
  constructor(private service: StorageSumReportService
  ) { super() }


  ngOnInit(): void {
    this.getData();
    this.getBrand();

  }
  pageChanged(e: any) {
    this.pagination.pageNumber = e.page;
    this.getData()
  }

  checkboxChange(event, index) {
    this.selectedIndex = event.target.checked ? index : undefined;
    this.disButton();
  }

  search() {
    this.pagination.pageNumber = 1;
    this.getData()
  }
  getData() {
    this.spinnerService.show();
    this.param.eta_start = !this.functionUtility.checkEmpty(this.dateFrom_date) ? this.functionUtility.getDateFormat(new Date(this.dateFrom_date)) : "";
    this.param.eta_end = !this.functionUtility.checkEmpty(this.dateTo_date) ? this.functionUtility.getDateFormat(new Date(this.dateTo_date)) : "";
    this.param.eta_start = !this.functionUtility.checkEmpty(this.dateFrom_eta) ? this.functionUtility.getDateFormat(new Date(this.dateFrom_eta)) : "";
    this.param.eta_end = !this.functionUtility.checkEmpty(this.dateTo_eta) ? this.functionUtility.getDateFormat(new Date(this.dateTo_eta)) : "";

    this.service.getData(this.pagination, this.param).subscribe({
      next: res => {
        // console.log(res.result)
        this.data = res.result;
        this.pagination = res.pagination;
        this.spinnerService.hide();
      },
      error: () => {
      }
    })
  }
  eportDetailExcel() {
    this.paramDetail.manno = this.data[this.selectedIndex].manno;
    this.paramDetail.purno = this.data[this.selectedIndex].purno;
    this.paramDetail.size = this.data[this.selectedIndex].size;
    console.log(this.paramDetail)
    this.spinnerService.show();
    this.service.ExportDetailExcel(this.paramDetail).subscribe({
      next: (res: Blob) => {
        if (res) {
          // console.log(res);
          this.functionUtility.exportExcel(res, 'SortSumReport')
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



  disButton() {
    let btn = document.getElementById('disBtn') as HTMLInputElement | null;
    if (typeof this.selectedIndex === "undefined") {
      btn.setAttribute('disabled', '');
    } else {
      btn.removeAttribute('disabled');
    }
  }
  eportExcel() {
    this.spinnerService.show();
    this.service.exportExcel(this.pagination, this.param).subscribe({
      next: (res: Blob) => {
        if (res) {
          // console.log(res);
          this.functionUtility.exportExcel(res, 'SortSumReport')
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
    this.param.date_kind = '',
      this.param.date_start = '',
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
      this.param.size = ''
    this.search();
  }

}
