import { NgSelectModule } from '@ng-select/ng-select';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ReportQrcodeWipRoutingModule } from './report-qrcode-wip-routing.module';
import { MainComponent } from './main/main.component';
import { FormsModule } from '@angular/forms';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';


@NgModule({
  declarations: [
    MainComponent
  ],
  imports: [
    CommonModule,
    PaginationModule.forRoot(),
    FormsModule,
    BsDatepickerModule,
    NgSelectModule,
    ReportQrcodeWipRoutingModule
  ]
})
export class ReportQrcodeWipModule { }
