import { OrderPrintResult } from './../../../../_core/models/transaction/search-for-order-data';
import { ModalService } from './../../../../_core/services/common/modal.service';
import { ReprintStickerService } from './../../../../_core/services/transaction/reprint-sticker.service';
import { ReprintStickerModel, ReprintStickerParam } from './../../../../_core/models/transaction/reprint-sticker';
import { IconButton } from './../../../../_core/constants/sd-team.utility';
import { Component, DestroyRef, OnInit, ViewChild } from '@angular/core';
import { LocalStorageConstants } from '@constants/local-storage.constants';
import { DestroyService } from '@services/common/destroy.service';
import { QrcodeComponent } from '@techiediaries/ngx-qrcode';
import { InjectBase } from '@utilities/inject-base-app';
import { Pagination } from '@utilities/pagination-utility';
import { BsDatepickerConfig, BsLocaleService } from 'ngx-bootstrap/datepicker';
import { QrcodePrinterComponent } from 'src/app/views/commons/qrcode-printer/qrcode-printer.component';
import { ReprintStickerModule } from '../reprint-sticker.module';
import { BsModalRef, BsModalService, ModalOptions } from 'ngx-bootstrap/modal';
import { takeUntil } from 'rxjs';
import { CaptionConstants, MessageConstants } from '@constants/message.enum';
import { FormComponent } from '../form/form.component';
import { PageChangedEvent } from 'ngx-bootstrap/pagination';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss'],
  providers: [DestroyService]
})
export class MainComponent extends InjectBase implements OnInit {
  @ViewChild('qrcodeprinter') qrcodeprinter: QrcodePrinterComponent;
  language: string = localStorage.getItem(LocalStorageConstants.LANG);
  pagination: Pagination = <Pagination>{
    pageNumber: 1,
    pageSize: 10
  }
  bsConfig: Partial<BsDatepickerConfig> = <Partial<BsDatepickerConfig>>{};
  iconButton = IconButton;
  time_start: string = '';
  time_end: string = '';
  checkAll: boolean = false;
  param: ReprintStickerParam = <ReprintStickerParam>{
    fromDate: '',
    toDate: '',
    prtno: '',
    manNo: '',
    purNo: '',
    size: '',
    serial: null
  };
  listData: ReprintStickerModel[] = [];
  dataCheck: ReprintStickerModel[] = [];

  bsModalRef?: BsModalRef;
  constructor(
    private reprintStickerService: ReprintStickerService,
    private bsLocaleService: BsLocaleService,
    private modalService: BsModalService
  ) { super() }

  ngOnInit(): void {
    this.bsConfig = Object.assign({},
      {
        isAnimated: true,
        dateInputFormat: 'YYYY/MM/DD'
      }
    );
    this.functionUtility.setDatepickerLanguage(this.bsLocaleService, this.language);
    this.translateService.onLangChange.pipe(takeUntil(this.destroyService.destroys$)).subscribe(res => {
      this.functionUtility.setDatepickerLanguage(this.bsLocaleService, res.lang);
    });
  }

  validateSearch(): boolean {
    let validate: boolean = true;

    if ((this.functionUtility.checkEmpty(this.time_start) && this.functionUtility.checkEmpty(this.time_end)) &&
      this.functionUtility.checkEmpty(this.param.prtno) && this.functionUtility.checkEmpty(this.param.manNo)
      && this.functionUtility.checkEmpty(this.param.purNo) && this.functionUtility.checkEmpty(this.param.size) &&
      this.functionUtility.checkEmpty(this.param.serial)) {
      validate = false;
      this.snotifyService.error('Please enter 1 condition', this.translateService.instant('System.Caption.Error'));
    }

    if ((this.functionUtility.checkEmpty(this.time_start) && !this.functionUtility.checkEmpty(this.time_end)) ||
      (!this.functionUtility.checkEmpty(this.time_start) && this.functionUtility.checkEmpty(this.time_end))) {
      validate = false;
      this.snotifyService.error('Please choose mdat start and mdat end', this.translateService.instant('System.Caption.Error'));
    }

    if (Date.parse(this.time_start) > Date.parse(this.time_end)) {
      validate = false;
      this.snotifyService.error('Please select the end date must be greater than the start date', this.translateService.instant('System.Caption.Error'));
    }
    return validate;
  }

