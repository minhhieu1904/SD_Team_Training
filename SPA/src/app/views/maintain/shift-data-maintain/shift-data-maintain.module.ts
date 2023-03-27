import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ShiftDataMaintainRoutingModule } from './shift-data-maintain-routing.module';
import { MainComponent } from './main/main.component';
import { AddComponent } from './add/add.component';
import { EditComponent } from './edit/edit.component';
import { FormsModule } from '@angular/forms';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { TranslateModule } from '@ngx-translate/core';


@NgModule({
  declarations: [
    MainComponent,
    AddComponent,
    EditComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ShiftDataMaintainRoutingModule,
    PaginationModule.forRoot(),
    TranslateModule
  ]
})
export class ShiftDataMaintainModule { }
