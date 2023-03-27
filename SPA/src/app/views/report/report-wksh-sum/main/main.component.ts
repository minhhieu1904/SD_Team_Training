import { KeyValue } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ReportwkshSum, MSQROrder, Brand } from '@models/report/report_wksh_Sum'
import { Pagination } from '@utilities/pagination-utility';
import { BsDatepickerConfig } from 'ngx-bootstrap/datepicker';
import { IconButton } from '@constants/common.constants'
import { ReportWkshSumService } from '@services/report/report-wksh-sum.service'
import { NgxSpinnerService } from 'ngx-spinner';
import { FunctionUtility } from '@utilities/function-utility'
import { NgSnotifyService } from '@services/common/ng-snotify.service';
import { CaptionConstants, MessageConstants } from '@constants/message.enum';
import { KeyValueUtility } from '@utilities/key-value-utility';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss']
})
export class MainComponent implements OnInit {
  actives: KeyValueUtility[] = [
    { key: false, value: 'N' },
    { key: true, value: 'Y' },
  ]

  brand: Brand[] = [];
  data: MSQROrder[] = [];
  data1: MSQROrder = <MSQROrder>{};
  pagination: Pagination = <Pagination>{
    pageSize: 10,
    pageNumber: 1,
  };

  param: ReportwkshSum = <ReportwkshSum>{
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
  };

  bsConfig?: Partial<BsDatepickerConfig>;
  dateFrom_mdat = "";
  dateTo_mdat = "";
  dateTo_eta = "";
  dateFrom_eta = "";
  iconButton = IconButton;




  constructor(
    private service: ReportWkshSumService,
    private spinnerService: NgxSpinnerService,
    private functionUtility: FunctionUtility,
    private snotiftyService: NgSnotifyService
  ) { }

  ngOnInit(): void {
    this.getBrand();
    this.getData();
  }


  getBrand() {
    this.service.getBrand().subscribe({
      next: res => {
        console.log(this.brand = res);
        this.brand = res;
      },
      error: err => { console.log(err) }
    })
  }

  getData() {
    this.param.mdat_start = !this.functionUtility.checkEmpty(this.dateFrom_mdat) ? this.functionUtility.getDateFormat(new Date(this.dateFrom_mdat)) : "";
    this.param.mdat_end = !this.functionUtility.checkEmpty(this.dateTo_mdat) ? this.functionUtility.getDateFormat(new Date(this.dateTo_mdat)) : "";
    this.param.eta_start = !this.functionUtility.checkEmpty(this.dateFrom_eta) ? this.functionUtility.getDateFormat(new Date(this.dateFrom_eta)) : "";
    this.param.eta_end = !this.functionUtility.checkEmpty(this.dateTo_eta) ? this.functionUtility.getDateFormat(new Date(this.dateFrom_eta)) : "";

    this.spinnerService.show();
    this.service.getData(this.pagination, this.param).subscribe({
      next: res => {
        console.log(res.result)
        this.data = res.result;
        this.pagination = res.pagination;
        this.spinnerService.hide();
      }
    })
  }

  export() {
    this.spinnerService.show();
    this.service.exportExcel(this.data1).subscribe({
      next: (res: Blob) => {
        this.functionUtility.exportExcel(res, 'wkshSumReport');
        this.snotiftyService.success("Export Success", "SUCCESS")
      }, error: () => {
        this.snotiftyService.error(
          MessageConstants.NO_DATA,
          CaptionConstants.ERROR
        )
      },
    }).add(() => this.spinnerService.hide());
  }


  pageChanged(e: any) {
    this.pagination.pageNumber = e.page;
    this.getData();
  }



  search() {
    this.pagination.pageNumber = 1;
    this.getData();
  }

  clear() {
    this.param.bitnbr = '';
    this.param.brandname = '';
    this.param.close_status = '';
    this.param.cusna = '';
    this.param.eta_end = '';
    this.param.eta_start = '';
    this.param.kind = '';
    this.param.manno = '';
    this.param.mdat_end = '';
    this.param.mdat_start = '';
    this.param.purno = '';
    this.param.rmodel = '';
    this.dateFrom_mdat = "";
    this.dateTo_mdat = "";
    this.dateFrom_eta = "";
    this.dateTo_eta = "";
    this.getData();
  }

  exportExcel() {

  }
}
