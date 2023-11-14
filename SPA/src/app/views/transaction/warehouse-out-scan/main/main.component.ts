
import { Component, OnInit } from '@angular/core'
import { IconButton } from '@constants/sd-team.utility'
import { BsDatepickerConfig, BsLocaleService } from 'ngx-bootstrap/datepicker'
import { LocalStorageConstants } from '@constants/local-storage.constants'
import { Pagination } from '@utilities/pagination-utility'
import { WarehouseOutScan } from '@models/transaction/warehouse-out-scan'
import { InjectBase } from '@utilities/inject-base-app'
import { WarehouseOutScanService } from '@services/transaction/warehouse-out-scan.service'
import { PageChangedEvent } from 'ngx-bootstrap/pagination'
import { takeUntil } from 'rxjs'
import { WarehouseOutScanParam, WarehouseOutScanSource } from '@helpers/params/transaction/warehouseOutScanParam'
import { PickingDetailParam } from '@helpers/params/transaction/pickingScanParam'


@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss']
})
export class MainComponent extends InjectBase implements OnInit {
  filterParam: WarehouseOutScanParam = <WarehouseOutScanParam>{};
  startDate: Date
  endDate: Date
  iconButton = IconButton
  language: string = localStorage.getItem(LocalStorageConstants.LANG)
  bsConfig: Partial<BsDatepickerConfig> = <Partial<BsDatepickerConfig>>{
    isAnimated: true,
    dateInputFormat: 'YYYY/MM/DD'
  }

  pagination: Pagination = <Pagination>{
    pageNumber: 1,
    pageSize: 10
  }

  dataMain: WarehouseOutScan[] = []
  dataCheck: WarehouseOutScan[] = []
  isCheckAll: boolean = false
  isButtonStorageOut: boolean = false
  isButtonExport: boolean = false
  constructor(
    private bsLocaleService: BsLocaleService,
    private warehouseOutScanService: WarehouseOutScanService
  ) {
    super()
  }

  ngOnInit(): void {
    this.functionUtility.setDatepickerLanguage(this.bsLocaleService, this.language);
    this.translateService.onLangChange.pipe(takeUntil(this.destroyService.destroys$)).subscribe(res => {
      this.functionUtility.setDatepickerLanguage(this.bsLocaleService, res.lang)
    })
    this.loadCondition();
  }

  async loadCondition() {
    await this.warehouseOutScanService.currentLayout.subscribe({
      next: (res) => {
        if (res) {
          console.log("CC:", this.filterParam);
          this.filterParam = res.param;
          this.pagination.pageNumber = res.currentPage;
        }
      },
    })
    this.getDataMain()
  }

  getDataMain() {
    this.filterParam.releaseStartDate =
      this.startDate != null
        ? this.functionUtility.getDateFormat(new Date(this.startDate))
        : ''
    this.filterParam.releaseEndDate =
      this.endDate != null
        ? this.functionUtility.getDateFormat(new Date(this.endDate))
        : ''
    this.spinnerService.show()
    this.warehouseOutScanService
      .getDataMain(this.pagination, this.filterParam)
      .subscribe({
        next: res => {
          this.dataMain = res.result
          this.pagination = res.pagination
          this.spinnerService.hide()
        },
        error: () =>
          this.snotifyService.error(
            this.translateService.instant('System.Message.UnknowError'),
            this.translateService.instant('System.Caption.Error')
          )
      })
      .add(() => this.spinnerService.hide())
  }

  search() {
    this.pagination.pageNumber == 1 ? this.getDataMain() : (this.pagination.pageNumber = 1);
    this.isCheckAll = false;
  }

  clear() {
    this.startDate = null;
    this.endDate = null;
    this.filterParam = <WarehouseOutScanParam>{
      declarationNo: '',
      iono: '',
      invno: '',
      manno: '',
      purno: '',
      size: ''
    }
    this.dataMain = [];
    this.dataCheck = [];
    this.isCheckAll = false;
    this.pagination.pageNumber = 1;
    this.search();
  }
  export() {
    this.spinnerService.show();
    this.warehouseOutScanService.exportExcel(this.dataCheck).subscribe({
      next: (result: Blob) => {
        const currentTime = new Date();
        const fileName = '2.10StorageOutExport_' +
          currentTime.getFullYear().toString() +
          (currentTime.getMonth() + 1) +
          currentTime.getDate() + currentTime.getHours() +
          (currentTime.getMinutes() + 1) + currentTime.getSeconds();
        this.functionUtility.exportExcel(result, fileName);
      },
      error: () => {
        this.snotifyService.error(this.translateService.instant('System.Message.UnknowError'), this.translateService.instant('System.Caption.Error'));
      }
    }).add(() => this.spinnerService.hide());
  }

  checkButton() {
    this.dataCheck = this.dataMain.filter(x => x.checkStorageOut);
    if (this.dataCheck.length > 0) {
      this.isButtonStorageOut = this.dataCheck.every(rel => rel.status == 'Release' && rel.expectQTY == rel.actualQTY);
      this.isButtonExport = this.dataCheck.every(rel => rel.status == 'Release' || rel.status == 'StorageOut');
    }
    else {
      this.isButtonStorageOut = false;
      this.isButtonExport = false;
    }
  }

  checkRecord(item: WarehouseOutScan) {
    item.checkStorageOut = !item.checkStorageOut;
    this.isCheckAll = this.dataMain.every(x => x.checkStorageOut);
    this.checkButton();
  }

  checkAll() {
    this.dataMain.map(item => {
      item.checkStorageOut = this.isCheckAll
      return item
    })
    this.dataCheck = this.dataMain.filter(x => x.checkStorageOut)
    this.checkButton();
  }

  redicrectView(item: PickingDetailParam) {
    this.warehouseOutScanService.layoutSource.next(<WarehouseOutScanSource>{
      currentPage: this.pagination.pageNumber,
      param: { ...this.filterParam },
      wareHouseOutScan: { ...item }
    });
    this.router.navigate(['transaction/warehouse-out-scan/pickinglist']);
  }

  pageChanged(event: PageChangedEvent) {
    this.pagination.pageNumber = event.page
    this.getDataMain()
  }

  storageOut() {
    //update dataStorageOut
    this.spinnerService.show()
    this.warehouseOutScanService
      .storageOut(this.dataCheck)
      .subscribe({
        next: res => {
          if (res.isSuccess) {
            this.isButtonStorageOut = true
            this.snotifyService.success(
              this.translateService.instant('System.Message.UpdateOKMsg'),
              this.translateService.instant('System.Caption.Success')
            )
          } else {
            //check đã StorageOut chưa
            return this.snotifyService.warning(
              this.translateService.instant(
                `Transaction.WarehouseOutScan.${res.error}`
              ),
              this.translateService.instant('System.Caption.Error')
            )
          }
          this.getDataMain()
        },
        error: () => {
          this.snotifyService.error(
            this.translateService.instant('System.Message.UnknowError'),
            this.translateService.instant('System.Caption.Error')
          )
        }
      })
      .add(() => this.spinnerService.hide())
  }
}
