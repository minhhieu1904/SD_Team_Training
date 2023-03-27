import { QrcodePrinterComponent } from './../../commons/qrcode-printer/qrcode-printer.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SearchForOrderDataRoutingModule } from './search-for-order-data-routing.module';
import { MainComponent } from './main/main.component';
import { NgSelectModule } from '@ng-select/ng-select';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { FormsModule } from '@angular/forms';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { TranslateModule } from '@ngx-translate/core';


@NgModule({
  declarations: [
    MainComponent
  ],
  imports: [
    CommonModule,
    SearchForOrderDataRoutingModule,
    BsDatepickerModule,
    FormsModule,
    PaginationModule.forRoot(),
    NgSelectModule,
    TranslateModule,
    QrcodePrinterComponent

  ],
  exports: [QrcodePrinterComponent]
})
export class SearchForOrderDataModule { }
