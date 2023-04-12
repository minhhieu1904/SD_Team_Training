import { CaptionConstants, MessageConstants } from '@constants/message.enum';
import { CommonService } from '@services/common/common.service';
import { StorageSumReportService } from '@services/report/storage-sum-report.service';
import { KeyValueUtility } from '@utilities/key-value-utility';
import { IconButton } from '@constants/common.constants';
import { Report_Storage_Sum, Report_Storage_Sum_Param } from '@models/Report/report_Storage_Sum_Param';
import { InjectBase } from '@utilities/inject-base-app';
import { Component, OnInit } from '@angular/core';
import { Pagination } from '@utilities/pagination-utility';
import { BsDatepickerConfig, BsDatepickerViewMode } from 'ngx-bootstrap/datepicker';
import { DestroyService } from '@services/destroy.service';
import { ExportDetailExcelParams } from '@models/Report/exportDetailExcelParams';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss'],
  providers: [DestroyService]
})
export class MainComponent extends InjectBase implements OnInit {
  iconButton = IconButton
  pagination: Pagination = <Pagination>{pageNumber: 1, pageSize: 10};
  param: Report_Storage_Sum_Param = <Report_Storage_Sum_Param>{
    crday: 'sdat',
    brandname: ''
  }
  paramExportDetailExcel: ExportDetailExcelParams = {
    manno: '',
    purno: '',
    size: '',
  }
  data: Report_Storage_Sum[] = [];
  brandName: KeyValueUtility[] = [];
  maxMdatStartDate: Date;
  minMdatEndDate: Date;
  maxEtaStartDate: Date;
  minEtaEndDate: Date;
  minMode: BsDatepickerViewMode = 'day';
  bsConfig: Partial<BsDatepickerConfig> = <Partial<BsDatepickerConfig>>{
    dateInputFormat: "YYYY/MM/DD",
    minMode: this.minMode
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
  dataChecked: Report_Storage_Sum;
  isCheckItem: boolean = false;

  constructor(
    private service: StorageSumReportService,
    private commonService: CommonService,
  ) { super() }

  ngOnInit() {
    this.getData();
    this.getBranchName();
    // this.clearSearch();
  }

  getBranchName() {
    this.spinnerService.show();
    this.commonService.getListBrandName().subscribe({
      next: res => {
        this.brandName = res;
        this.spinnerService.hide();
      },
      error: () => {
        this.snotifyService.error(CaptionConstants.ERROR, MessageConstants.UN_KNOWN_ERROR);
        this.spinnerService.hide();
      }
    })
  }

  checkDate(){
    this.param.sdat = this.maxMdatStartDate != null ? this.functionUtility.getDateFormat(this.maxMdatStartDate) : ''
    this.param.mdat = this.minMdatEndDate != null ? this.functionUtility.getDateFormat(this.minMdatEndDate) : ''
    this.param.startDate = this.maxEtaStartDate != null ? this.functionUtility.getDateFormat(this.maxEtaStartDate) : ''
    this.param.endDate = this.minEtaEndDate != null ? this.functionUtility.getDateFormat(this.minEtaEndDate) : ''
  }

  getData(){
    this.checkDate();
    this.spinnerService.show();
    this.service.search(this.pagination, this.param).subscribe({
      next: res => {
        this.data = res.result;
        this.pagination = res.pagination;
        this.spinnerService.hide();
      },
      error: () => {
        this.spinnerService.hide();
        this.snotifyService.error(MessageConstants.SYSTEM_ERROR_MSG, CaptionConstants.ERROR);
      }
    })
  }

  clearSearch() {
    this.param = <Report_Storage_Sum_Param>{
      crday: 'sdat'
    };
    this.maxMdatStartDate = null
    this.minMdatEndDate = null
    this.maxEtaStartDate = null
    this.minEtaEndDate = null
    this.data = []
    this.isCheckItem = false;
  }

  search() {
    this.pagination.pageNumber = 1;
    this.getData();
  }

  exportExcel(){
    this.spinnerService.show();
    this.service.exportExcel(this.pagination, this.param).subscribe({
      next: (result: Blob) => {
        this.spinnerService.hide();
        const currentTime = new Date();
        let fileName = "4.3.StorageSumReport_" +
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
        let fileName = "4.3.StorageSumReport_" +
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

  checkRecord(item: Report_Storage_Sum) {
    item.check = !item.check;
    this.isCheckItem = item.check;
    this.data.filter(x => x != item).forEach(x => { x.check = false });
    this.dataChecked = item;
  }

  pageChanged(e: any) {
    this.pagination.pageNumber = e.page;
    this.getData();
  }
}
