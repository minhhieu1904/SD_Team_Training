import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { WkshSumReportResolver } from '@resolvers/report/wksh-sum-report.resolver';
import { SortSumReportResolver } from '@resolvers/report/sort-sum-report.resolver';
import { ReportRoutingModule } from './report-routing.module';


@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    ReportRoutingModule
  ],
  providers: [
    WkshSumReportResolver,
    SortSumReportResolver
  ]
})
export class ReportModule { }
