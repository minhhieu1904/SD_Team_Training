import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StorageSumReportRoutingModule } from './storage-sum-report-routing.module';
import { MainComponent } from './main/main.component';
import { FormsModule } from '@angular/forms';
import { NgSelectModule } from '@ng-select/ng-select';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { PaginationModule } from 'ngx-bootstrap/pagination';


@NgModule({
  declarations: [
    MainComponent
  ],
  imports: [
    CommonModule,
    StorageSumReportRoutingModule,
    FormsModule,
    NgSelectModule,
    BsDatepickerModule.forRoot(),
    PaginationModule.forRoot()
  ]
})
export class StorageSumReportModule { }
