import { Component, OnInit } from '@angular/core';
import { IconButton } from '@constants/sd-team.utility';
import { MsQrOrder, SortSumReportParam, SortSumReportDTO, BrandDTO, SortSumDetailReportParam} from '../../../../_core/models/msQrOrder'; 
import { SortSumReportService } from '../../../../_core/services/report/sort-sum-report.service'; 
import { InjectBase } from '@utilities/inject-base-app';
import { BsDatepickerConfig } from 'ngx-bootstrap/datepicker';
import { Pagination } from '@utilities/pagination-utility';
import { KeyValuePair } from '@utilities/key-value-pair';
import { CaptionConstants, MessageConstants } from '@constants/message.enum';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss']
})
export class MainComponent extends InjectBase implements OnInit {
  iconButton = IconButton;
  constructor(private service: SortSumReportService) {super();}
  dateKind: KeyValuePair[] = [
    {
      'key': 'sdat',
      'value': 'Sdat Date'
    },
    {
      'key': 'mdat',
      'value': 'Production_Date'
    }
  ];
  data: MsQrOrder[] = [];
  dataChecked: SortSumReportDTO[] = [];
  brand: BrandDTO[] = [];
  pagination: Pagination = <Pagination>{
    pageNumber: 1, 
    pageSize: 10
  };
  maxSize = 5;
  param: SortSumReportParam = <SortSumReportParam>{
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
  
  ngOnInit(): void {
    this.getBrand();
    this.getData();
  }
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

  search() {
    this.pagination.pageNumber = 1;
    this.getData()
  } 
  clear() {
    this.param = <SortSumReportParam>{
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
  selectedIndex: number;
  changeSelection(event: any, index: number) {
    this.selectedIndex = event.target.checked ? index : undefined;
    let btn = document.getElementById('disBtn') as HTMLInputElement | null;
    if (typeof this.selectedIndex === "undefined") {
      btn.setAttribute('disabled', '');
    } else {
      btn.removeAttribute('disabled');
    }
  }


  getBrand(){
    this.service.getBrand().subscribe({
      next: (res) => {
        this.brand = res;
      }, error: () => {}
    })
  }

  exportExcel(){
    this.spinnerService.show();
    this.service.exportExcel(this.param).subscribe({
      next: (res: Blob) =>{
        if (res) {
          this.functionUtility.exportExcel(res, 'SortSumReport')
          this.spinnerService.hide();
        }
      }, error: () => this.snotifyService.error(MessageConstants.SYSTEM_ERROR_MSG, CaptionConstants.ERROR)
    })
  }

  paramDetail: SortSumDetailReportParam = <SortSumDetailReportParam>{
    purno: '',
    size: '',
    manno: '',
  };
  exportDetailExcel(){
    this.paramDetail.manno = this.data[this.selectedIndex].manno;
    this.paramDetail.purno = this.data[this.selectedIndex].purno;
    this.paramDetail.size = this.data[this.selectedIndex].size;
    console.log(this.paramDetail)
    this.spinnerService.show();
    this.service.exportDetailExcel(this.paramDetail).subscribe({
      next: (res) => {
          this.functionUtility.exportExcel(res, 'SortSumDetailReport')
          this.spinnerService.hide();
      },error: () => this.snotifyService.error(((MessageConstants.SYSTEM_ERROR_MSG)), CaptionConstants.ERROR)
    })
  }

}
