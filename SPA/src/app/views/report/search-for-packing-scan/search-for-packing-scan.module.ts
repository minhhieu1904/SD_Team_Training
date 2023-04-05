import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SearchForPackingScanRoutingModule } from './search-for-packing-scan-routing.module';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { FormsModule } from '@angular/forms';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { NgSelectModule } from '@ng-select/ng-select';
import { MainComponent } from './main/main.component';

@NgModule({
  declarations: [MainComponent],
  imports: [
    CommonModule,
    SearchForPackingScanRoutingModule,
    PaginationModule.forRoot(),
    FormsModule,
    BsDatepickerModule,
    NgSelectModule,
  ],
})
export class SearchForPackingScanModule {}
