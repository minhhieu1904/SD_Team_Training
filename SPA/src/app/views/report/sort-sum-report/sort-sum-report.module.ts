import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SortSumReportRoutingModule } from './sort-sum-report-routing.module';
import { MainComponent } from './main/main.component';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { FormsModule } from '@angular/forms';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { NgSelectModule } from '@ng-select/ng-select';
import { AlertModule } from 'ngx-bootstrap/alert';
import { TranslateModule } from '@ngx-translate/core';

@NgModule({
  declarations: [MainComponent],
  imports: [
    CommonModule,
    SortSumReportRoutingModule,
    PaginationModule.forRoot(),
    FormsModule,
    BsDatepickerModule,
    NgSelectModule,
    AlertModule.forRoot(),
    TranslateModule,
  ],
})
export class SortSumReportModule {}
