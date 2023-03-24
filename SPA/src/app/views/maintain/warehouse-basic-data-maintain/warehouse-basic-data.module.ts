import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { WarehouseBasicDataRoutingModule } from './warehouse-basic-data-routing.module';
import { MainComponent} from "./main/main.component";
import { AddComponent} from "./add/add.component";
import { EditComponent} from "./edit/edit.component";
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
