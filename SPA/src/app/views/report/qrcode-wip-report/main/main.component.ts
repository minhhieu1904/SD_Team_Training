import { Component, OnInit } from '@angular/core';
import { IconButton } from '@constants/common.constants';
import { InjectBase } from '@utilities/inject-base-app';
import { QrcodeWipReportService } from '@services/report/qrcode-wip-report.service';
import { QRCodeParam } from '@models/qRCodeParam';
import { CheckSumMissDetailDTO, CheckSumMissHeaderDTO } from '@models/qRCodeDTO';
import { BsDatepickerConfig } from 'ngx-bootstrap/datepicker';
import { Pagination } from '@utilities/pagination-utility';
import { CaptionConstants, MessageConstants } from '@constants/message.enum';


@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss']
})
export class MainComponent extends InjectBase implements OnInit {
  iconButton = IconButton
  data: CheckSumMissDetailDTO[] = [];
  pagination: Pagination = <Pagination>{
    pageNumber: 1,
    pageSize: 10,
  }
  param: QRCodeParam = <QRCodeParam>{
    rmodel: '',
    tolcls: '',
    mdat_start: '',
    mdat_end: ''
  }
  bsConfig? : Partial<BsDatepickerConfig>;
  mdat_start: '';
  mdat_end: '';
  constructor(private service: QrcodeWipReportService) {
    super();
  }
  
  ngOnInit(): void {
    this.getData();
  }
  pageChanged(e: any) {
    this.pagination.pageNumber = e.page;
    this.getData();
  }
  search(){
    this.pagination.pageNumber = 1;
    this.getData();
  }
  clear(){
    this.param = <QRCodeParam>{
      rmodel: '',
      tolcls: '',
      mdat_start: '',
      mdat_end: ''
    }
    this.getData();
  }
  getData(){
    this.param.mdat_start = !this.functionUtility.checkEmpty(this.mdat_start) ? this.functionUtility.getDateFormat(new Date(this.mdat_start)) : "";
    this.param.mdat_end = !this.functionUtility.checkEmpty(this.mdat_end) ? this.functionUtility.getDateFormat(new Date(this.mdat_end)) : "";
    this.spinnerService.show();
    this.service.getData(this.pagination, this.param).subscribe({
      next: res => {
        this.data = res.result,
        this.pagination = res.pagination,
        this.spinnerService.hide()
      }, error: () => {
        this.snotifyService.error(MessageConstants.UPLOAD_ERROR_MSG, CaptionConstants.ERROR);
      },
    })
  }
  exportExcel(){
    this.param.mdat_start = !this.functionUtility.checkEmpty(this.mdat_start) ? this.functionUtility.getDateFormat(new Date(this.mdat_start)) : "";
    this.param.mdat_end = !this.functionUtility.checkEmpty(this.mdat_end) ? this.functionUtility.getDateFormat(new Date(this.mdat_end)) : "";
    this.spinnerService.show();
    this.service.exportExcel(this.param).subscribe({
      next: res => {
        this.spinnerService.hide();
        const curDate = new Date();
        this.functionUtility.exportExcel(res, "QRCodeWipReport");
      }, error: () => {
        this.spinnerService.hide();
        this.snotifyService.error(
          MessageConstants.SYSTEM_ERROR_MSG,
          CaptionConstants.ERROR
        )
      }
    })
  }
}
