import { Component, OnInit } from '@angular/core';
import { KeyValuePair } from '@utilities/key-value-pair';
import {MsQrOrder, WkshSumReport } from '../../../../_core/models/msQrOrder'; 
import { BrandDTO } from '../../../../_core/models/brandDTO'; 
import { Pagination } from '@utilities/pagination-utility';
import { BsDatepickerConfig } from 'ngx-bootstrap/datepicker';
import { IconButton } from '@constants/sd-team.utility';
import { WkshSumReportService } from "../../../../_core/services/report/wksh-sum-report.service";
import { InjectBase } from '@utilities/inject-base-app';
import { CaptionConstants, MessageConstants } from '@constants/message.enum';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss']
})
export class MainComponent extends InjectBase implements OnInit {
  actives: KeyValuePair[] = [
    {key: false, value: 'N'},
    {key: true, value: 'Y'},
  ]
  maxSize = 5;
  data: MsQrOrder[] = [];
  brand: BrandDTO[] = [];
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
  bsConfig? : Partial<BsDatepickerConfig>;
  dateFrom_mdat = "";
  dateTo_mdat = "";
  dateFrom_eta = "";
  dateTo_eta = "";
  iconButton = IconButton;

  createParam() {
    this.param;
    this.dateFrom_mdat = '';
    this.dateTo_mdat = '';
    this.dateFrom_eta = '';
    this.dateTo_eta = '';
  }
  constructor(private service: WkshSumReportService) { super()}
  

  ngOnInit(): void {
    this.getData();
    this.getBrand();
    this.createParam();
  }
  pageChanged(e: any) { 
    this.pagination.pageNumber = e.page;
    this.getData()
  }
  search() {
    this.pagination.pageNumber = 1;
    this.getData()
  }  
  clear() {
    this.createParam()
    this.getData()
    
  }
  checkDate(){
    this.param.mdat_start = !this.functionUtility.checkEmpty(this.dateFrom_mdat) ? this.functionUtility.getDateTimeFormat(new Date(this.dateFrom_mdat)):'';
    this.param.mdat_end = !this.functionUtility.checkEmpty(this.dateTo_mdat) ? this.functionUtility.getDateTimeFormat(new Date(this.dateTo_mdat)):'';
    this.param.eta_start = !this.functionUtility.checkEmpty(this.dateFrom_eta) ? this.functionUtility.getDateTimeFormat(new Date(this.dateFrom_eta)):'';
    this.param.eta_end = !this.functionUtility.checkEmpty(this.dateTo_eta) ? this.functionUtility.getDateTimeFormat(new Date(this.dateTo_eta)):'';
  }

  getData(){
    this.checkDate()
    this.spinnerService.show();
    this.service.getData(this.pagination,this.param).subscribe({
      next: res => { 
        res.result.map(ngay => this.functionUtility.getDateFormat(new Date(ngay.mdat)))
        this.data = res.result;
        this.pagination = res.pagination;
        this.spinnerService.hide();
      },
      error: () => { 
      }
    })
  }
  exportExcel() {
    this.checkDate()
    this.spinnerService.show();
    this.service.exportExcel(this.pagination, this.param).subscribe({
      next: (res: Blob) => {
        if (res) {
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
