import { QrcodeWipReportService } from './../../../../_core/services/report/qrcode-wip-report.service';
import { QRcodeWIPReportParam } from './../../../../_core/models/Report/qRCode_WIP_Report/qRcodeWIPReportParam';
import { Component, OnInit } from '@angular/core';
import { IconButton } from '@constants/common.constants';
import { InjectBase } from '@utilities/inject-base-app';
import { BsDatepickerConfig, BsDatepickerViewMode, BsLocaleService } from 'ngx-bootstrap/datepicker';
import { Pagination } from '@utilities/pagination-utility';
import { ReportCheckSumMissDetail } from '@models/Report/qRCode_WIP_Report/reportQRcodeWIPDto';
import { CaptionConstants, MessageConstants } from '@constants/message.enum';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss']
})
export class MainComponent extends InjectBase implements OnInit {
  iconButton = IconButton;
  param: QRcodeWIPReportParam = <QRcodeWIPReportParam>{
    moldno: '',
    toolno: '',
    mdat_start: '',
    mdat_end: '',
  }
  minMode: BsDatepickerViewMode = 'day';
  bsConfig: Partial<BsDatepickerConfig> = <Partial<BsDatepickerConfig>>{
    dateInputFormat: "YYYY/MM/DD",
    minMode: this.minMode
  }
  maxMdatStartDate: Date;
  minMdatEndDate: Date;
  pagination: Pagination = <Pagination>{
    pageNumber: 1,
    pageSize: 10
  }
  data: ReportCheckSumMissDetail[] = [];

  constructor(
    private service: QrcodeWipReportService,
    private bsLocaleService: BsLocaleService
  ) { super() }

  ngOnInit(): void {
    this.getData();
  }

  getData() {
    this.spinnerService.show();
    this.param.mdat_start = this.maxMdatStartDate != null ? this.functionUtility.getDateFormat(this.maxMdatStartDate) : '';
    this.param.mdat_end = this.minMdatEndDate != null ? this.functionUtility.getDateFormat(this.minMdatEndDate) : '';

    this.service.search(this.pagination, this.param)
      .subscribe({
        next: (res) => {
          this.data = res.details.result;
          this.pagination = res.details.pagination;
          this.spinnerService.hide();
        }, error: () => {
          this.snotifyService.error(this.translateService.instant('System.Message.UnknowError'), this.translateService.instant('System.Caption.Error'))
          this.spinnerService.hide();
        }
      })
  }

  search() {
    this.pagination.pageNumber == 1 ? this.getData() : this.pagination.pageNumber = 1;
  }

  clearSearch() {
    this.param = <QRcodeWIPReportParam>{
      moldno: '',
      toolno: ''
    };
    this.maxMdatStartDate = null
    this.minMdatEndDate = null
    this.getData();
  }

  exportExcel() {
    this.spinnerService.show();
    this.service.exportExcel(this.pagination, this.param).subscribe({
      next: (result: Blob) => {
        const currentTime = new Date();
        const fileName = '4.4QRCodeWIPReport_'
          + currentTime.getFullYear().toString()
          + (currentTime.getMonth() + 1)
          + currentTime.getDate();
        this.functionUtility.exportExcel(result, fileName);
        this.spinnerService.hide();
      },
      error: () => {
        this.spinnerService.hide();
        this.snotifyService.error(MessageConstants.SYSTEM_ERROR_MSG, CaptionConstants.ERROR)}
    })
  }

  pageChanged(event: any){
    this.pagination.pageNumber = event.page;
    this.getData();
  }
}
