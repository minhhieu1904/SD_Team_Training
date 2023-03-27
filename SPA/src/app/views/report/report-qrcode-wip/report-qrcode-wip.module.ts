import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ReportQrcodeWipRoutingModule } from './report-qrcode-wip-routing.module';
import { MainComponent } from './main/main.component';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { FormsModule } from '@angular/forms';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { NgSelectModule } from '@ng-select/ng-select';
import { TranslateModule } from '@ngx-translate/core';


@NgModule({
  declarations: [
    MainComponent
  ],
  imports: [
    CommonModule,
    ReportQrcodeWipRoutingModule,
    BsDatepickerModule,
    FormsModule,
    PaginationModule.forRoot(),
    NgSelectModule,
    TranslateModule
  ]
})
export class ReportQrcodeWipModule { }
