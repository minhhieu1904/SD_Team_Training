import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { NgxPrintElementModule } from 'ngx-print-element';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { TranslateModule } from '@ngx-translate/core';
import { NgSelectModule } from '@ng-select/ng-select';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ReprintWarehouseScanRoutingModule } from './reprint-warehouse-scan-routing.module';
import { BsModalService } from 'ngx-bootstrap/modal';


@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    ReprintWarehouseScanRoutingModule,
    NgSelectModule,
    TranslateModule,
    PaginationModule.forRoot(),
    NgxPrintElementModule,
    BsDatepickerModule.forRoot(),
    FormsModule,
    ReactiveFormsModule,
  ],
  providers: [BsModalService]
})
export class ReprintWarehouseScanModule { }
