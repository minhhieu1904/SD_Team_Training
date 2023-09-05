import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { WarehouseBasicDataMaintenanceRoutingModule } from './warehouse-basic-data-maintenance-routing.module';
import { MainComponent } from './main/main.component';
import { FormComponent } from './form/form.component';
import { FormsModule } from '@angular/forms';
import { TranslateModule } from '@ngx-translate/core';
import { ModalModule } from 'ngx-bootstrap/modal';
import { PaginationModule } from 'ngx-bootstrap/pagination';


@NgModule({
  declarations: [
    MainComponent,
    FormComponent,
  ],
  imports: [
    CommonModule,
    WarehouseBasicDataMaintenanceRoutingModule,
    FormsModule,
    TranslateModule,
    ModalModule.forRoot(),
    PaginationModule
  ]
})
export class WarehouseBasicDataMaintenanceModule { }
