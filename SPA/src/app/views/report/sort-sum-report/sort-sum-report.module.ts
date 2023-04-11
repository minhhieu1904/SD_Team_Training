import { PaginationModule } from 'ngx-bootstrap/pagination';
import { NgSelectModule } from '@ng-select/ng-select';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SortSumReportRoutingModule } from './sort-sum-report-routing.module';
import { MainComponent } from './main/main.component';
import { FormsModule } from '@angular/forms';
import { TranslateModule } from '@ngx-translate/core';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';


@NgModule({
  declarations: [
    MainComponent
  ],
  imports: [
    CommonModule,
    SortSumReportRoutingModule,
    NgSelectModule,
    FormsModule,
    PaginationModule.forRoot(),
    TranslateModule,
    BsDatepickerModule.forRoot()
  ]
})
export class SortSumReportModule { }
