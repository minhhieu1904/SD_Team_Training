import { CancelStickerService } from './../../../../_core/services/transaction/cancel-sticker.service';
import { IconButton } from './../../../../_core/constants/sd-team.utility';
import { InjectBase } from '../../../../_core/utilities/inject-base-app';
import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';

import { BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.scss']
})
export class FormComponent extends InjectBase implements OnInit {
  @ViewChild('inputScanCode') input: ElementRef;
  iconButton = IconButton;
  scanDate: string;
  location: string = '';
  department: string = '';
  listScanCode: string[] = [];

  constructor(public bsModalRef: BsModalRef,
    private service: CancelStickerService
  ) {
    super();
  }

  ngOnInit() {
    setTimeout(() => this.input.nativeElement.focus(), 0);
  }

  checkScanList(target: any) {
    if (target.split(',').filter(x => x.trim()).length != 9) {
      this.functionUtility.playAudioFail();
      this.input.nativeElement.select();
      this.snotifyService.warning(
        this.translateService.instant('Transaction.WarehouseScan.ThisDataWrong'),
        this.translateService.instant('System.Caption.Warning'));
    }
    else if (!this.listScanCode.every(code => code != target)) {
      this.functionUtility.playAudioFail();
      this.input.nativeElement.select();
      this.snotifyService.warning(
        this.translateService.instant('Transaction.WarehouseScan.ThisStickerAlreadyBeenScanned'),
        this.translateService.instant('System.Caption.Warning'));
    }
    else {
      this.spinnerService.show();
      this.service.checkRecordScan(target).subscribe({
        next: (res) => {
          if (res.isSuccess) {
            this.functionUtility.playAudioSuccess();
            this.listScanCode.push(target);
            this.input.nativeElement.value = '';
            this.input.nativeElement.focus();
          }
          else {
            this.functionUtility.playAudioFail();
            this.input.nativeElement.select();
            this.snotifyService.error(
              this.translateService.instant('Transaction.CancelSticker.' + res.error),
              this.translateService.instant('System.Caption.Error'));
          }
        },
        error: () => {
          this.functionUtility.playAudioFail();
          this.snotifyService.error(
            this.translateService.instant('System.Message.UnknowError'),
            this.translateService.instant('System.Caption.Error'));
        }
      }).add(() => this.spinnerService.hide());
    }
  }

  save() {
    this.spinnerService.show();
    this.service.cancelStickerAction(this.listScanCode).subscribe({
      next: (res) => {
        if (res.isSuccess) {
          this.snotifyService.success(
            this.translateService.instant('System.Message.UpdateOKMsg'),
            this.translateService.instant('System.Caption.Success'));
        }
        else {
          this.snotifyService.error(
            this.translateService.instant('System.Message.UpdateErrorMsg'),
            this.translateService.instant('System.Caption.Error'));
        }
        this.bsModalRef.hide();
        this.spinnerService.hide();
      },
      error: () => {
        this.spinnerService.hide();
        this.snotifyService.error(
          this.translateService.instant('System.Message.UnknowError'),
          this.translateService.instant('System.Caption.Error'));
      }
    });
  }

  onRemove(qrCodeValue: string): void {
    this.listScanCode = this.listScanCode.filter(code => code !== qrCodeValue);
  }
}
