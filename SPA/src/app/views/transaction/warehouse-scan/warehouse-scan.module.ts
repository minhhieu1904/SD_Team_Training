import { TranslateModule } from '@ngx-translate/core';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { ModalModule } from 'ngx-bootstrap/modal';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { NgSelectModule } from '@ng-select/ng-select';
import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { WarehouseScanRoutingModule } from './warehouse-scan-routing.module';
import { MainComponent } from './main/main.component';
import { FormComponent } from './form/form.component';


@NgModule({
  declarations: [
    MainComponent,
    FormComponent
  ],
  imports: [
    FormsModule,
    CommonModule,
    NgSelectModule,
    BsDatepickerModule.forRoot(),
    ModalModule.forRoot(),
    PaginationModule.forRoot(),
    TranslateModule,
    WarehouseScanRoutingModule
  ]
})
export class WarehouseScanModule { }
