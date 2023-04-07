import { CaptionConstants, MessageConstants } from '@constants/message.enum';
import { CommonService } from '@services/common/common.service';
import { InjectBase } from '@utilities/inject-base-app'
import { WkshSumReportService } from '@services/report/wksh-sum-report.service';
import { IconButton } from '@constants/common.constants';
import { KeyValueUtility } from '@utilities/key-value-utility';
import { Pagination } from '@utilities/pagination-utility';
import { Report_Wksh_SumResult, WkshSumReportParam } from '@models/report/wksh-sum-report';
import { Component, OnInit } from '@angular/core';
import { BsDatepickerConfig, BsDatepickerViewMode } from 'ngx-bootstrap/datepicker';
import { DestroyService } from '@services/destroy.service';
import { PageChangedEvent } from 'ngx-bootstrap/pagination';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss'],
  providers: [DestroyService]
})
export class MainComponent extends InjectBase implements OnInit {
  pagination: Pagination = <Pagination>{ pageNumber: 1, pageSize: 10 };
  minMode: BsDatepickerViewMode = 'day';
  param: WkshSumReportParam = <WkshSumReportParam>{};
  data: Report_Wksh_SumResult[] = [];
  iconButton = IconButton;
  bsConfig: Partial<BsDatepickerConfig> = <Partial<BsDatepickerConfig>>{
    dateInputFormat: "YYYY/MM/DD",
    minMode: this.minMode
  }
  maxMdatStartDate: Date;
  minMdatEndDate: Date;
  maxEtaStartDate: Date;
  minEtaEndDate: Date;
  closeStatus: KeyValueUtility[] = [
    {key: 'Y', value: 'Y'},
    {key: 'N', value: 'N'}
  ];
  brandNames: KeyValueUtility[] = [];
  constructor(
    private wkshSumReportService: WkshSumReportService,
    private commonService: CommonService,
  ) { super() }

  ngOnInit() {
    this.clearSearch();
    this.getData();
    this.getListBranchName();
  }

  getListBranchName() {
    this.spinnerService.show();
    this.commonService.getListBrandName().subscribe({
      next: res => {
        this.brandNames = res;
        this.spinnerService.hide();
      },
      error: () => {
        this.snotifyService.error(CaptionConstants.ERROR, MessageConstants.UN_KNOWN_ERROR);
        this.spinnerService.hide();
      }
    })
  }

  search() {
    this.pagination.pageNumber = 1;
    this.getData();
  }

  checkDate(){
    this.param.mdat_start = this.maxMdatStartDate != null ? this.functionUtility.getDateFormat(this.maxMdatStartDate) : ''
    this.param.mdat_end = this.minMdatEndDate != null ? this.functionUtility.getDateFormat(this.minMdatEndDate) : ''
    this.param.eta_start = this.maxEtaStartDate != null ? this.functionUtility.getDateFormat(this.maxEtaStartDate) : ''
    this.param.eta_end = this.minEtaEndDate != null ? this.functionUtility.getDateFormat(this.minEtaEndDate) : ''
  }

  getData(){
    this.checkDate();
    this.spinnerService.show();
    this.wkshSumReportService.searchWithPagination(this.pagination, this.param).subscribe({
      next: res => {
        this.spinnerService.hide();
        this.data = res.result;
        this.pagination = res.pagination;
      },
      error: () => {
        this.spinnerService.hide();
        this.snotifyService.error(MessageConstants.SYSTEM_ERROR_MSG, CaptionConstants.ERROR);
      }
    })
  }

  pageChanged(e: any) {
    this.pagination.pageNumber = e.page;
    this.getData();
  }

  export(){
    this.checkDate();
    this.wkshSumReportService.exportExcel(this.pagination, this.param).subscribe({
      next: (result: Blob) => {
        this.spinnerService.hide();
        const currentTime = new Date();
        let fileName =
          '4.1.WkshSumReport_' +
          currentTime.getFullYear().toString() +
          (currentTime.getMonth() + 1) +
          currentTime.getDate();

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

  clearSearch() {
    this.param = <WkshSumReportParam>{
      bitnbr: '',
      brandname: '',
      close_status: '',
      cusna: '',
      eta_end: '',
      eta_start: '',
      kind: '',
      manno: '',
      mdat_end: '',
      mdat_start: '',
      purno: '',
      rmodel: '',
      size: '',
      tolcls: ''
    };
    this.data = []
  }
}
