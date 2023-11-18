import { ModalService } from './../../../../_core/services/common/modal.service';
import { IconButton } from './../../../../_core/constants/sd-team.utility';
import { KeyValue } from '@angular/common';
import { Component, OnInit, ViewChild } from '@angular/core';
import { PackingScanDto, PackingScanExportDto, PackingScanViewDto } from '@models/transaction/packing-scan';
import { InjectBase } from '@utilities/inject-base-app';
import { KeyValueUtility } from '@utilities/key-value-utility';
import { Pagination } from '@utilities/pagination-utility';
import { BsDatepickerConfig } from 'ngx-bootstrap/datepicker';
import { PackingScanExportComponent } from 'src/app/views/commons/packing-scan-export/packing-scan-export.component';
import { PackingScanService } from '@services/transaction/packing-scan.service'

import { CommonService } from '@services/common/common.service';
import { OperationResult } from '@utilities/operation-result';
import { document } from 'ngx-bootstrap/utils';
import { NgxPrintElementService } from 'ngx-print-element';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss'],
})
export class MainComponent extends InjectBase implements OnInit {
  @ViewChild('printTemplate') packingScanExportTemplate: PackingScanExportComponent;
  bsConfig: Partial<BsDatepickerConfig> = <Partial<BsDatepickerConfig>>{};

  listShift: KeyValueUtility[] = [];
  scanDate: string = new Date().toDateString();
  shift: string = null;
  listPackingScan: PackingScanViewDto[] = [];
  listDataExport: PackingScanExportDto[] = [];
  iconButton = IconButton;
  transactionNo: string = '';
  pagination: Pagination = <Pagination>{
    pageNumber: 1,
    pageSize: 10
  }

  constructor(
    private service: PackingScanService,
    private modalService: ModalService,
    public printService: NgxPrintElementService,
    private commonService: CommonService,
  ) { super() }

  ngOnInit(): void {
    this.bsConfig = Object.assign(
      {},
      {
        isAnimated: true,
        dateInputFormat: 'YYYY/MM/DD',
      }
    );
    this.getListShift();
  }

  getListShift() {
    this.spinnerService.show();
    this.commonService.getListShift().subscribe({
      next: (res: KeyValueUtility[]) => {
        this.spinnerService.hide();
        this.listShift = res;
      },
      error: (err) => {
        this.snotifyService.error("ERROR")
      },
    })
  }

  getList() {
    this.spinnerService.show();
    this.service.getList(this.pagination, this.transactionNo).subscribe({
      next: res => {
        this.spinnerService.hide;
        this.pagination = res.pagination;
        this.listPackingScan = res.result;
      },
    })
  }

  pageChanged(e: any) {
    this.pagination.pageNumber = e.page;
    this.getList();
  }

  openScanModal(id: string) {
    debugger
    console.log(id);
    this.modalService.open(id);
  }

  modalChange(event: OperationResult) {
    this.spinnerService.show();
    let stringdate = this.functionUtility.getDateFormat(new Date(this.scanDate));
    let data: PackingScanDto = {
      shift: this.shift,
      scan_Date: stringdate,
      listData: event.data
    };
    this.service.saveScanList(data).subscribe({
      next: (result: OperationResult) => {
        if (result.isSuccess) {
          this.spinnerService.hide();
          this.transactionNo = result.data;
          this.getList();
          this.snotifyService.success(
            this.translateService.instant('System.Message.CreateOKMsg'),
            this.translateService.instant('System.Caption.Success'));
        } else {
          this.spinnerService.hide();
          this.snotifyService.error(this.translateService.instant('Transaction.PackingScan.' + result.error), this.translateService.instant('System.Caption.Error'));
        }
      },
      error: () => {
        this.spinnerService.hide();
        this.snotifyService.error(this.translateService.instant('System.Message.UnknowError'), this.translateService.instant('System.Caption.Error'));
      }
    })
  }

  export() {
    document.getElementById('printTemplate').hidden = false;
    this.spinnerService.show();
    this.service.getDataExport(this.transactionNo).subscribe({
      next: (res: PackingScanExportDto[]) => {
        this.spinnerService.hide();
        this.listDataExport = res;
        //cho form cua khi rendered
        this.packingScanExportTemplate.print(this.listDataExport, true);
        this.reset();
      },
      error: (err) => {
        document.getElementById('printTemplate').hidden = true;
        this.spinnerService.hide();
        this.snotifyService.error("ERROR PRINT, PLEASE CHECK DATA");
      },
    })
  }

  reset() {
    this.transactionNo = '';
    this.listPackingScan = [];
    this.listDataExport = []
  }

}
