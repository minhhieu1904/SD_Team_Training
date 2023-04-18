import { Component, OnInit } from '@angular/core';
import {
  Config,
  NgxPrintElementModule,
  NgxPrintElementService,
} from 'ngx-print-element';
import { interval, take } from 'rxjs';
import { OrderPrintResult } from '@models/transaction/search-for-order-data';
import { CommonModule } from '@angular/common';
import { NgxQRCodeModule } from '@techiediaries/ngx-qrcode';
import { InjectBase } from '@utilities/inject-base-app';

@Component({
  selector: 'app-qrcode-printer',
  templateUrl: './qrcode-printer.component.html',
  styleUrls: ['./qrcode-printer.component.scss'],
  standalone: true,
  imports: [CommonModule, NgxQRCodeModule, NgxPrintElementModule],
})
export class QrcodePrinterComponent extends InjectBase implements OnInit {
  data: OrderPrintResult[] = [];
  constructor(public printService: NgxPrintElementService) { super()}

  ngOnInit(): void {}

  async print(dataPrint: OrderPrintResult[]) {
    this.data = dataPrint;
    console.log(dataPrint);
    let time = interval(1);
    await time.pipe(take(1)).subscribe(async () => {
      // config print of ngx-print-element npm
      const config: Config = <Config>{
        printMode: 'template',
        popupProperties:
          'toolbar=yes,scrollbars=yes,resizable=yes,top=0,left=0,fullscreen=yes',
      };
      // print
      await this.printService.print('printData', config);
      // hidden list data print
      document.getElementById('printData').hidden = true;
    });
  }
}
