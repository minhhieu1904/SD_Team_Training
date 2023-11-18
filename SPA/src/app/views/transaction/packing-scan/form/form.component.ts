import { Component, ElementRef, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { IconButton } from '@constants/common.constants';
import { CaptionConstants, MessageConstants } from '@constants/message.enum';
import { ModalService } from '@services/common/modal.service';
import { PackingScanService } from '@services/transaction/packing-scan.service';
import { InjectBase } from '@utilities/inject-base-app';
import { OperationResult } from '@utilities/operation-result';
import { ModalDirective } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-qr-code-scan',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.scss']
})
export class FormComponent extends InjectBase implements OnInit {
  @ViewChild('modal', { static: false }) directive?: ModalDirective;
  @ViewChild('autofocus') autofocusElement: ElementRef<HTMLInputElement>;
  @Input() id: string;
  @Output() modalChange = new EventEmitter<OperationResult>();

  listScanText: string[] = [];
  iconButton = IconButton;
  scanText: string = "";
  constructor(
    private modalService: ModalService,
    private ef: ElementRef,
    private service: PackingScanService
  ) { super() }

  ngOnInit(): void {
    this.modalService.add(this);
  }


  checkScanText() {
    if (this.scanText.split(',').map(x => x.trim()).length !== 9) {
      this.functionUtility.playAudioFail();
      this.autofocusElement.nativeElement.select();
      return this.snotifyService.warning("", CaptionConstants.WARNING);
    } else if (this.listScanText.some(x => x === this.scanText)) {
      this.functionUtility.playAudioFail();
      this.autofocusElement.nativeElement.select();
      return this.snotifyService.warning("ERROR CMNR", CaptionConstants.WARNING);
    }
    this.spinnerService.show();
    this.service.checkScanItem(this.scanText).subscribe({
      next: (res: OperationResult) => {
        if (res.isSuccess) {
          this.functionUtility.playAudioSuccess();
          this.spinnerService.hide();
          this.listScanText.push(this.scanText);
          this.scanText = "";
          this.autofocusElement.nativeElement.focus();
        } else {
          this.functionUtility.playAudioFail();
          this.spinnerService.hide();
          this.autofocusElement.nativeElement.select();
          this.snotifyService.error(res.error, CaptionConstants.ERROR);
        }
      },
      error: () => {
        this.functionUtility.playAudioFail();
        this.spinnerService.hide();
        this.snotifyService.error("ERROR ROI", CaptionConstants.ERROR);
      }
    })
  }

  save() {
    this.modalChange.emit(<OperationResult>{ isSuccess: true, data: this.listScanText });
    this.closeModal();
  }

  open(): void {
    this.directive.show();
    setTimeout(() => {
      this.autofocusElement.nativeElement.focus();
    }, 500);
  }
  closeModal() {
    this.listScanText = [];
    this.scanText = "";
    this.directive.hide();
  }

  delete(deleteItem: string) {
    this.listScanText = this.listScanText.filter(x => x != deleteItem)
  }
}
