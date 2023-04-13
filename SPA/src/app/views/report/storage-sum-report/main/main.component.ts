import { Component, OnInit } from '@angular/core';
import { InjectBase } from '@utilities/inject-base-app';
import { StorageSumReportService } from '../../../../_core/services/report/storage-sum-report.service'; 
import { KeyValuePair } from '@utilities/key-value-pair';
import { MsQrOrder } from '@models/msQrOrder';
import { Pagination } from '@utilities/pagination-utility';
import { StorageSumReportParam } from '@models/storageSumReportParam';
import { BsDatepickerConfig } from 'ngx-bootstrap/datepicker';
import { IconButton } from '@constants/sd-team.utility';
import { CaptionConstants, MessageConstants } from '@constants/message.enum';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss']
})
export class MainComponent extends InjectBase implements OnInit {

  constructor(private service: StorageSumReportService) { super(); }
  dateKind: KeyValuePair[] = [
    {'key': 'sdat', 'value': 'Sdat Date'},
    {'key': 'mdat', 'value': 'Production_Date'}
  ];
  ngOnInit(): void {
  this.getData();
  }
  
  iconButton = IconButton;
  data: MsQrOrder[] = [];
  pagination: Pagination = <Pagination>{
    pageNumber: 1,
    pageSize: 10,
  }
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
bsConfig? : Partial<BsDatepickerConfig>;
dateFrom_date = "";
dateTo_date = "";
dateFrom_eta = "";
dateTo_eta = "";
checkDate(){
  this.param.date_start = !this.functionUtility.checkEmpty(this.dateFrom_date) ? this.functionUtility.getDateTimeFormat(new Date(this.dateFrom_date)):'';
  this.param.date_end = !this.functionUtility.checkEmpty(this.dateTo_date) ? this.functionUtility.getDateTimeFormat(new Date(this.dateTo_date)):'';
  this.param.eta_start = !this.functionUtility.checkEmpty(this.dateFrom_eta) ? this.functionUtility.getDateTimeFormat(new Date(this.dateFrom_eta)):'';
  this.param.eta_end = !this.functionUtility.checkEmpty(this.dateTo_eta) ? this.functionUtility.getDateTimeFormat(new Date(this.dateTo_eta)):'';
}   
getData(){
  this.checkDate();
  this.spinnerService.show();
  this.service.getData(this.pagination, this.param).subscribe({
    next: res => {
      res.result.map(ngay => this.functionUtility.getDateFormat(new Date(ngay.mdat)))
      this.data = res.result;
      this.pagination = res.pagination;
      this.spinnerService.hide();
    }, error: () =>{}
  })
}
pageChanged(e: any) { 
  this.pagination.pageNumber = e.page;
  this.getData()
}
search(){
  this.pagination.pageNumber = 1;
  this.getData();
}
clear(){
  this.param = <StorageSumReportParam>{
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
  this.dateFrom_date = '';
    this.dateTo_date = '';
    this.dateFrom_eta = '';
    this.dateTo_eta = '';
    this.search() 
}
exportExcel(){
  this.spinnerService.show();
  this.service.exportExcel(this.param).subscribe({
    next: res => {
      this.functionUtility.exportExcel(res, 'StorageSumReport')
      this.spinnerService.hide();
    }, error: () => this.snotifyService.error(((MessageConstants.SYSTEM_ERROR_MSG)), CaptionConstants.ERROR)
  })
}
exportDetailExcel(){}
}
