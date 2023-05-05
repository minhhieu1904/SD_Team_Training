import { Component, OnInit } from '@angular/core';
import { InjectBase } from '@utilities/inject-base-app';
import { StorageSumReportService } from '@services/report/storage-sum-report.service'; 
import { KeyValuePair } from '@utilities/key-value-pair';
import { MsQrOrder, BrandDTO, StorageSumDetailReportParam} from '@models/report/msQrOrder';
import { Pagination } from '@utilities/pagination-utility';
import { StorageSumReportParam } from '@models/report/storageSumReportParam';
import { StorageSumReportDTO } from '@models/report/storageSumReportDTO';
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
  this.getBrand();
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

paramDetail: StorageSumDetailReportParam = <StorageSumDetailReportParam>{
  manno: '',
  purno: '',
  size: ''
}
selectedIndex: number;
exportDetailExcel(){
  this.paramDetail.manno = this.data[this.selectedIndex].manno;
  this.paramDetail.purno = this.data[this.selectedIndex].purno;
  this.paramDetail.size = this.data[this.selectedIndex].size;
  console.log(this.paramDetail)
  this.spinnerService.show()
  this.service.exportDetailExcel(this.paramDetail).subscribe({
    next: res => {
      this,this.functionUtility.exportExcel(res, "StorageSumDetailReport")
      this.spinnerService.hide();
    },error: () => this.snotifyService.error(((MessageConstants.SYSTEM_ERROR_MSG)), CaptionConstants.ERROR)
  })
}
dataChecked: StorageSumReportDTO[] = [];
changeSelection(event: any, index: number) {
  this.selectedIndex = event.target.checked ? index : undefined;
  let btn = document.getElementById('disBtn') as HTMLInputElement | null;
  if (typeof this.selectedIndex === "undefined") {
    btn.setAttribute('disabled', '');
  } else {
    btn.removeAttribute('disabled');
  }
}

brand: BrandDTO[] = [];
getBrand(){
  this.service.getBrand().subscribe({
    next: res => {
      this.brand = res;
    },error: () => {}
  })
}
}
