import { PickingScanParam } from './../../../../_core/_helpers/params/transaction/pickingScanParam';
import { PickingScanService } from '@services/transaction/picking-scan.service'
import { Component, ElementRef, OnInit, ViewChild } from '@angular/core'
import { LocalStorageConstants } from '@constants/local-storage.constants'
import { IconButton } from '@constants/sd-team.utility'
import {
  GetScanPickingMainDto,
  PickingDetail
} from '@models/transaction/picking-scan'

import { InjectBase } from '@utilities/inject-base-app'
import { BsDatepickerConfig, BsDatepickerModule } from 'ngx-bootstrap/datepicker'
import { BsModalRef, ModalDirective, ModalModule } from 'ngx-bootstrap/modal'
import { takeUntil } from 'rxjs'
import { TranslateModule } from '@ngx-translate/core';
import { FormsModule } from '@angular/forms';
import { NgIf, NgFor, DatePipe } from '@angular/common';

@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.scss'],
  standalone: true,
  imports: [NgIf, FormsModule, BsDatepickerModule, NgFor, ModalModule, DatePipe, TranslateModule]
})
export class FormComponent extends InjectBase implements OnInit {
  @ViewChild('modal', { static: false }) directive?: ModalDirective
  @ViewChild('autofocus') autofocusElement: ElementRef<HTMLInputElement>

  iconButton = IconButton
  language: string = localStorage.getItem(LocalStorageConstants.LANG)
  bsConfig: Partial<BsDatepickerConfig> = <Partial<BsDatepickerConfig>>{
    isAnimated: true,
    dateInputFormat: 'YYYY-MM-DD',
  }
  modalRef?: BsModalRef
  listData: GetScanPickingMainDto = <GetScanPickingMainDto>{}
  listScanCode: string[] = []
  listQrCodeIDCurrent: string[] = []

  param: PickingScanParam = <PickingScanParam>{
    declarationNo: '',
    invno: '',
    iono: '',
    manNo: '',
    purNo: '',
    size: '',
  }
  startDate: Date;
  scanText: string = ''

  isFirstScan: boolean = false
  isDisableSaveAll: boolean = false
  isEdit: boolean = true; // Trạng thái Cập nhật hay Xem chi tiết

  isUpdate: boolean = false
  isRelease: boolean = false

  constructor(
    private pickingScanService: PickingScanService
  ) {
    super()
  }

  ngOnInit(): void {
    this.translateService.onLangChange.pipe(takeUntil(this.destroyService.destroys$)).subscribe(res => { })
    this.loadDataFromCondition();
  }


