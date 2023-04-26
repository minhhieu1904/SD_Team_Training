import { FormComponent } from './form/form.component';
import { NgSelectModule } from '@ng-select/ng-select';
import { TranslateModule } from '@ngx-translate/core';
import { ModalModule } from 'ngx-bootstrap/modal';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { PackingScanExportComponent } from './../../common/packing-scan-export/packing-scan-export.component';
import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PackingScanRoutingModule } from './packing-scan-routing.module';
import { NgxPrintElementModule } from 'ngx-print-element';
import { MainComponent } from './main/main.component';


@NgModule({
  declarations: [
    MainComponent,
    FormComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    NgSelectModule,
    TranslateModule,
    ModalModule.forRoot(),
    PaginationModule.forRoot(),
    NgxPrintElementModule,
    PackingScanExportComponent,
    PackingScanRoutingModule
  ]
})
export class PackingScanModule { }
