import { Component, OnInit } from '@angular/core';
import { Pagination } from '@utilities/pagination-utility';
import { CheckSumMissDetailReportDTO, CheckSumMissReportParam } from '@models/report/report_QRCode_WIP';
import { ReportQrcodeWipService } from '@services/report/report-qrcode-wip.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { SnotifyService } from 'ng-snotify';
import { NgSnotifyService } from '@services/common/ng-snotify.service';
import { CaptionConstants, MessageConstants } from '@constants/message.enum';
import { FunctionUtility } from '@utilities/function-utility';
import { IconButton } from '@constants/common.constants';
import { BsDatepickerConfig } from 'ngx-bootstrap/datepicker';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss']
})
export class MainComponent implements OnInit {

  data: CheckSumMissDetailReportDTO[] = [];
  pagination: Pagination = <Pagination>{
    pageNumber: 1,
    pageSize: 10
  }
  param: CheckSumMissReportParam = <CheckSumMissReportParam>{
    mdat_end: '',
    mdat_start: '',
    rmodel: '',
    tolcls: ''
  }

  bsConfig?: Partial<BsDatepickerConfig>;
  mdat_start: '';
  mdat_end: '';
  iconButton = IconButton;

  constructor(
    private service: ReportQrcodeWipService,
    private spinerService: NgxSpinnerService,
    private snotifyService: NgSnotifyService,
    private functionUtility: FunctionUtility,
  ) { }

  ngOnInit(): void {
    this.getData();
  }


  getData() {
    this.param.mdat_start = !this.functionUtility.checkEmpty(this.mdat_start) ? this.functionUtility.getDateFormat(new Date(this.mdat_start)) : "";
    this.param.mdat_end = !this.functionUtility.checkEmpty(this.mdat_end) ? this.functionUtility.getDateFormat(new Date(this.mdat_end)) : "";
    this.spinerService.show();
    this.service.getData(this.pagination, this.param).subscribe({
      next: res => {
        this.data = res.result,
          this.pagination = res.pagination
      },
      error: () => {
        this.snotifyService.error(MessageConstants.UPLOAD_ERROR_MSG, CaptionConstants.ERROR);
      },
      complete: () => {
        this.spinerService.hide();
      }
    })
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
    this.mdat_end = '';
    this.mdat_start = '';
    this.param.rmodel = '';
    this.param.tolcls = '';
    this.getData();
  }

  exportExcel() {
    this.spinerService.show();
    this.service.exportExcel(this.param).subscribe({
      next: (res: Blob) => {
        this.functionUtility.exportExcel(res, 'CheckSumMissReport');
        this.snotifyService.success("EXPORT SUCCESS", "SUCCESS");
      },
      error: () => {
        this.snotifyService.error("EXPORT ERROR", "ERROR");
      },
      complete: () => {
        this.spinerService.hide();
      }
    })
  }
}
