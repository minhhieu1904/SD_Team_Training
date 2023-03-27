import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ReprintStickerRoutingModule } from './reprint-sticker-routing.module';
import { MainComponent } from './main/main.component';
import { FormComponent } from './form/form.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgxSpinnerModule } from 'ngx-spinner';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { ModalModule } from 'ngx-bootstrap/modal';
import { TranslateModule } from '@ngx-translate/core';
import { QrcodePrinterComponent } from '../../commons/qrcode-printer/qrcode-printer.component';


@NgModule({
  declarations: [
    MainComponent,
    FormComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    NgxSpinnerModule,
    BsDatepickerModule.forRoot(),
    PaginationModule.forRoot(),
    ModalModule.forRoot(),
    TranslateModule,
    ReprintStickerRoutingModule,
    QrcodePrinterComponent
  ],
  exports: [QrcodePrinterComponent]
})
export class ReprintStickerModule { }
