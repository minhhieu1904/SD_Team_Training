import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { WarehouseBasicDataRoutingModule } from './warehouse-basic-data-routing.module';
import { FormsModule } from '@angular/forms';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { MainComponent } from '../warehouse-basic-data/main/main.component';
import { AddComponent } from '../warehouse-basic-data/add/add.component';
import { EditComponent } from '../warehouse-basic-data/edit/edit.component';
import { TranslateModule } from '@ngx-translate/core';
import { AlertModule } from 'ngx-bootstrap/alert';

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
    PaginationModule.forRoot(),
    TranslateModule,
    AlertModule.forRoot()
  ]
})
export class WarehouseBasicDataModule { }
