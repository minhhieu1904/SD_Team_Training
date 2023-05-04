import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { QrcodePrinterComponent } from './qrcode-printer/qrcode-printer.component';
import { QRCodeModule } from 'angularx-qrcode';
import { NgxPrintElementModule } from 'ngx-print-element';



@NgModule({
  declarations: [
    //QrcodePrinterComponent
  ],
  imports: [
    CommonModule,
    QRCodeModule,
    NgxPrintElementModule
  ],
  exports:[
    //QrcodePrinterComponent
  ]
})
export class CommonsModule { }
