import { Component, OnInit, ViewChild } from '@angular/core';
import { PackingScanService } from './../../../../_core/services/packing-scan.service'
import { InjectBase } from '@utilities/inject-base-app';
import { IconButton } from '@constants/common.constants';
import { TransactionCommonService } from './../../../../_core/services/common/transaction-common.service'
import { KeyValueUtility } from '@utilities/key-value-utility';
import { Pagination, PaginationResult } from '@utilities/pagination-utility';
import { PackingScanDTO, PackingScanExportDTO, ViewDataPackingScan } from '@models/transaction/packing-scan';
import { PackingScanExportComponent } from '../../packing-scan-export/packing-scan-export.component';
import { OperationResult } from '@utilities/operation-result';
import { ModalService } from './../../../../_core/services/common/modal.service'
import { NgxPrintElementService } from 'ngx-print-element';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss']
})
export class MainComponent extends InjectBase implements OnInit {
  @ViewChild('printTemplate') packingScanExportTemplate: PackingScanExportComponent;
  listShift: KeyValueUtility[] = [];
  scanDate = new Date().toDateString();
  shift: string = null;
  listPackingScan: ViewDataPackingScan[] = [];
  listDataExport: PackingScanExportDTO[] = [];
  iconButton = IconButton;
  transactionNo: string = '';
  pagination: Pagination = <Pagination>{
    pageNumber: 1,
    pageSize: 100
  };


  constructor(
    private service: PackingScanService,
    private modalService: ModalService,
    public printService: NgxPrintElementService,
    private commonService: TransactionCommonService
  ) {
    super();
  }

  ngOnInit(): void {
    this.getListShift();
  }

  getListShift() {
    this.spinnerService.show();
    this.commonService.getListShift().subscribe({
      next: (result: KeyValueUtility[]) => {
        this.spinnerService.hide();
        this.listShift = result;
      },
      error: () => {
        this.spinnerService.hide();
        this.snotifyService.error(this.translateService.instant('System.Message.UnknowError'), this.translateService.instant('System.Caption.Error'));
      }
    });
  }

  getList() {
    this.spinnerService.show();
    this.service.getList(this.pagination, this.transactionNo).subscribe({
      next: (result: PaginationResult<ViewDataPackingScan>) => {
        this.spinnerService.hide();
        this.listPackingScan = result.result;
        this.pagination = result.pagination;
      },
      error: () => {
        this.spinnerService.hide();
        this.snotifyService.error(this.translateService.instant('System.Message.UnknowError'), this.translateService.instant('System.Caption.Error'));
      }
    })
  }

  pageChanged(e: any) {
    this.pagination.pageNumber = e.page;
    this.getList();
  }

  openScanModal(id: string) {
    this.modalService.open(id);
  }

  modalChange(event) {
    this.spinnerService.show();
    let data: PackingScanDTO = {
      shift: this.shift,
      listData: event.data
    };
    this.service.saveScanList(data).subscribe({
      next: (result: OperationResult) => {
        if (result.isSuccess) {
          this.spinnerService.hide();
          this.transactionNo = result.data;
          this.getList();
        } else {
          this.spinnerService.hide();
          this.snotifyService.error(this.translateService.instant('System.Message.CreateErrorMsg'), this.translateService.instant('System.Caption.Error'));
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
      next: (result: PackingScanExportDTO[]) => {
        this.spinnerService.hide();
        this.listDataExport = result;
        this.packingScanExportTemplate.print(this.listDataExport, true);
        this.reset();
      },
      error: () => {
        document.getElementById('printTemplate').hidden = true;
        this.spinnerService.hide();
        this.snotifyService.error(this.translateService.instant('System.Message.UnknowError'), this.translateService.instant('System.Caption.Error'));
      }
    })
  }

  reset() {
    this.transactionNo = '';
    this.listPackingScan = [];
    this.listDataExport = [];
  }
}
