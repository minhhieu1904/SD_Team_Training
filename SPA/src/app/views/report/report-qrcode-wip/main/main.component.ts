
import { Pagination } from '@utilities/pagination-utility';
import { BsDatepickerConfig } from 'ngx-bootstrap/datepicker';
import { ReportQRCODEWIPParam,QRCODEWIPDetailReportDTO } from '@models/report/QRCODE_WIP';
import { InjectBase } from '@utilities/inject-base-app';
import { Component, OnInit } from '@angular/core';
import { ReportQRCODEWIPService } from '@services/Report/Report-QRCODE-WIP.service';
import { CaptionConstants, MessageConstants } from '@constants/message.enum';
import { IconButton } from '@constants/common.constants';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss']
})
export class MainComponent extends InjectBase  implements OnInit {

  constructor(private service : ReportQRCODEWIPService) {super(); }

  ngOnInit(): void {
    this.search();

  }
  iconButton = IconButton;
  param: ReportQRCODEWIPParam = <ReportQRCODEWIPParam>{
    rmodel: '',
    tolcls: '',
    mdat_start: '',
    mdat_end: ''
  };
  data : QRCODEWIPDetailReportDTO[] = [];
  bsConfig?: Partial<BsDatepickerConfig>;
  pagination: Pagination = <Pagination>{
    pageNumber: 1,
    pageSize: 10,
  };
  dateFrom_mdat = '';
  dateTo_mdat = '';
  search() {
    this.pagination.pageNumber === 1
      ? this.getData()
      : (this.pagination.pageNumber = 1);
  }
  pageChanged(e: any) {
    this.pagination.pageNumber = e.page;
    this.getData();
  }
  clear() {
    this.param.rmodel = '',
    this.param.tolcls = '',
    this.param.mdat_start = '',
    this.param.mdat_end = '',
    this.dateFrom_mdat = '';
    this.dateTo_mdat = '';
    this.getData();
  }
  getData() {
    this.param.mdat_start  = !this.functionUtility.checkEmpty(this.dateFrom_mdat )
    ? this.functionUtility.getDateFormat(new Date(this.dateFrom_mdat))
    : '';
  this.param.mdat_end  = !this.functionUtility.checkEmpty(this.dateTo_mdat)
    ? this.functionUtility.getDateFormat(new Date(this.dateTo_mdat))
    : '';
    this.spinnerService.show();
    this.service.getData(this.pagination, this.param).subscribe({
      next: (result) => {
        console.log(result);
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


  export() {
    this.param.mdat_start  = !this.functionUtility.checkEmpty(this.dateFrom_mdat )
    ? this.functionUtility.getDateFormat(new Date(this.dateFrom_mdat))
    : '';
  this.param.mdat_end  = !this.functionUtility.checkEmpty(this.dateTo_mdat)
    ? this.functionUtility.getDateFormat(new Date(this.dateTo_mdat))  : '';
    this.spinnerService.show();
    this.service
      .exportExcel(this.param)
      .subscribe({
        next: (result: Blob) => {
          this.functionUtility.exportExcel(result, 'Report QRCODE WIP');
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
}
