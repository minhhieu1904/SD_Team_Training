import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { QrcodeWipReportRoutingModule } from './qrcode-wip-report-routing.module';
import { MainComponent } from './main/main.component';
import { FormsModule } from '@angular/forms';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';


@NgModule({
  declarations: [
    MainComponent
  ],
  imports: [
    CommonModule,
    QrcodeWipReportRoutingModule,
    FormsModule,
    PaginationModule.forRoot(),
    BsDatepickerModule.forRoot(),
  ]
})
export class QrcodeWipReportModule { }
