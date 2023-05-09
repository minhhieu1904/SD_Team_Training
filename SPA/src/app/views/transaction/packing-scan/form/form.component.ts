import { Component, ElementRef, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { IconButton } from '@constants/common.constants';
import { ModalService } from '@services/common/modal.service';
import { PackingScanService } from '@services/packing-scan.service';
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
    private el: ElementRef,
    private service: PackingScanService
  ) {
    super();
   }

  ngOnInit(): void {
    if (!this.id) {
      console.error('modal must have an id');
      return;
    }
    this.modalService.add(this);

  }

  ngOnDestroy(): void {
    this.modalService.remove(this.id);
    this.el.nativeElement.remove();
  }

  checkScanText() {
    if (this.scanText.split(',').map(x => x.trim()).length !== 9) {
      this.functionUtility.playAudioFail();
      this.autofocusElement.nativeElement.select();
      return this.snotifyService.warning(this.translateService.instant('Transaction.PackingScan.ThisDataWrong'), this.translateService.instant('System.Caption.Warning'));
    }
    else if (this.listScanText.some(x => x === this.scanText)) {
      this.functionUtility.playAudioFail();
      this.autofocusElement.nativeElement.select();
      return this.snotifyService.warning(this.translateService.instant('Transaction.PackingScan.ThisCodeAlreadysCanned'), this.translateService.instant('System.Caption.Warning'));
    }
    this.spinnerService.show();
    this.service.checkScanItem(this.scanText).subscribe({
      next: (result: OperationResult) => {
        if (result.isSuccess) {
          this.functionUtility.playAudioSuccess();
          this.spinnerService.hide();
          this.listScanText.push(this.scanText);
          this.scanText = "";
          this.autofocusElement.nativeElement.focus();
        }
        else {
          this.functionUtility.playAudioFail();
          this.spinnerService.hide();
          this.autofocusElement.nativeElement.select();
          this.snotifyService.error(this.translateService.instant('Transaction.PackingScan.' + result.error), this.translateService.instant('System.Caption.Error'));
        }
      },
      error: () => {
        this.functionUtility.playAudioFail();
        this.spinnerService.hide();
        this.snotifyService.error(this.translateService.instant('System.Message.UnknowError'), this.translateService.instant('System.Caption.Error'));
      }
    })
  }

  save() {
    this.modalChange.emit(<OperationResult>{ isSuccess: true, data: this.listScanText });
    this.closeModal();
  }

  // open modal
  open(): void {
    this.directive.show();
    setTimeout(() => {
      this.autofocusElement.nativeElement.focus();
    }, 500);
  }

  closeModal() {
    this.listScanText = [];
    this.scanText = '';
    this.directive.hide();
  }

  delete(deleteItem: string) {
    this.listScanText = this.listScanText.filter(x => x != deleteItem)
  }
}