  async loadDataFromCondition() {
    await this.pickingScanService.currentPrintQrCode.subscribe(source => {
      if (source) {
        this.param.iono = source.source.iono;
        this.param.manNo = source.source.manNo;
        this.param.purNo = source.source.purNo;
        this.param.size = source.source.size;
        this.param.releaseDate = source.source.releaseDate;
        this.startDate = !this.functionUtility.checkEmpty(source.source.releaseDate) ? new Date(source.source.releaseDate) : null;

        // Set trạng thái cập nhật hoặc xem chi tiết
        this.isEdit = source.source.isEditOrDetail;
        // Lấy danh sách chi tiết
      }
      else this.back();
    })
    this.getDataDetail();
  }



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
    } else if (this.listScanCode.some(x => x === this.scanText)) {
      this.functionUtility.playAudioFail()
      this.autofocusElement.nativeElement.select()
      return this.snotifyService.warning(
        this.translateService.instant('Transaction.PickingScan.ThisCodeAlreadysCanned'),
        this.translateService.instant('System.Caption.Warning')
      )
    }
    // Kiểm tra xem đã có trong DataTables hay chưa ?


    else if (this.listData.listPickingDetail.some(x => x.qrCodeValue === this.scanText)) {
      let isShowError = true;
      this.listData.listPickingDetail.forEach(x => {
        if (x.qrCodeValue == this.scanText) {
          if (x.isDelete == true) {
            this.listScanCode.push(this.scanText);
            // x.isDelete = false;
            isShowError = false;
            this.autofocusElement.nativeElement.value = ''
            return;
          }
        }
      })

      if (isShowError) {
        this.functionUtility.playAudioFail()
        this.autofocusElement.nativeElement.select()
        return this.snotifyService.warning(
          this.translateService.instant('Transaction.PickingScan.29AlreadyPickingQRCODE'),
          this.translateService.instant('System.Caption.Warning')
        )
      }
    }
    else {
      this.spinnerService.show()
      this.pickingScanService.checkPickingScanCode(this.param, this.scanText).subscribe({
        next: res => {
          if (res.isSuccess) {
            this.functionUtility.playAudioSuccess()
            this.listScanCode.push(this.scanText)
            this.autofocusElement.nativeElement.value = ''
            this.autofocusElement.nativeElement.focus()
          } else {
            this.functionUtility.playAudioFail()
            this.autofocusElement.nativeElement.select()
            this.snotifyService.error(
              this.translateService.instant(
                'Transaction.PickingScan.' + res.error
              ),
              this.translateService.instant('System.Caption.Error')
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
   * Kiểm tra những item trong ListDataDetail các IsBlock  = true có Qrcode nào trùng thì trả về
   * @memberof FormComponent
   */
  returnQRcodeBlock() {
    this.listData.listPickingDetail.filter(x => x.isBlock && x.isDelete).forEach(x => {
      let checkQrcode = this.listScanCode.find(code => x.qrCodeValue == code);
      if (checkQrcode != null) {
        x.isDelete = false;
        this.listScanCode = this.listScanCode.filter(code => code != x.qrCodeValue);
      }
    })
  }

  /**
   * Lấy danh sách value của QrCode
   * @memberof FormComponent
   */
  getDataScan() {
    // Kiểm tra những item trong ListDataDetail các IsBlock  = true có Qrcode nào trùng thì trả về
    this.returnQRcodeBlock();

    if (this.listScanCode.length > 0) {


      this.spinnerService.show()
      this.pickingScanService.getDataByScan(this.listScanCode.join('_')).subscribe({
        next: res => {
          let isSuccess = true
          if (!this.isFirstScan) {
            res.listPickingDetail.forEach(x => x.isBlock = false);
            this.listData.listPickingDetail.push(...res.listPickingDetail);
            this.isFirstScan = true
          } else {
            this.checkQrCodeAfterScan()
            res.listPickingDetail.forEach(i => {
              if (!this.listQrCodeIDCurrent.includes(i.qrCodeID)) {
                if (this.isUpdate) {
                  if (
                    this.listData.listPickingDetail[0].manno !== i.manno ||
                    this.listData.listPickingDetail[0].purno !== i.purno
                  ) {
                    isSuccess = false
                    return this.snotifyService.error(this.translateService.instant('Transaction.PickingScan.InvalidUpdateData'), this.translateService.instant('System.Caption.Error'))
                  }
                }
                i.declaration_no = this.listData.pickingMain.declaration_no
                this.listData.listPickingDetail.push(i)
              } else {
                this.listData.listPickingDetail.find(
                  x => x.qrCodeID === i.qrCodeID
                ).isDelete = false
              }
            })
          }
          if (!this.isUpdate) this.createPickingTrNo()
          if (isSuccess) {
            this.isDisableSaveAll = false
            this.closeModal()
          }
        },
        error: () => {
          this.snotifyService.error(
            this.translateService.instant('System.Message.UnknowError'),
            this.translateService.instant('System.Caption.Error')
          )
        }
      }).add(() => this.spinnerService.hide())
    }
    else {
      this.isDisableSaveAll = false
      this.closeModal()
    }
  }
  checkQrCodeAfterScan() {
    this.listData.listPickingDetail.forEach(i => {
      this.listQrCodeIDCurrent.push(i.qrCodeID)
    })
  }

  back = () => this.router.navigate(['transaction/picking-scan'])

  delete = (deleteItem: string) => this.listScanCode = this.listScanCode.filter(x => x != deleteItem)

  closeModal() {
    this.listScanCode = []
    this.scanText = ''
    this.directive.hide()
  }
  deleteItem(item: PickingDetail) {
    this.listData.listPickingDetail.forEach(x => {
      if (x.qrCodeID === item.qrCodeID) {
        x.isBlock ? x.isDelete = true : this.listData.listPickingDetail = this.listData.listPickingDetail.filter(x => x.qrCodeID != item.qrCodeID)
      }
    })
    this.isDisableSaveAll = this.listData.listPickingDetail.every(x => x.isDelete);
  }

  getDataDetail() {
    this.spinnerService.show()
    this.pickingScanService.getDataDetail(this.param).subscribe({
      next: res => {
        console.log(res);
        // Lấy danh sách hiển thị
        this.listData = res;
        this.listData.listPickingDetail.forEach(x => x.isBlock = true);
      },
      error: () => {
        this.snotifyService.error(
          this.translateService.instant('System.Message.UnknowError'),
          this.translateService.instant('System.Caption.Error')
        )
      }
    }).add(() => this.spinnerService.hide())
  }

  updateAll() {
    // check data released
    if (this.isRelease)
      return this.snotifyService.error(this.translateService.instant('System.Message.UpdateErrorMsg'), this.translateService.instant('System.Caption.Error'))
    //check data [manno] and [purno] are same
    if (
      this.listData.listPickingDetail.some(x => x.manno !== this.listData.listPickingDetail[0].manno || x.purno !== this.listData.listPickingDetail[0].purno)
    )
      return this.snotifyService.error(
        this.translateService.instant('Transaction.PickingScan.InvalidUpdateData'), this.translateService.instant('System.Caption.Error'))

    // update list data
    this.spinnerService.show()
    this.pickingScanService.update(this.listData).subscribe({
      next: res => {
        if (res.isSuccess) {
          this.snotifyService.success(
            this.translateService.instant('System.Message.UpdateOKMsg'),
            this.translateService.instant('System.Caption.Success')
          )
          this.back()
        } else {
          this.snotifyService.error(
            this.translateService.instant('System.Message.UpdateErrorMsg'),
            this.translateService.instant('System.Caption.Error')
          )
        }
      },
      error: () => {
        this.snotifyService.error(
          this.translateService.instant('System.Message.UnknowError'),
          this.translateService.instant('System.Caption.Error')
        )
      }
    }).add(() => this.spinnerService.hide())
  }
  //--------------------------------- create PickingTrNo when add new----------------------------------
  // When insert into MS_QR_PickingMain, if user scan 2 or more qrcode,
  // pickingTrNo need to separate base on [manno] and [purno]
  createPickingTrNo() {
    let pickingTrNo = this.listData.listPickingDetail[0].declaration_no
    let pickingTrNoLeft = pickingTrNo.slice(0, -3)
    let pickingTrNoRight = +pickingTrNo.slice(-3)

    this.listData.listPickingDetail.forEach((el, i) => {
      if (i != 0) {
        let indexNew = this.listData.listPickingDetail.findIndex(
          x => x.manno === el.manno && x.purno === el.purno
        )
        if (indexNew != -1 && indexNew < i)
          el.declaration_no = this.listData.listPickingDetail[indexNew].declaration_no
        else {
          pickingTrNoRight++
          el.declaration_no =
            pickingTrNoLeft + this.codeGeneration(pickingTrNoRight)
        }
      }
    })
  }

  codeGeneration(pickingTrNoRight: number) {
    if (pickingTrNoRight < 10) return '00' + pickingTrNoRight.toString()
    else if (pickingTrNoRight >= 10 && pickingTrNoRight < 100)
      return '0' + pickingTrNoRight.toString()
    else return pickingTrNoRight.toString()
  }


}
