import { MainComponent } from './main/main.component';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { TranslateModule } from '@ngx-translate/core';
import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SortNewreportByMidasProvideRoutingModule } from './sort-newreport-by-midas-provide-routing.module';


@NgModule({
  declarations: [
    MainComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    TranslateModule,
    BsDatepickerModule.forRoot(),
    SortNewreportByMidasProvideRoutingModule
  ]
})

export class SortNewreportByMidasProvideModule { }
