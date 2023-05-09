import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MainComponent } from './main/main.component';
import { PackingScanRoutingModule } from './packing-scan.routing';
import { TranslateModule } from '@ngx-translate/core';
import { NgSelectModule } from '@ng-select/ng-select';
import { FormsModule } from '@angular/forms';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { ModalModule } from 'ngx-bootstrap/modal';
import { PackingScanExportComponent } from './../packing-scan-export/packing-scan-export.component'
import { NgxPrintElementModule } from 'ngx-print-element';
import { FormComponent } from './form/form.component';

@NgModule({
  imports: [
    CommonModule,
    PackingScanRoutingModule,
    TranslateModule,
    NgSelectModule,
    FormsModule,
    PaginationModule.forRoot(),
    ModalModule.forRoot(),
    PackingScanExportComponent,
    NgxPrintElementModule,
  ],
  declarations: [
    MainComponent,
    FormComponent
  ],
})
export class PackingScanModule { }
