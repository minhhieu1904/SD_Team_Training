import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MaintainRoutingModule } from './maintain-routing.module';
import { FormsModule } from '@angular/forms';
import { PaginationModule } from 'ngx-bootstrap/pagination';


@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    MaintainRoutingModule,
    FormsModule,
    PaginationModule.forRoot()
  ]
})
export class MaintainModule { }
