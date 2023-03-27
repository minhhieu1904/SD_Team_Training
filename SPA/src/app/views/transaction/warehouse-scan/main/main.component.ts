import { IconButton } from '@constants/sd-team.utility';
import { WarehouseScanService } from './../../../../_core/services/transaction/warehouse-scan.service';
import { Component, OnInit } from '@angular/core';
import { InjectBase } from '@utilities/inject-base-app';
import { BsDatepickerConfig } from 'ngx-bootstrap/datepicker';
import { BsModalRef, BsModalService, ModalOptions } from 'ngx-bootstrap/modal';
import { KeyValueUtility } from '@utilities/key-value-utility';
import { MS_QR_Storage } from '@models/transaction/warehouse-scan';
import { Pagination } from '@utilities/pagination-utility';
import { CommonService } from '@services/common/common.service';
import { CaptionConstants, MessageConstants } from '@constants/message.enum';
import { FormComponent } from '../form/form.component';


@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss']
})
export class MainComponent extends InjectBase implements OnInit {
  bsModalRef?: BsModalRef;
  bsConfig: Partial<BsDatepickerConfig> = <Partial<BsDatepickerConfig>>{};

  iconButton = IconButton;
  locations: KeyValueUtility[] = [];
  location: string = null;
  departments: KeyValueUtility[] = [];
  department: string = null;
  scanDate: string = new Date().toDateString();
  dataStorage: MS_QR_Storage[] = [];
  trNo: string = '';
  pagination: Pagination = <Pagination>{
    pageNumber: 1,
    pageSize: 10
  };

  constructor(
    private service: WarehouseScanService,
    private modalService: BsModalService,
    private commonService: CommonService
  ) { super() }

  ngOnInit(): void {
    this.bsConfig = Object.assign(
      {},
      {
        isAnimated: true,
        dateInputFormat: 'YYYY/MM/DD',
      }
    );
    this.getLocation();
    this.getDepartment();
  }

  getLocation() {
    this.spinnerService.show();
    this.commonService.getListLocationName().subscribe({
      next: res => {
        this.locations = res
      },
      error: (err) => {
        this.snotifyService.error(MessageConstants.UN_KNOWN_ERROR, CaptionConstants.ERROR)
      },
    }).add(() => this.spinnerService.hide())
  }

  getDepartment() {
    this.spinnerService.show();
    this.commonService.getListDepartmentName().subscribe({
      next: res => {
        this.departments = res;
      },
      error: (err) => {
        this.snotifyService.error(MessageConstants.UN_KNOWN_ERROR, CaptionConstants.ERROR)
      },
    })
  }

  scanCode(location: string, department: string) {
    const intialState: ModalOptions = {
      class: 'modal-dialog-centered modal-lg',
      initialState: {
        location, department
      } as Partial<Object>
    };
    this.service.setParamForm(this.scanDate);
    this.bsModalRef = this.modalService.show(FormComponent, intialState);
    this.bsModalRef.content.scanQRCode.subscribe((trNo: string) => {
      this.trNo = trNo;
      this.getListWarehouseScan();
    })
  }

  getListWarehouseScan() {
    this.service.getListWarehouseScan(this.pagination, this.trNo).subscribe({
      next: res => {
        this.pagination = res.pagination;
        this.dataStorage = res.result;
      },
      error: (err) => {
        this.snotifyService.error(MessageConstants.UN_KNOWN_ERROR, CaptionConstants.ERROR)
      },
    }).add(() => this.spinnerService.hide())
  }

  export() {
    this.spinnerService.show();
    this.service.exportReport(this.trNo).subscribe({
      next: (res: Blob) => {
        this.functionUtility.print(res);
        this.dataStorage = []
      },
      error: (err) => {
        this.snotifyService.error(MessageConstants.UPDATED_ERROR_MSG, CaptionConstants.ERROR)
      },
    })
  }

  pageChanged(event: any) {
    this.pagination.pageNumber = event.page;
    this.getListWarehouseScan();
  }
}
