import { PickingScanParam } from './../../../../_core/_helpers/params/transaction/pickingScanParam';
import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { LocalStorageConstants } from '@constants/local-storage.constants';
import { IconButton } from '@constants/sd-team.utility';
import { GetScanPickingMainDto, PickingDetail, PickingUpdate } from '@models/transaction/picking-scan';

import { PickingScanService } from '@services/transaction/picking-scan.service';
import { InjectBase } from '@utilities/inject-base-app';
import { Pagination } from '@utilities/pagination-utility';
import { BsModalRef, ModalDirective, ModalModule } from 'ngx-bootstrap/modal';
import { TranslateModule } from '@ngx-translate/core';
import { NgIf, NgFor, DatePipe } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-picking',
  templateUrl: './picking.component.html',
  styleUrls: ['./picking.component.scss'],
  standalone: true,
  imports: [FormsModule, NgIf, NgFor, ModalModule, DatePipe, TranslateModule]
})
export class PickingComponent extends InjectBase implements OnInit {
  iconButton = IconButton;
  language: string = localStorage.getItem(LocalStorageConstants.LANG);
  listData: GetScanPickingMainDto = <GetScanPickingMainDto>{
    listPickingDetail: [],
    pickingMain: null
  };  // danh sách khi quét mã
  param: PickingScanParam = <PickingScanParam>{
    declarationNo: '',
    invno: '',
    iono: '',
    manNo: '',
    purNo: '',
    size: '',
    releaseDate: ''
  }

  pagination: Pagination = <Pagination>{
    pageNumber: 1,
    pageSize: 10
  };

  scanCodes: string[] = []
  scanText: string = ''

  //#region Modal
  modalRef?: BsModalRef
  @ViewChild('modal', { static: false }) directive?: ModalDirective
  @ViewChild('autofocus') autofocusElement: ElementRef<HTMLInputElement>
  //#endregion

  constructor(
    private pickingScanService: PickingScanService,
  ) { super() }

  ngOnInit(): void {
    this.getDataFromSouce();
  }


  //#endregion Danh mục
  getDataFromSouce() {
    this.pickingScanService.scanPickingSource.subscribe(source => {
      if (source && source != null) {
        this.param = { ...source.source };
      }
      else this.back();
    })
  }

  //#endregion

  //#region Methods

  back = () => this.router.navigate(['/transaction/picking-scan'])
  delete = (deleteItem: string) => this.scanCodes = this.scanCodes.filter(x => x != deleteItem);

  scan() {
    this.directive.show()
    setTimeout(() => {
      this.autofocusElement.nativeElement.focus()
    }, 500)
  }

  checkScanText() {
    setTimeout(() => this.autofocusElement.nativeElement.focus(), 0)
    if (this.scanText.split(',').filter(x => x.trim()).length != 9) {
      this.functionUtility.playAudioFail()
      this.autofocusElement.nativeElement.select()
      this.snotifyService.warning(
        this.translateService.instant('Transaction.PickingScan.ThisDataWrong'),
        this.translateService.instant('System.Caption.Warning')
      )
    } else if (this.scanCodes.some(x => x === this.scanText)) {
      this.functionUtility.playAudioFail()
      this.autofocusElement.nativeElement.select()
      return this.snotifyService.warning(
        this.translateService.instant('Transaction.PickingScan.ThisCodeAlreadysCanned'),
        this.translateService.instant('System.Caption.Warning')
      )
    }
    else {
      this.spinnerService.show()
      this.pickingScanService.checkPickingScanCode(this.param, this.scanText).subscribe({
        next: res => {
          if (res.isSuccess) {
            debugger
            this.functionUtility.playAudioSuccess()
            this.scanCodes.push(this.scanText)
            this.autofocusElement.nativeElement.value = ''
            this.autofocusElement.nativeElement.focus()
          } else {
            this.functionUtility.playAudioFail()
            this.autofocusElement.nativeElement.select()
            this.snotifyService.error(this.translateService.instant('Transaction.PickingScan.' + res.error), this.translateService.instant('System.Caption.Error')
            )
          }
        },
        error: () => {
          this.functionUtility.playAudioFail()
          this.snotifyService.error(
            this.translateService.instant('System.Message.UnknowError'),
            this.translateService.instant('System.Caption.Error')
          )
        }
      }).add(() => this.spinnerService.hide())
    }
  }


  /**
   * Lấy danh sách data theo danh sách QRCode
   */
  getDataScan() {
    this.spinnerService.show()
    this.pickingScanService.getDataByScanCode(this.param, this.scanCodes.join('_')).subscribe({
      next: result => {
        this.spinnerService.hide()
        this.listData = result;
        this.closeModal()
      },
      error: () => {
        this.snotifyService.error(
          this.translateService.instant('System.Message.UnknowError'),
          this.translateService.instant('System.Caption.Error')
        )
        this.spinnerService.hide()
      }
    })
      .add(() => this.spinnerService.hide())
  }

  closeModal() {
    this.scanCodes = []
    this.scanText = ''
    this.directive.hide()
  }
  //#endregion

  //#region SAVECHANGE
  saveChange() {
    // lấy danh sách với status không phải delete
    this.listData.listPickingDetail = this.listData.listPickingDetail.filter(x => !x.isDelete);
    // tạo model cần add
    let model = <PickingUpdate>{ data: this.listData, param: this.param };
    // Xử lí cập nhật
    this.spinnerService.show();
    this.pickingScanService.updatePickingQrCode(model).subscribe({
      next: result => {
        this.spinnerService.hide();
        if (result.isSuccess) {
          this.snotifyService.success(this.translateService.instant('System.Message.CreateOKMsg'), this.translateService.instant('System.Caption.Success'));
          this.back();
        }
        else this.snotifyService.error(this.translateService.instant('Transaction.PickingScan.' + result.error), this.translateService.instant('System.Caption.Error'))
      },
      error: () => {
        this.snotifyService.error(
          this.translateService.instant('System.Message.UnknowError'),
          this.translateService.instant('System.Caption.Error')
        )
        this.spinnerService.hide()
      }
    })
  }
  //#endregion

  //#region Events
  deleteItem(item: PickingDetail) {
    this.listData.listPickingDetail.forEach(x => {
      if (x.qrCodeID === item.qrCodeID) x.isDelete = true
    })
  }
  //#endregion
}
