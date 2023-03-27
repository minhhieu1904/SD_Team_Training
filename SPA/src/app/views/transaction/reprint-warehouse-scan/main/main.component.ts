import { CommonService } from './../../../../_core/services/common/common.service';
import { KeyValueUtility } from './../../../../_core/utilities/key-value-utility';
import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { InjectBase } from '@utilities/inject-base-app';
import { LocalStorageConstants } from '@constants/local-storage.constants';
import { IconButton } from '@constants/sd-team.utility';
import { BsDatepickerConfig, BsLocaleService } from 'ngx-bootstrap/datepicker';
import { Pagination } from '@utilities/pagination-utility';
import { ReprintWarehouseScan, ReprintWarehouseScanParam } from '@models/transaction/reprint-warehouse-scan';
import { ReprintWarehouseScanService } from '@services/transaction/reprint-warehouse-scan.service';
import { CaptionConstants, MessageConstants } from '@constants/message.enum';
import { PageChangedEvent } from 'ngx-bootstrap/pagination';
import { takeUntil } from 'rxjs';
import { BsModalService } from 'ngx-bootstrap/modal';
import { DestroyService } from '@services/common/destroy.service';

@Component({
  selector: 'app-main',
  standalone: true,
  imports: [CommonModule],
  providers: [DestroyService],
  templateUrl: './main.component.html',
  styleUrl: './main.component.scss'
})
export class MainComponent extends InjectBase implements OnInit {
  language: string = localStorage.getItem(LocalStorageConstants.LANG);
  iconButton = IconButton;
  bsConfig?: Partial<BsDatepickerConfig>;

  pagination: Pagination = <Pagination>{
    pageNumber: 1,
    pageSize: 10
  };

  param: ReprintWarehouseScanParam = <ReprintWarehouseScanParam>{
    location: '',
    department: ''
  };

  dateKind: KeyValueUtility[] = [
    { key: 'sdat', value: 'sdat Date' },
    { key: 'mdat', value: 'mdat Date' }
  ];

  dateFrom: string = null;
  dateTo: string = null;
  locations: KeyValueUtility[] = [];
  departments: KeyValueUtility[] = [];
  dataMain: ReprintWarehouseScan[] = [];
  dataCheck: ReprintWarehouseScan[] = [];
  isCheckAll: boolean = false;

  constructor(
    private service: ReprintWarehouseScanService,
    private commonService: CommonService,
    private bsLocalService: BsLocaleService
  ) { super() }

  ngOnInit(): void {
    this.getListLocation();
    this.getListDepartment();
    this.bsConfig = Object.assign({}, { isAnimated: true, dateInputFormat: 'YYYY/MM/DD' });
    this.functionUtility.setDatepickerLanguage(this.bsLocalService, this.language);
    this.translateService.onLangChange.pipe(takeUntil(this.destroyService.destroys$)).subscribe(res => {
      this.functionUtility.setDatepickerLanguage(this.bsLocalService, res.lang)
    });
  }

  getListLocation() {
    this.spinnerService.show();
    this.commonService.getListLocationName().subscribe({
      next: (res: KeyValueUtility[]) => {
        this.locations = res;
      },
      error: (err) => {
        this.snotifyService.error(MessageConstants.UN_KNOWN_ERROR, CaptionConstants.ERROR)
      },
    }).add(() => this.spinnerService.hide())
  }

  getListDepartment() {
    this.spinnerService.show();
    this.commonService.getListDepartmentName().subscribe({
      next: (res: KeyValueUtility[]) => {
        this.departments = res
      },
      error: (err) => {
        this.snotifyService.error(MessageConstants.UN_KNOWN_ERROR, CaptionConstants.ERROR)
      }
    }).add(() => this.spinnerService.hide())
  }

  getData() {
    this.param.dateFrom = this.dateFrom == null ? '' : this.functionUtility.getDateFormat(new Date(this.dateFrom));
    this.param.dateTo = this.dateTo == null ? '' : this.functionUtility.getDateFormat(new Date(this.dateTo));

    this.spinnerService.show();
    this.service.getData(this.param, this.pagination).subscribe({
      next: res => {
        this.dataMain = res.result.map(item => {
          item.check = false;
          return item;
        });
        this.pagination = res.pagination;
        this.isCheckAll = false
      },
      error: err => {
        this.snotifyService.error(MessageConstants.UN_KNOWN_ERROR, CaptionConstants.ERROR)
      }
    }).add(() => this.spinnerService.hide());
  }

  search() {

  }

  print() {
    this.spinnerService.show();
    this.service.print(this.dataCheck).subscribe({
      next: (res: Blob) => {
        this.functionUtility.print(res);
      },
      error: err => {
        this.snotifyService.error(MessageConstants.UN_KNOWN_ERROR, CaptionConstants.ERROR)
      }
    }).add(() => this.spinnerService.hide())
  }

  clear() {
    this.dateFrom = null;
    this.dateTo = null;
    this.param = <ReprintWarehouseScanParam>{
      location: '',
      department: ''
    },
      this.isCheckAll = false;
    this.dataCheck.length = 0;
    this.dataMain.length = 0;
  }

  checkAll() {
    this.dataMain.map(item => {
      item.check = this.isCheckAll;
      return item;
    });
    this.dataCheck = this.dataMain.filter(x => x.check);
  }

  checkRecord(item: ReprintWarehouseScan) {
    item.check = !item.check;
    this.isCheckAll = this.dataMain.every(x => x.check);
    this.dataCheck = this.dataMain.filter(x => x.check);
  }

  validateSearch(): boolean {
    let isValid: boolean = true;
    if (this.functionUtility.checkEmpty(this.dateFrom) && this.functionUtility.checkEmpty(this.dateTo) &&
      this.functionUtility.checkEmpty(this.param.trno) && this.functionUtility.checkEmpty(this.param.manno) &&
      this.functionUtility.checkEmpty(this.param.purno) && this.functionUtility.checkEmpty(this.param.size) &&
      this.functionUtility.checkEmpty(this.param.location) && this.functionUtility.checkEmpty(this.param.department)) {
      isValid = false;
      this.snotifyService.error(MessageConstants.UN_KNOWN_ERROR, CaptionConstants.ERROR)

      if ((this.dateFrom == null && this.dateTo != null) || (this.dateTo == null || this.dateFrom != null)) {
        isValid = false;
        this.snotifyService.error(MessageConstants.UN_KNOWN_ERROR, CaptionConstants.ERROR)
      }
    }
    return isValid;
  }


  pageChanged(event: PageChangedEvent) {
    this.pagination.pageNumber = event.page;
    this.getData();
  }
}
