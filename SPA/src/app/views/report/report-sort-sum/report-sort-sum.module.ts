import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ReportSortSumRoutingModule } from './report-sort-sum-routing.module';
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
    ReportSortSumRoutingModule,
    BsDatepickerModule,
    FormsModule,
    PaginationModule.forRoot(),
    NgSelectModule,
    TranslateModule
  ]
})
export class ReportSortSumModule { }
