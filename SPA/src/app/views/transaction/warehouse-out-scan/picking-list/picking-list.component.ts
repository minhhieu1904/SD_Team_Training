import { PickingMainDto } from './../../../../_core/models/transaction/picking-scan';
import { WarehouseOutScanPickingMainDto } from './../../../../_core/models/transaction/warehouse-out-scan';
import { LocalStorageConstants } from '@constants/local-storage.constants';
import { IconButton } from './../../../../_core/constants/sd-team.utility';
import { Component, Inject, OnInit } from '@angular/core';
import { BsDatepickerConfig, BsLocaleService } from 'ngx-bootstrap/datepicker';
import { PickingDetailParam } from '@helpers/params/transaction/pickingScanParam';
import { Pagination } from '@utilities/pagination-utility';
import { InjectBase } from '@utilities/inject-base-app';
import { WarehouseOutScanService } from '@services/transaction/warehouse-out-scan.service';
import { CaptionConstants, MessageConstants } from '@constants/message.enum';
import { PageChangedEvent } from 'ngx-bootstrap/pagination';
import { takeUntil } from 'rxjs';

@Component({
  selector: 'app-picking-list',
  templateUrl: './picking-list.component.html',
  styleUrl: './picking-list.component.scss'
})
export class PickingListComponent extends InjectBase implements OnInit {
  iconButton = IconButton;
  language: string = localStorage.getItem(LocalStorageConstants.LANG);
  bsConfig: Partial<BsDatepickerConfig> = <Partial<BsDatepickerConfig>>{};
  param: PickingDetailParam = <PickingDetailParam>{
    iono: '',
    releaseDate: '',
    startReleaseDate: '',
    endReleaseDate: '',
    storageOutDate: '',
    size: '',
    manno: '',
    purno: ''
  };

  dataMainPicking: WarehouseOutScanPickingMainDto[] = [];
  listDataUpdateRelease: PickingMainDto[] = [];
  pagination: Pagination = <Pagination>{
    pageNumber: 1,
    pageSize: 10
  }

  constructor(
    private service: WarehouseOutScanService,
    private bsLocalService: BsLocaleService
  ) { super() }

  ngOnInit(): void {
    this.bsConfig = <Partial<BsDatepickerConfig>>(
      {
        isAnimated: true,
        dateInputFormat: 'YYYY-MM-DD'
      }
    )
    this.functionUtility.setDatepickerLanguage(
      this.bsLocalService,
      this.language
    )
    this.translateService.onLangChange
      .pipe(takeUntil(this.destroyService.destroys$))
      .subscribe(res => {
        this.functionUtility.setDatepickerLanguage(
          this.bsLocalService,
          res.lang
        )
      })
    this.getData()
  }


  getData() {
    this.spinnerService.show();
    this.service.currentLayout.subscribe({
      next: res => {
        if (res) {
          this.param = res.wareHouseOutScan;
          this.spinnerService.hide()
        } else {
          this.back()
        }
      },
      error: () => {
        this.snotifyService.error(MessageConstants.UN_KNOWN_ERROR, CaptionConstants.ERROR)
      }
    }).add(() => this.spinnerService.hide())

    this.service.getDetailPickingFromMain(this.pagination, this.param).subscribe({
      next: res => {
        this.pagination = res.pagination;
        this.dataMainPicking = res.result;
      },
      error: () => {
        this.snotifyService.error(MessageConstants.UN_KNOWN_ERROR, CaptionConstants.ERROR)
      }
    }).add(() => this.spinnerService.hide())
  }

  back() {
    this.router.navigate(['/transaction/warehouse-out-scan'])
  }

  pageChanged(event: PageChangedEvent) {
    this.pagination.pageNumber = event.page
    this.getData()
  }
}
