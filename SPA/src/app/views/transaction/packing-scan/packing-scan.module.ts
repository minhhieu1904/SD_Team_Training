import { PackingScanExportComponent } from './../../commons/packing-scan-export/packing-scan-export.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PackingScanRoutingModule } from './packing-scan-routing.module';
import { MainComponent } from './main/main.component';
import { FormComponent } from './form/form.component';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { FormsModule } from '@angular/forms';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { NgSelectModule } from '@ng-select/ng-select';
import { TranslateModule } from '@ngx-translate/core';
import { ModalModule } from 'ngx-bootstrap/modal';
import { NgxPrintElementModule } from 'ngx-print-element';
import { ModalService } from '@services/common/modal.service';


@NgModule({
  declarations: [
    MainComponent,
    FormComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    PackingScanRoutingModule,
    NgSelectModule,
    TranslateModule,
    BsDatepickerModule.forRoot(),
    ModalModule.forRoot(),
    PaginationModule.forRoot(),
    NgxPrintElementModule,
    PackingScanExportComponent
  ],
})
export class PackingScanModule { }
