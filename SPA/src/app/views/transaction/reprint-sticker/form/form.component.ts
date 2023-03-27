import { Component, ElementRef, EventEmitter, OnInit, Output, ViewChild } from '@angular/core';
import { CaptionConstants, MessageConstants } from '@constants/message.enum';
import { IconButton } from '@constants/sd-team.utility';
import { ReprintStickerModel, ReprintStickerParam } from '@models/transaction/reprint-sticker';
import { ReprintStickerService } from '@services/transaction/reprint-sticker.service';
import { InjectBase } from '@utilities/inject-base-app';
import { BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.scss']
})
export class FormComponent extends InjectBase implements OnInit {
  @Output() scanCode = new EventEmitter();
  @ViewChild('inputScanCode') input: ElementRef;
  iconButton = IconButton;
  listScanCode: string[] = [];

  constructor(
    public bsModalRef: BsModalRef,
    private reprintStickerService: ReprintStickerService
  ) { super() }

  ngOnInit(): void {

  }

  checkScanList(target: any) {
    if (target.split(',').filter(x => x.trim()).length != 9) {
      this.input.nativeElement.select();
      this.snotifyService.warning(MessageConstants.NO_DATA, CaptionConstants.WARNING);
    } else if (!this.listScanCode.every(x => x !== target)) {
      this.input.nativeElement.select();
      this.snotifyService.warning(MessageConstants.NO_DATA, CaptionConstants.WARNING);
    } else {
      this.listScanCode.push(target);
      this.input.nativeElement.value = '';
      this.input.nativeElement.focus();
      this.functionUtility.playAudioSuccess();
    }
  }

  onClosed(i: number) {
    this.listScanCode.splice(i, 1);
  }

  save() {
    debugger
    let data = this.listScanCode.map(x => {
      let arr = x.trim().split(',');
      let item: ReprintStickerModel = <ReprintStickerModel>{
        manNo: arr[0],
        purNo: arr[1],
        size: arr[2],
        qty: + arr[3],
        serial: + arr[4],
        wkshno: arr[5],
        prtno: arr[6],
        empno: arr[7],
        grade: arr[8],
        qrCodeValue: x.trim()
      };
      return item
    });
    this.reprintStickerService.getDataByScan(data).subscribe({
      next: res => {
        this.scanCode.emit(res);
      },
      error: (err) => {
        this.snotifyService.error(MessageConstants.UN_KNOWN_ERROR, CaptionConstants.ERROR);
      },
    })
  }

}
