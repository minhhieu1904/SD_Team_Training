import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ReportRoutingModule } from './report-routing.module';
import { NgSelectModule } from '@ng-select/ng-select';
import { FormsModule } from '@angular/forms';
import { PaginationModule } from 'ngx-bootstrap/pagination';


@NgModule({
  declarations: [

  ],
  imports: [
    CommonModule,
    NgSelectModule,
    FormsModule,
    ReportRoutingModule,
    PaginationModule.forRoot(),
  ]
})
export class ReportModule { }
