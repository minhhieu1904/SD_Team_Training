import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PaginationModule } from 'ngx-bootstrap/pagination';

import { WkshSumReportRoutingModule } from './wksh-sum-report-routing.module';
import { MainComponent } from './main/main.component';
import { FormsModule } from '@angular/forms';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { NgSelectModule } from '@ng-select/ng-select';

@NgModule({
  declarations: [
    MainComponent
  ],
  imports: [
    CommonModule,
    WkshSumReportRoutingModule,
    PaginationModule.forRoot(),
    FormsModule,
    BsDatepickerModule,
    NgSelectModule
  ]
})
export class WkshSumReportModule { }
