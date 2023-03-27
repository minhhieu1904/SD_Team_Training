import { CancelStickerParam } from './../../../../_core/_helpers/params/transaction/cancelStickerParam';
import { CancelStickerViewModel } from './../../../../_core/models/transaction/cancel-sticker';
import { DestroyService } from './../../../../_core/services/common/destroy.service';
import { Component, OnInit } from '@angular/core';
import { LocalStorageConstants } from '@constants/local-storage.constants';
import { IconButton } from '@constants/sd-team.utility';

import { CancelStickerService } from '@services/transaction/cancel-sticker.service';
import { InjectBase } from '@utilities/inject-base-app';
import { Pagination } from '@utilities/pagination-utility';
import { BsLocaleService } from 'ngx-bootstrap/datepicker';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { takeUntil } from 'rxjs';
import { FormComponent } from '../form/form.component';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss'],
  providers: [DestroyService]
})
export class MainComponent extends InjectBase implements OnInit {
  bsModalRef?: BsModalRef;
  language: string = localStorage.getItem(LocalStorageConstants.LANG);
  data: CancelStickerViewModel[] = [];
  pagination: Pagination = <Pagination>{
    pageNumber: 1,
    pageSize: 10
  };
  param: CancelStickerParam = <CancelStickerParam>{};
  checkAll: boolean = false;
  iconButton = IconButton;
  isCancel: boolean = true;
  constructor(
    private cancelStickerService: CancelStickerService,
    private bsLocaleService: BsLocaleService,
    private modalService: BsModalService
  ) {
    super();
  }

  ngOnInit(): void {
    // config datepicker set language
    this.functionUtility.setDatepickerLanguage(this.bsLocaleService, this.language);
    this.translateService.onLangChange.pipe(takeUntil(this.destroyService.destroys$)).subscribe(event => {
      this.functionUtility.setDatepickerLanguage(this.bsLocaleService, event.lang);
    });
  }

  // get data pagination
  getData() {
    debugger
    this.spinnerService.show();
    this.cancelStickerService.getDataPagination(this.pagination, this.param)
      .subscribe({
        next: (res) => {
          this.pagination = res.pagination;
          this.data = res.result;
          this.checkAll = false;

          // set x, y scroll position of table
          let table = document.getElementById('table-main');
          table.scrollLeft = 0;
          table.scrollTop = 0;

          this.spinnerService.hide();
        }, error: () => {
          this.snotifyService.error(this.translateService.instant('System.Message.UnknowError'), this.translateService.instant('System.Caption.Error'));
          this.spinnerService.hide();
        }
      })
  }
  // search only current page = 1
  search() {
    this.data = [];
    if (this.validation())
      this.pagination.pageNumber == 1 ? this.getData() : this.pagination.pageNumber = 1;
  }
  validation() {
    if (!this.param.manNo && !this.param.purNo && !this.param.size && !this.param.serial) {
      this.snotifyService.error('Please enter 1 condition', this.translateService.instant('System.Caption.Error'));
      return false;
    }
    return true;
  }
  // change current page
  pageChanged(event: any) {
    this.pagination.pageNumber = event.page;
    this.getData();
  }

  clearSearch() {
    this.param = <CancelStickerParam>{};
    this.data = [];
    this.isCancel = true;
    this.checkAll = false;
  }

  changCheckAll() {
    this.isCancel = !this.checkAll;
    this.data.forEach(item => item.isChecked = this.checkAll);
  }
  // Checked item
  changeChecked(item: CancelStickerViewModel) {
    item.isChecked = !item.isChecked;
    this.isCancel = !this.data.some(x => x.isChecked);
    this.checkAll = this.data.every(x => x.isChecked);
  }

  scan() {
    this.bsModalRef = this.modalService.show(FormComponent, { class: 'modal-dialog-centered modal-lg' });
  }

  cancelStickerAction() {
    this.spinnerService.show();
    let datas = this.data.filter(x => x.isChecked).map(x => x.qrCodeValue);
    this.cancelStickerService.cancelStickerAction(datas)
      .subscribe({
        next: (res) => {
          if (res.isSuccess) {
            this.clearSearch();
            this.snotifyService.success(
              this.translateService.instant('System.Message.UpdateOKMsg'),
              this.translateService.instant('System.Caption.Success'));
          }
          else {
            this.snotifyService.error(
              this.translateService.instant(`Transaction.CancelSticker.${res.error}QRCodeID`, { QRCodeID: res.data }),
              this.translateService.instant('System.Caption.Error'));
          }
          this.spinnerService.hide();
        },
        error: () => {
          this.spinnerService.hide();
          this.snotifyService.error(
            this.translateService.instant('System.Message.UnknowError'),
            this.translateService.instant('System.Caption.Error'));
        }
      })
  }
}
