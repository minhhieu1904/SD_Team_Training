import { PickingScanParam, PickingScanSource } from './../../../../_core/_helpers/params/transaction/pickingScanParam';
import { DestroyService } from './../../../../_core/services/common/destroy.service';
import { PickingMainDto } from '@models/transaction/picking-scan';

import { Component, OnInit } from '@angular/core';
import { LocalStorageConstants } from '@constants/local-storage.constants';
import { IconButton } from '@constants/sd-team.utility';

import { InjectBase } from '@utilities/inject-base-app';
import { BsDatepickerConfig, BsLocaleService, BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { takeUntil } from 'rxjs';
import { Pagination } from '@utilities/pagination-utility';
import { PickingScanService } from '@services/transaction/picking-scan.service';
import { PageChangedEvent, PaginationModule } from 'ngx-bootstrap/pagination';
import { TranslateModule } from '@ngx-translate/core';
import { NgIf, NgFor, NgClass, DatePipe } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss'],
  providers: [DestroyService],
  standalone: true,
  imports: [FormsModule, BsDatepickerModule, NgIf, NgFor, NgClass, PaginationModule, DatePipe, TranslateModule]
})
export class MainComponent extends InjectBase implements OnInit {
  listData: PickingMainDto[] = [];
  listDataUpdateRelease: PickingMainDto[] = []; // Danh sách được chọn

  iconButton = IconButton;
  language: string = localStorage.getItem(LocalStorageConstants.LANG);
  bsConfig: Partial<BsDatepickerConfig> = <Partial<BsDatepickerConfig>>{
    isAnimated: true,
    dateInputFormat: 'YYYY-MM-DD',
  };


  startDate: Date;
  endDate: Date;
  isCheckAll: boolean = false; // Trạng thái chọn tất cả
  isButtonRelease: boolean = false; // Trạng thái nút [Release]
  isButtonExport: boolean = false; // Trạng thái nút [Export]

  filterParam: PickingScanParam = <PickingScanParam>{
    declarationNo: '',
    invno: '',
    iono: '',
    manNo: '',
    purNo: '',
    size: '',
  };

  pagination: Pagination = <Pagination>{
    pageNumber: 1,
    pageSize: 10
  };



  constructor(
    private bsLocaleService: BsLocaleService,
    private pickingScanService: PickingScanService,
  ) {
    super();
  }

  ngOnInit(): void {
    this.functionUtility.setDatepickerLanguage(this.bsLocaleService, this.language);
    this.translateService.onLangChange.pipe(takeUntil(this.destroyService.destroys$)).subscribe(res => {
      this.functionUtility.setDatepickerLanguage(this.bsLocaleService, res.lang);
    });
    this.loadCondition();
  }

  async loadCondition() {
    await this.pickingScanService.scanPickingSource.subscribe(source => {
      if (source) {
        this.pagination.pageNumber = source.currentPage;
        this.filterParam = source.param;
      }
    })

    this.getData();
  }

  getData() {
    // Set data
    this.filterParam.startReleaseDate = this.startDate == null ? '' : this.functionUtility.getDateFormat(new Date(this.startDate));
    this.filterParam.endReleaseDate = this.endDate == null ? '' : this.functionUtility.getDateFormat(new Date(this.endDate));

    // Set trạng thái Release
    this.isButtonRelease = false;
    // Set trạng thái Export
    this.isButtonExport = false;

    this.listDataUpdateRelease = [];

    this.spinnerService.show();
    this.pickingScanService.getData(this.pagination, this.filterParam).subscribe({
      next: (res) => {
        console.log(res);
        this.pagination = res.pagination;
        this.listData = res.result;
        this.isCheckAll = false;
      },
      error: () => {
        this.snotifyService.error(
          this.translateService.instant('System.Message.UnknowError'),
          this.translateService.instant('System.Caption.Error'));
      }
    }).add(() => this.spinnerService.hide());
  }

  search = () => this.pagination.pageNumber === 1 ? this.getData() : this.pagination.pageNumber = 1;

  clear() {
    this.startDate = null;
    this.endDate = null;
    this.filterParam = <PickingScanParam>{
      declarationNo: '',
      invno: '',
      iono: '',
      manNo: '',
      purNo: '',
      size: '',
      startReleaseDate: null,
      endReleaseDate: null,
    };

    this.listData = [];
    this.listDataUpdateRelease = [];

    this.isCheckAll = false;
    this.isButtonRelease = false;
    this.isButtonExport = false;

    this.pagination.pageNumber = 1;
    this.getData();
  }

