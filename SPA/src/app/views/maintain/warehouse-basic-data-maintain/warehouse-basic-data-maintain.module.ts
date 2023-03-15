import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { WarehouseBasicDataMaintainRoutingModule } from './warehouse-basic-data-maintain-routing.module';
import { MainComponent } from './main/main.component';
import { AddComponent } from './add/add.component';
import { EditComponent } from './edit/edit.component';


@NgModule({
  declarations: [
    MainComponent,
    AddComponent,
    EditComponent
  ],
  imports: [
    CommonModule,
    WarehouseBasicDataMaintainRoutingModule
  ]
})
export class WarehouseBasicDataMaintainModule { }
