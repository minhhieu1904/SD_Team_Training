import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ShiftDataMaintainanceRoutingModule } from './shift-data-maintainance-routing.module';
import { MainComponent } from './main/main.component';
import { FormsModule } from '@angular/forms';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { TranslateModule } from '@ngx-translate/core';
import { ModalModule } from 'ngx-bootstrap/modal';
import { FormComponent } from './form/form.component';


@NgModule({
  declarations: [
    MainComponent,
    FormComponent,
  ],
  imports: [
    CommonModule,
    ShiftDataMaintainanceRoutingModule,
    FormsModule,
    PaginationModule,
    TranslateModule,
    ModalModule.forRoot()
  ]
})
export class ShiftDataMaintainanceModule { }
