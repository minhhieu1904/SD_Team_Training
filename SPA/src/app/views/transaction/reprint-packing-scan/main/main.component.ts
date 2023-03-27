import { ReprintPackingScanModel, ReprintPackingScanParam } from '@models/transaction/reprint-packing-scan';
import { IconButton } from '@constants/sd-team.utility';
import { Component, OnInit, ViewChild } from '@angular/core';
import { LocalStorageConstants } from '@constants/local-storage.constants';
import { InjectBase } from '@utilities/inject-base-app';

import { KeyValueUtility } from '@utilities/key-value-utility';
import { PackingScanExportComponent } from 'src/app/views/commons/packing-scan-export/packing-scan-export.component';
import { PackingScanExportDto } from '@models/transaction/packing-scan';
import { ReprintPackingScanService } from '@services/transaction/reprint-packing-scan.service'
import { Pagination } from '@utilities/pagination-utility';
import { BsDatepickerConfig, BsLocaleService } from 'ngx-bootstrap/datepicker';
import { CommonService } from '@services/common/common.service';
import { CaptionConstants, MessageConstants } from '@constants/message.enum';
import { PageChangedEvent } from 'ngx-bootstrap/pagination';
import { takeUntil } from 'rxjs';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss']
})
export class MainComponent extends InjectBase implements OnInit {
  @ViewChild('printTemplate') packingScanExportTemplate: PackingScanExportComponent
  iconButton = IconButton;
  language: string = localStorage.getItem(LocalStorageConstants.LANG);
  listShift: KeyValueUtility[] = [];
  listData: ReprintPackingScanModel[] = [];
  dataCheck: ReprintPackingScanModel[] = [];
  listDataExport: PackingScanExportDto[] = [];
  pagination: Pagination = <Pagination>{
    pageNumber: 1,
    pageSize: 10
  };
  startDate: Date;
  endDate: Date;
  listDateKind: KeyValueUtility[] = [
    {
      key: 'sdat',
      value: 'sdat Date'
    },
    {
      key: 'mdat',
      value: 'mdat Date'
    }
  ];
  filterParam: ReprintPackingScanParam = <ReprintPackingScanParam>{}
  isCheckAll: boolean = false;
  bsConfig: Partial<BsDatepickerConfig> = <Partial<BsDatepickerConfig>>{};
  constructor(
    private bsLocaleService: BsLocaleService,
    private reprintPackingScanService: ReprintPackingScanService,
    private commonService: CommonService
  ) { super() }



  ngOnInit(): void {
    this.bsConfig = Object.assign({},
      {
        isAnimated: true,
        dateInputFormat: 'YYYY/MM/DD'
      });
    this.functionUtility.setDatepickerLanguage(this.bsLocaleService, this.language);
    this.translateService.onLangChange.pipe(takeUntil(this.destroyService.destroys$)).subscribe(res => {
      this.functionUtility.setDatepickerLanguage(this.bsLocaleService, res.lang);
    });
    this.getListShift();
  }

  getListShift() {
    this.spinnerService.show();
    this.commonService.getListShift().subscribe({
      next: (res: KeyValueUtility[]) => {
        this.spinnerService.hide();
        this.listShift = res;
      },
      error: err => {
        this.snotifyService.error(this.translateService.instant('System.Message.UnknownError'),
          this.translateService.instant('System.Caption.Error'));
      }
    });
  }

  search() {
    if (this.validateSearch()) {
      this.pagination.pageNumber === 1 ? this.getData() : this.pagination.pageNumber = 1;
    }
  }

  clearSearch() {
    this.isCheckAll = false;
    this.startDate = null;
    this.endDate = null;
    this.filterParam = <ReprintPackingScanParam>{}
    this.listData.length = 0;
    this.dataCheck.length = 0;
    this.pagination = <Pagination>{
      pageNumber: 1,
      pageSize: 10
    }
  }

  export() {
    this.spinnerService.show();
    document.getElementById('printTemplate').hidden = false;
    this.reprintPackingScanService.getDataReprint(this.dataCheck).subscribe({
      next: (res: PackingScanExportDto[]) => {
        this.spinnerService.hide();
        this.listDataExport = res;
        //cho template hien len
        this.packingScanExportTemplate.print(this.listDataExport, false);
      },
      error: err => {
        document.getElementById('printTemplate').hidden = true;
        this.spinnerService.hide();
        this.snotifyService.error(this.translateService.instant('System.Message.UnknownError'),
          this.translateService.instant('System.Caption.Error'));
      }
    })
  }

  pageChanged(event: PageChangedEvent) {
    this.pagination.pageNumber = event.page;
    // this.getData();
  }

  validateSearch(): boolean {
    let isValid: boolean = true;
    if ((this.functionUtility.checkEmpty(this.startDate) && this.functionUtility.checkEmpty(this.endDate)) &&
      this.functionUtility.checkEmpty(this.filterParam.manNo) &&
      this.functionUtility.checkEmpty(this.filterParam.purNo) &&
      this.functionUtility.checkEmpty(this.filterParam.size) &&
      this.functionUtility.checkEmpty(this.filterParam.trno) &&
      this.functionUtility.checkEmpty(this.filterParam.shift)) {
      isValid = false;
      this.snotifyService.error(this.translateService.instant('Transaction.ReprintPackingScan.PleaseEnterField'),
        this.translateService.instant('System.Caption.Error'));
    }

    if ((this.functionUtility.checkEmpty(this.startDate) && this.functionUtility.checkEmpty(this.endDate)) ||
      (!this.functionUtility.checkEmpty(this.startDate) && this.functionUtility.checkEmpty(this.endDate))) {
      isValid = false;
      this.snotifyService.error(this.translateService.instant('Transaction.ReprintPackingScan.PleaseEnterField'),
        this.translateService.instant('System.Caption.Error'))
    }
    return isValid;
  }

  getData() {
    if (!this.functionUtility.checkEmpty(this.startDate) && this.functionUtility.checkEmpty(this.endDate)) {
      this.filterParam.startDate = this.functionUtility.getDateFormat(this.startDate);
      this.filterParam.endDate = this.functionUtility.getDateFormat(this.endDate);
    }
    this.spinnerService.show();
    this.reprintPackingScanService.getData(this.pagination, this.filterParam).subscribe({
      next: (res) => {
        this.listData = res.result.map(item => {
          item.check = false;
          return item;
        });
        this.pagination = res.pagination;
        this.isCheckAll = false;
        this.spinnerService.hide();
      },
      error: () => {
        this.spinnerService.hide();
        this.snotifyService.error(this.translateService.instant('System.Message.UnknownError'),
          this.translateService.instant('System.Caption.Error'))
      }
    })
  }

  checkAll() {
    this.listData.map(item => {
      item.check = this.isCheckAll;
      return item;
    });
    this.dataCheck = this.listData.filter(x => x.check);
  }

  checkRecord(item: ReprintPackingScanModel) {
    item.check = !item.check;
    this.isCheckAll = this.listData.every(x => x.check);
    this.dataCheck = this.listData.filter(x => x.check);
  }
}