  /**
   * Chuyển sang trang Picking
   * Với 1 dòng dữ liệu
   * Nếu trạng thái status là Picking thì không được picking nữa
   * @memberof MainComponent
   */
  onPicking(pickingScan: PickingMainDto) {
    // Set dữ liệu
    let pickingScanSource = <PickingScanSource>{
      currentPage: this.pagination.pageNumber,
      param: { ...this.filterParam },
      source: <PickingScanParam>{
        iono: pickingScan.iono,
        manNo: pickingScan.manno,
        purNo: pickingScan.purno,
        size: pickingScan.size,
        releaseDate: pickingScan.releaseDate
      }
    }
    this.pickingScanService.scanPickingSource.next(pickingScanSource)
    // Chuyển qua trang Picking
    this.router.navigate(['transaction/picking-scan/picking']);
  }

  onEdit(pickingScan: PickingMainDto) {
    // Set dữ liệu
    let pickingScanSource = <PickingScanSource>{
      currentPage: this.pagination.pageNumber,
      param: { ...this.filterParam },
      source: <PickingScanParam>{
        iono: pickingScan.iono,
        manNo: pickingScan.manno,
        purNo: pickingScan.purno,
        size: pickingScan.size,
        releaseDate: pickingScan.releaseDate,
        isEditOrDetail: true
      }
    }
    this.pickingScanService.scanPickingSource.next(pickingScanSource);
    // Chuyển qua trang Edit
    this.router.navigate(['transaction/picking-scan/edit']);
  }

  onDetail(pickingScan: PickingMainDto) {
    console.log(pickingScan);
    // Set dữ liệu
    let pickingScanSource = <PickingScanSource>{
      currentPage: this.pagination.pageNumber,
      param: { ...this.filterParam },
      source: <PickingScanParam>{
        iono: pickingScan.iono,
        manNo: pickingScan.manno,
        purNo: pickingScan.purno,
        size: pickingScan.size,
        releaseDate: pickingScan.releaseDate,
        isEditOrDetail: false
      }
    }
    this.pickingScanService.scanPickingSource.next(pickingScanSource);
    // Chuyển qua trang Edit với trạng thái là chỉ được xem
    this.router.navigate(['transaction/picking-scan/edit']);
  }

  export() {
    this.spinnerService.show();
    this.pickingScanService.exportExcel(this.listDataUpdateRelease).subscribe({
      next: (result: Blob) => {
        const currentTime = new Date();
        const fileName =
          "2.9PickingScan_" +
          currentTime.getFullYear().toString() +
          (currentTime.getMonth() + 1) +
          currentTime.getDate() + currentTime.getHours() + (currentTime.getMinutes() + 1) + currentTime.getSeconds();
        this.functionUtility.exportExcel(result, fileName);
      },
      error: () => {
        this.snotifyService.error(
          this.translateService.instant('System.Message.UnknowError'),
          this.translateService.instant('System.Caption.Error'));
      }
    }).add(() => this.spinnerService.hide());
  }

  checkAll() {
    this.listData.map(item => {
      item.checkRelease = this.isCheckAll;
      return item;
    });

    this.validateButtonChecked();
  }

  validateButtonChecked() {
    this.listDataUpdateRelease = this.listData.filter(x => x.checkRelease);
    if (this.listDataUpdateRelease.length > 0) {
      // Bật nút Release khi tất cả trạng thái của Items được chọn có status là 'Picking'
      this.isButtonRelease = this.listDataUpdateRelease.every(rel => rel.status === 'Picking' && rel.expectQTY == rel.actualQTY);
      // bật nút Export khi tất cả trạng thái của Items được chọn có status khác 'N'
      this.isButtonExport = this.listDataUpdateRelease.every(rel => rel.status != 'N');
    }
    else {
      this.isButtonRelease = false;
      this.isButtonExport = false;
    }
  }

  checkRecord(item: PickingMainDto) {
    item.checkRelease = !item.checkRelease;
    this.isCheckAll = this.listData.every(x => x.checkRelease);
    this.validateButtonChecked();
  }

  release() {
    //check đã release chưa
    if (!this.listDataUpdateRelease.every(rel => rel.status === 'Picking'))
      return this.snotifyService.warning(
        this.translateService.instant('System.Message.DataFound'),
        this.translateService.instant('System.Caption.Error'));

    //update release
    this.spinnerService.show();
    this.pickingScanService.release(this.listDataUpdateRelease).subscribe({
      next: (res) => {
        if (res.isSuccess) {
          this.snotifyService.success(
            this.translateService.instant('System.Message.UpdateOKMsg'),
            this.translateService.instant('System.Caption.Success'));
          this.getData();
        }
        else {
          this.snotifyService.error(
            this.translateService.instant('System.Message.UpdateErrorMsg'),
            this.translateService.instant('System.Caption.Error'));
        }
      },
      error: () => {
        this.snotifyService.error(
          this.translateService.instant('System.Message.UnknowError'),
          this.translateService.instant('System.Caption.Error'));
      }
    }).add(() => this.spinnerService.hide());
  }

  pageChanged(event: PageChangedEvent) {
    this.pagination.pageNumber = event.page;
    this.getData();
  }

}
