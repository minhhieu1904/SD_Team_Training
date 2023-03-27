import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MaintainRoutingModule } from './maintain-routing.module';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { FormsModule } from '@angular/forms';
import { TranslateModule } from '@ngx-translate/core';
import { ModalModule } from 'ngx-bootstrap/modal';


@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    MaintainRoutingModule,
    FormsModule,
    PaginationModule.forRoot(),
    TranslateModule,
    ModalModule.forRoot()
  ]
})
export class MaintainModule { }
