import { BsModalRef } from 'ngx-bootstrap/modal';
import { Component, ElementRef, EventEmitter, Inject, OnInit, Output, ViewChild } from '@angular/core';
import { IconButton } from '@constants/sd-team.utility';
import { InjectBase } from '@utilities/inject-base-app';
import { WarehouseScanService } from '@services/transaction/warehouse-scan.service';
import { CaptionConstants, MessageConstants } from '@constants/message.enum';
import { WarehouseScanDto } from '@models/transaction/warehouse-scan';

@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.scss']
})
export class FormComponent extends InjectBase implements OnInit {
  @Output() scanQRCode = new EventEmitter();
  @ViewChild('inputScanCode') input: ElementRef;
  iconButton = IconButton;
  location: string = '';
  department: string = '';
  selectedScanDate: string = '';
  listScanCode: string[] = [];

  constructor(
    public bsModalRef: BsModalRef,
    private service: WarehouseScanService
  ) { super() }

  ngOnInit(): void {
    this.service.paramForm.subscribe(res => {
      if (res != null) {
        this.selectedScanDate = res
      }
    }).unsubscribe();
    setTimeout(() => this.input.nativeElement.focus(), 0);
  }

  checkScanList(target: any) {
    if (target.split(',').filter(x => x.trim()).length != 9) {
      this.functionUtility.playAudioFail();
      this.input.nativeElement.select();
      this.snotifyService.warning(MessageConstants.NO_DATA, CaptionConstants.WARNING)
    }
    else if (!this.listScanCode.every(code => code != target)) {
      this.functionUtility.playAudioFail();
      this.input.nativeElement.select();
      this.snotifyService.warning(MessageConstants.NO_DATA, CaptionConstants.WARNING)
    }
    else {
      this.spinnerService.show();
      this.service.checkScanCode(target).subscribe({
        next: res => {
          if (res.isSuccess) {
            this.functionUtility.playAudioFail();
            this.listScanCode.push(target);
            this.input.nativeElement.value = '';
            this.input.nativeElement.focus();
          }
          else {
            this.functionUtility.playAudioFail();
            this.input.nativeElement.select();
            this.snotifyService.error(res.error, CaptionConstants.ERROR)
          }
        },
        error: () => {
          this.functionUtility.playAudioFail();
          this.snotifyService.error(MessageConstants.UN_KNOWN_ERROR, CaptionConstants.ERROR)
        }
      }).add(() => { this.spinnerService.hide() });
    }
  }

  save() {
    this.spinnerService.show();
    let scanDate: string = this.functionUtility.getDateFormat(new Date(this.selectedScanDate));
    let data: WarehouseScanDto = <WarehouseScanDto>{
      label: this.listScanCode,
      location: this.location,
      department: this.department,
      scan_Date: scanDate
    }
    this.service.saveQRStorage(data).subscribe({
      next: (res) => {
        if (res.isSuccess) {
          this.bsModalRef.hide();
          this.scanQRCode.emit(res.data);
          this.snotifyService.success(MessageConstants.UPDATED_OK_MSG, CaptionConstants.SUCCESS)
        }
        else {
          this.bsModalRef.hide();
          this.scanQRCode.emit(res.data);
          this.snotifyService.error(res.error, CaptionConstants.ERROR)
        }
      }
    })
  }

  onClosed(dismissedAlert: any): void {
    this.listScanCode = this.listScanCode.filter(x => x !== dismissedAlert);
  }
}


