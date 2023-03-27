import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CancelStickerRoutingModule } from './cancel-sticker-routing.module';
import { MainComponent } from './main/main.component';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { FormsModule } from '@angular/forms';
import { TranslateModule } from '@ngx-translate/core';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { FormComponent } from './form/form.component';
import { ModalModule } from 'ngx-bootstrap/modal';


@NgModule({
  declarations: [
    MainComponent,
    FormComponent
  ],
  imports: [
    CommonModule,
    CancelStickerRoutingModule,
    BsDatepickerModule.forRoot(),
    FormsModule,
    TranslateModule,
    PaginationModule.forRoot(),
    ModalModule.forRoot()
  ]
})
export class CancelStickerModule { }
