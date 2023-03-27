import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ReprintPackingScanRoutingModule } from './reprint-packing-scan-routing.module';
import { MainComponent } from './main/main.component';
import { TranslateModule } from '@ngx-translate/core';
import { NgSelectModule } from '@ng-select/ng-select';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { NgxPrintElementModule } from 'ngx-print-element';
import { PackingScanExportComponent } from '../../commons/packing-scan-export/packing-scan-export.component';


@NgModule({
  declarations: [
    MainComponent
  ],
  imports: [
    CommonModule,
    ReprintPackingScanRoutingModule,
    NgSelectModule,
    TranslateModule,
    PaginationModule.forRoot(),
    NgxPrintElementModule,
    BsDatepickerModule.forRoot(),
    FormsModule,
    ReactiveFormsModule,
    PackingScanExportComponent
  ]
})
export class ReprintPackingScanModule { }
