import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SearchForOrderDataRoutingModule } from './search-for-order-data-routing.module';
import { MainComponent } from './main/main.component';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { FormsModule } from '@angular/forms';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { NgSelectModule } from '@ng-select/ng-select';


@NgModule({
  declarations: [
    MainComponent
  ],
  imports: [
    CommonModule,
    SearchForOrderDataRoutingModule,
    PaginationModule.forRoot(),
    FormsModule,
    BsDatepickerModule,
    NgSelectModule
  ]
})
export class SearchForOrderDataModule { }