  search() {
    if (this.validateSearch()) {
      this.pagination.pageNumber === 1 ? this.getData() : this.pagination.pageNumber = 1;
      this.getData();
    }
  }

  getData() {
    if ((!this.functionUtility.checkEmpty(this.time_start) && !this.functionUtility.checkEmpty(this.time_end))) {
      this.param.fromDate = this.functionUtility.getDateFormat(new Date(this.time_start));
      this.param.toDate = this.functionUtility.getDateFormat(new Date(this.time_end));
    }
    this.spinnerService.show();
    this.reprintStickerService.getData(this.pagination, { ...this.param }).subscribe({
      next: (res) => {
        this.listData = res.result.map(item => {
          item.check = false;
          return item;
        });
        this.pagination = res.pagination;
        this.checkAll = false;
      },
      error: () => {
        this.snotifyService.error(
          this.translateService.instant('System.Message.UnknowError'),
          this.translateService.instant('System.Caption.Error'));
        this.spinnerService.hide();
      },
      complete: () => {
        this.spinnerService.hide();
      }
    })
  }

  rePrint() {
    this.dataCheck = this.listData.filter(x => x.check);
    if (this.dataCheck.length == 0) {
      return this.snotifyService.error('Please check record', CaptionConstants.ERROR);
    }
    this.reprintStickerService.updateData(this.dataCheck).subscribe({
      next: res => {
        if (res) {
          (this.dataCheck = this.dataCheck.map(item => {
            item.prt_Cnt += 1;
            return item;
          }));
          this.print(this.dataCheck);
        }
      },
      error: (err) => {
        this.snotifyService.error(MessageConstants.UN_KNOWN_ERROR, CaptionConstants.ERROR);
        document.body.style.overflow = 'auto';
        document.getElementById('printData').hidden = true;
      },
    })
  }

  print(data: ReprintStickerModel[]) {
    document.body.style.overflow = 'hidden';
    //remove hidden of list data print
    document.getElementById('printData').hidden = false;

    let orderPrintResult = data.map(item => {
      let model: OrderPrintResult = <OrderPrintResult>{
        article: item.article,
        bitnbr: item.bitnbr,
        custid: item.cusid,
        kind: item.kind,
        manno: item.manNo,
        printDate: item.prtDay,
        purno: item.purNo,
        qrCodeName: item.qrCodeValue,
        qty: item.qty,
        rmodel: item.rmodel,
        serial: item.serial,
        size: item.size,
        remark: item.remark
      }
      return model;
    });
    this.qrcodeprinter.print(orderPrintResult);

    // remove overflow hidden of body
    document.body.style.overflow = 'auto';
  }

  scan() {
    const initialState: ModalOptions = {
      class: 'modal-dialog-centered modal-lg'
    };
    this.bsModalRef = this.modalService.show(FormComponent, initialState);
    this.bsModalRef.content.scanQRCode.subscribe((data: ReprintStickerModel[]) => {
      this.print(data);
      this.modalService.hide();
    });
  }

  changeCheckAll() {
    this.listData.map(item => {
      item.check = this.checkAll;
      return item;
    });
    this.dataCheck = this.listData.filter(x => x.check);
  }

  changeCheckRecord(item: ReprintStickerModel) {
    item.check = !item.check;
    this.checkAll = this.listData.every(x => x.check);
    this.dataCheck = this.listData.filter(x => x.check);
  }

  changeSerial(event) {
    if (event.target.value < 0) {
      event.target.value = 0;
    }
  }

  pageChanged(event: PageChangedEvent) {
    this.pagination.pageNumber = event.page;
    this.getData();
  }

  clear() {
    this.checkAll = false;
    this.time_start = '';
    this.time_end = '';
    this.param = <ReprintStickerParam>{
      fromDate: '',
      toDate: '',
      prtno: '',
      serial: null
    };
    this.listData.length = 0;
    this.dataCheck.length = 0;
  }

}
