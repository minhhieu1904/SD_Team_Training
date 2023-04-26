import { QrcodePrinterComponent } from '../../common/qrcode-printer/qrcode-printer.component';
import { TranslateModule } from '@ngx-translate/core';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { NgSelectModule } from '@ng-select/ng-select';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SearchForOrderDataRoutingModule } from './search-for-order-data-routing.module';
import { MainComponent } from './main/main.component';
import { FormsModule } from '@angular/forms';


@NgModule({
  declarations: [
    MainComponent
  ],
  imports: [
    CommonModule,
    SearchForOrderDataRoutingModule,
    NgSelectModule,
    FormsModule,
    PaginationModule.forRoot(),
    TranslateModule,
    QrcodePrinterComponent
  ],
  exports: [QrcodePrinterComponent],
})
export class SearchForOrderDataModule { }
