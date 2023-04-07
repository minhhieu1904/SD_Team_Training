import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { WkshSumReportRoutingModule } from './wksh-sum-report-routing.module';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { MainComponent } from './main/main.component';
import { TranslateModule } from '@ngx-translate/core';
import { NgSelectModule } from '@ng-select/ng-select';
import { PaginationModule } from 'ngx-bootstrap/pagination';


@NgModule({
  declarations: [
    MainComponent
  ],
  imports: [
    CommonModule,
    WkshSumReportRoutingModule,
    TranslateModule,
    BsDatepickerModule.forRoot(),
    FormsModule,
    NgSelectModule,
    PaginationModule.forRoot()
  ]
})
export class WkshSumReportModule { }
