import { PaginationModule } from 'ngx-bootstrap/pagination';
import { NgSelectModule } from '@ng-select/ng-select';
import { TranslateModule } from '@ngx-translate/core';
import { FormsModule } from '@angular/forms';
import { MainComponent } from './main/main.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { OrderDataStatusAdjustRoutingModule } from './order-data-status-adjust-routing.module';


@NgModule({
  declarations: [
    MainComponent
  ],
  imports: [
    CommonModule,
    OrderDataStatusAdjustRoutingModule,
    FormsModule,
    TranslateModule,
    NgSelectModule,
    PaginationModule.forRoot()
  ]
})
export class OrderDataStatusAdjustModule { }
