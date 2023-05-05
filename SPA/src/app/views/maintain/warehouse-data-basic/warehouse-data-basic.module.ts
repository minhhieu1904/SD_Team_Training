import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { WareHouseDataBasicRoutingModule } from './warehouse-data-basic-routing.module';
import { MainComponent } from './main/main.component';
import { AddComponent } from './add/add.component';
import { EditComponent } from './edit/edit.component';
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
    WareHouseDataBasicRoutingModule,
    PaginationModule.forRoot()
  ]
})
export class WareHouseDataBasicModule { }
