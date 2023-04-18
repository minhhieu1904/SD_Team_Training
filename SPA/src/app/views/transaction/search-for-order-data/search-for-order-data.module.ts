import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SearchForOrderDataRoutingModule } from './search-for-order-data-routing.module';
import { MainComponent } from './../../transaction/search-for-order-data/main/main.component';
import { NgSelectModule } from '@ng-select/ng-select';
import { FormsModule } from '@angular/forms';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { TranslateModule } from '@ngx-translate/core';
import { QrcodePrinterComponent } from './../qrcode-printer/qrcode-printer.component';

@NgModule({
  declarations: [MainComponent],
  imports: [
    CommonModule,
    SearchForOrderDataRoutingModule,
    NgSelectModule,
    FormsModule,
    PaginationModule.forRoot(),
    TranslateModule,
    QrcodePrinterComponent,
  ],
  exports: [QrcodePrinterComponent],
})
export class SearchForOrderDataModule {}
