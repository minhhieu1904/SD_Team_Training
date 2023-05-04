import { Component, OnInit } from '@angular/core';
import {
  Config,
  NgxPrintElementModule,
  NgxPrintElementService,
} from 'ngx-print-element';
import { interval, take } from 'rxjs';
import { OrderPrintResult } from '@models/transaction/searchForOrderDataDTO';
import { CommonModule } from '@angular/common';
import { QRCodeModule } from 'angularx-qrcode';
@Component({
  selector: 'app-qrcode-printer',
  templateUrl: './qrcode-printer.component.html',
  styleUrls: ['./qrcode-printer.component.scss'],
  standalone: true,
  imports: [CommonModule, QRCodeModule, NgxPrintElementModule],
})
export class QrcodePrinterComponent implements OnInit {
  constructor(public printService: NgxPrintElementService) {}
  data: OrderPrintResult[] = [];

  ngOnInit() {}

  async print(dataPrint: OrderPrintResult[]) {
    this.data = dataPrint;
    let time = interval(1);
    await time.pipe(take(1)).subscribe(async () => {
      // config print of ngx-print-element npm
      const config: Config = <Config>{
        printMode: 'template',
        // popupProperties: 'toolbar=yes,scrollbars=yes,resizable=yes,top=0,left=0,fullscreen=yes'
      };
      // print
      await this.printService.print('printData', config);
      // hidden list data print
      document.getElementById('printData').hidden = true;
    });
  }
}
