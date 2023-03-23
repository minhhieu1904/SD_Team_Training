import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { WarehouseBasicDataRoutingModule } from './warehouse-basic-data-routing.module';
import { MainComponent} from "../warehouse-basic-data/main/main.component";
import { AddComponent} from "../warehouse-basic-data/add/add.component";
import { EditComponent} from "../warehouse-basic-data/edit/edit.component";
import { FormsModule } from '@angular/forms';
import { PaginationModule } from 'ngx-bootstrap/pagination';

@NgModule({
  declarations: [
     MainComponent,
    AddComponent,
    EditComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    WarehouseBasicDataRoutingModule,
    PaginationModule.forRoot()
  ]
})
export class WarehouseBasicDataModule { }
