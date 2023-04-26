import { PackingScanService } from '@services/Transaction/PackingScan/packing-scan.service';
import { InjectBase } from '@utilities/inject-base-app';
import { PackingScanDTOParam } from '@models/Transaction/PackingScan/PackingScanDTOParam';
import { OperationResult } from '@utilities/operation-result';
import { Pagination, PaginationResult } from '@utilities/pagination-utility';
import { IconButton } from '@constants/common.constants';
import { PackingScanViewDTO } from '@models/Transaction/PackingScan/PackingScanViewDTO';
import { KeyValueUtility } from '@utilities/key-value-utility';
import { CommonService } from '@services/common/common.service';
import { NgxPrintElementService } from 'ngx-print-element';
import { ModalService } from '@services/common/modal.service';
import { Component, OnInit, ViewChild } from '@angular/core';
import { PackingScanExportDTO } from '@models/Transaction/PackingScan/PackingScanExportDTO';
import { PackingScanExportComponent } from '../../../common/packing-scan-export/packing-scan-export.component';
import { LangConstants } from '@constants/lang-constant';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss']
})
export class MainComponent extends InjectBase  implements OnInit {
  @ViewChild('printTemplate') packingScanExportTemplate: PackingScanExportComponent;

  listShift: KeyValueUtility[] = [];

  scanDate: string = new Date().toDateString();
  shift: string = "";
  listPackingScan: PackingScanViewDTO[] = [];
  listDataExport: PackingScanExportDTO[] = [];
  iconButton = IconButton;
  transactionNo: string = '';
  pagination: Pagination = <Pagination>{
    pageNumber: 1,
    pageSize: 100
  };
  lang: string = localStorage.getItem(LangConstants.LANG) != null ? localStorage.getItem(LangConstants.LANG):'en'
  constructor(private service: PackingScanService,
    private modalService: ModalService,
    public printService: NgxPrintElementService,
    private commonService: CommonService) { super();

    }

  ngOnInit(): void {
    this.getListShift();
    this.translateService.onLangChange.subscribe(event => {
      this.lang = event.lang
    });

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
  getList(){
    this.spinnerService.show();
    this.service.getList(this.pagination,this.transactionNo).subscribe({
      next: (result: PaginationResult<PackingScanViewDTO>) => {
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
  modalChange(event : OperationResult)
  {
    this.spinnerService.show();
    let data: PackingScanDTOParam = {
      shift: this.shift,
      listData: event.data
    };
     this.service.saveScanList(data).subscribe({
      next : (result: OperationResult) =>{
        if(result.isSuccess){
          this.spinnerService.hide();
          this.transactionNo = result.data;
          this.getList();
          this.snotifyService.success(
            this.translateService.instant('System.Message.CreateOKMsg'),
            this.translateService.instant('System.Caption.Success'));
        }
        else {
          this.spinnerService.hide();
          this.snotifyService.error(this.translateService.instant('Transaction.PackingScan.' + result.error), this.translateService.instant('System.Caption.Error'));
        }
      }, error: () => {
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
        //wait for template rendered
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
