import { PickingListComponent } from './picking-list/picking-list.component';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { WarehouseOutScanRoutingModule } from './warehouse-out-scan-routing.module';
import { FormsModule } from '@angular/forms';
import { NgSelectModule } from '@ng-select/ng-select';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { ModalModule } from 'ngx-bootstrap/modal';
import { TranslateModule } from '@ngx-translate/core';
import { MainComponent } from './main/main.component';


@NgModule({
  declarations: [
    MainComponent, PickingListComponent
  ],
  imports: [
    FormsModule,
    NgSelectModule,
    BsDatepickerModule.forRoot(),
    ModalModule.forRoot(),
    PaginationModule.forRoot(),
    TranslateModule,
    CommonModule,
    WarehouseOutScanRoutingModule
  ]
})
export class WarehouseOutScanModule { }
