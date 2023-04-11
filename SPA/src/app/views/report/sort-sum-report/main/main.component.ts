import { SnotifyService } from 'ng-snotify';
import { ExportDetailExcelParams } from '@models/Report/exportDetailExcelParams';
import { ReportSortSumResult, SearchSortSumReportParams } from '@models/Report/reportSortSumResult';
import { SortSumReportService } from './../../../../_core/services/report/sort-sum-report.service';
import { CaptionConstants, MessageConstants } from '@constants/message.enum';
import { CommonService } from '@services/common/common.service';
import { InjectBase } from '@utilities/inject-base-app'
import { IconButton } from '@constants/common.constants';
import { KeyValueUtility } from '@utilities/key-value-utility';
import { Pagination } from '@utilities/pagination-utility';
import { Component, OnInit } from '@angular/core';
import { BsDatepickerConfig, BsDatepickerViewMode } from 'ngx-bootstrap/datepicker';
import { DestroyService } from '@services/destroy.service';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss'],
  providers: [DestroyService]
})
export class MainComponent extends InjectBase implements OnInit {
  pagination: Pagination = <Pagination>{ pageNumber: 1, pageSize: 10 };
  minMode: BsDatepickerViewMode = 'day';
  bsConfig: Partial<BsDatepickerConfig> = <Partial<BsDatepickerConfig>>{
    dateInputFormat: "YYYY/MM/DD",
    minMode: this.minMode
  }
  param: SearchSortSumReportParams = <SearchSortSumReportParams>{
    crday: 'sdat',
    brandname: ''
  };
  paramExportDetailExcel: ExportDetailExcelParams = {
    manno: '',
    purno: '',
    size: '',
  }
  dateKind = [
    {
      'key': 'sdat',
      'value': 'Sdat Date'
    },
    {
      'key': 'mdat',
      'value': 'Production_Date'
    }
  ]
  data: ReportSortSumResult[] = [];
  iconButton = IconButton;
  maxMdatStartDate: Date;
  minMdatEndDate: Date;
  maxEtaStartDate: Date;
  minEtaEndDate: Date;
  brandNames: KeyValueUtility[] = [];
  dataChecked: ReportSortSumResult;
  isCheckItem: boolean = false;
  constructor(
    private service: SortSumReportService,
    private commonService: CommonService,
  ) { super() }

  ngOnInit() {
    this.getData();
    this.getBranchName();
    this.clearSearch();
  }

  getBranchName() {
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
    this.param.dateStart = this.maxMdatStartDate != null ? this.functionUtility.getDateFormat(this.maxMdatStartDate) : ''
    this.param.dateEnd = this.minMdatEndDate != null ? this.functionUtility.getDateFormat(this.minMdatEndDate) : ''
    this.param.etaFrom = this.maxEtaStartDate != null ? this.functionUtility.getDateFormat(this.maxEtaStartDate) : ''
    this.param.etaTo = this.minEtaEndDate != null ? this.functionUtility.getDateFormat(this.minEtaEndDate) : ''
  }

  getData(){
    this.checkDate();
    this.spinnerService.show();
    this.service.search(this.pagination, this.param).subscribe({
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

  exportExcel(){
    this.spinnerService.show();
    this.service.exportExcel(this.pagination, this.param).subscribe({
      next: (result: Blob) => {
        this.spinnerService.hide();
        const currentTime = new Date();
        let fileName = "4.2.SortSumReport_" +
          currentTime.getFullYear().toString() +
          (currentTime.getMonth() + 1) +
          currentTime.getDate();
        this.functionUtility.exportExcel(result, fileName)
        this.spinnerService.hide()
      },
      error: () => {
        this.spinnerService.hide();
        this.snotifyService.error(MessageConstants.SYSTEM_ERROR_MSG, CaptionConstants.ERROR)}
    })
  }

  exportDetailExcel(){
    let dataChecked = this.data.filter(x => x.check);
    if (dataChecked.length > 0) {
      this.paramExportDetailExcel.manno = this.dataChecked.manno;
      this.paramExportDetailExcel.purno = this.dataChecked.purno;
      this.paramExportDetailExcel.size = this.dataChecked.size;
    }
    this.spinnerService.show();
    this.service.exportDetailExcel(this.paramExportDetailExcel).subscribe({
      next: (result: Blob) => {
        this.spinnerService.hide();
        const currentTime = new Date();
        let fileName = "4.2.SortSumReport_" +
        currentTime.getFullYear().toString() +
        (currentTime.getMonth() + 1) +
        currentTime.getDate();
        this.functionUtility.exportExcel(result, fileName)
        this.spinnerService.hide()
      },
      error: () => {
        this.spinnerService.hide();
        this.snotifyService.error(MessageConstants.SYSTEM_ERROR_MSG, CaptionConstants.ERROR)
      }
    })
  }

  checkRecord(item: ReportSortSumResult) {
    item.check = !item.check;
    this.isCheckItem = item.check;
    this.data.filter(x => x != item).forEach(x => { x.check = false });
    this.dataChecked = item;
  }

  pageChanged(e: any) {
    this.pagination.pageNumber = e.page;
    this.getData();
  }

  clearSearch() {
    this.param = <SearchSortSumReportParams>{
      crday: 'sdat'
    };
    this.maxMdatStartDate = null
    this.minMdatEndDate = null
    this.maxEtaStartDate = null
    this.minEtaEndDate = null
    this.data = []
    this.isCheckItem = false;
  }
}
