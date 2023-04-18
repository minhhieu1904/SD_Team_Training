import { Component, OnInit } from '@angular/core';
import { InjectBase } from '@utilities/inject-base-app';
import { CheckSumMissDetailReportDTO } from '@models/report/checkSumMissReportDTO';
import { Pagination } from '@utilities/pagination-utility';
import { CheckSumMissReportParam } from '@models/report/checkSumMissReportParam';
import { IconButton } from '@constants/common.constants';
import { BsDatepickerConfig } from 'ngx-bootstrap/datepicker';
import { CaptionConstants, MessageConstants } from '@constants/message.enum';
import { QrcodeWipReportService } from "../../../../_core/services/report/qrcode-wip-report.service";
@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss']
})
export class MainComponent extends InjectBase implements OnInit {

  data: CheckSumMissDetailReportDTO[] = [];
  param: CheckSumMissReportParam;

  iconButton = IconButton;
  bsConfig?: Partial<BsDatepickerConfig>;
  dateFrom_mdat;
  dateTo_mdat;
  pagination: Pagination = <Pagination>{
    pageNumber: 1,
    pageSize: 10,
  };

  constructor(private service: QrcodeWipReportService) {
    super();
  }

  ngOnInit(): void {
    this.createParam();
    this.getData();
  }

  getData() {
    this.checkDate();
    this.spinnerService.show();
    this.service.getData(this.pagination, this.param).subscribe({
      next: (result) => {
        this.spinnerService.hide();
        console.log(result.result);
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
          'QRcodeWipReport' +
          curDate.getFullYear().toString() +
          (curDate.getMonth() + 1) +
          curDate.getDate();
        this.functionUtility.exportExcel(result, fileName);
      },
      error: () => {
        this.spinnerService.hide();
        this.snotifyService.error(
          MessageConstants.SYSTEM_ERROR_MSG,
          CaptionConstants.ERROR
        );
      },
    });
  }

  clear() {
    this.createParam();
    this.getData();
  }

  search() {
    this.pagination.pageNumber = 1;
    this.getData();
  }

  createParam() {
    this.param = {
      rmodel: '',
      tolcls: '',
      mdat_start: '',
      mdat_end: '',
    };
    this.dateFrom_mdat = '';
    this.dateTo_mdat = '';
  }

  checkDate() {
    this.param.mdat_start = !this.functionUtility.checkEmpty(this.dateFrom_mdat)
      ? this.functionUtility.getDateFormat(new Date(this.dateFrom_mdat))
      : '';
    this.param.mdat_end = !this.functionUtility.checkEmpty(this.dateTo_mdat)
      ? this.functionUtility.getDateFormat(new Date(this.dateTo_mdat))
      : '';
  }

  pageChanged(e: any) {
    this.pagination.pageNumber = e.page;
    this.getData();
  }

}
