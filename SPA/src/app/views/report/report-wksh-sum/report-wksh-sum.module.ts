import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker'
import { ReportWkshSumRoutingModule } from './report-wksh-sum-routing.module';
import { MainComponent } from './main/main.component';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { FormsModule } from '@angular/forms';
import { NgSelectModule } from '@ng-select/ng-select';
import { TranslateModule } from '@ngx-translate/core';


@NgModule({
  declarations: [
    MainComponent
  ],
  imports: [
    CommonModule,
    BsDatepickerModule,
    ReportWkshSumRoutingModule,
    FormsModule,
    PaginationModule.forRoot(),
    NgSelectModule,
    TranslateModule
  ]
})
export class ReportWkshSumModule { }
