import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShiftDataMaintenaceRoutingModule } from './shift-data-maintenance.routing';
import { FormsModule } from '@angular/forms';
import { MainComponent } from './main/main.component';
import { FormComponent } from './form/form.component';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { TransactionModule } from '../../transaction/transaction.module';
import { TranslateModule } from '@ngx-translate/core';

@NgModule({
  imports: [
    CommonModule,
    ShiftDataMaintenaceRoutingModule,
    FormsModule,
    PaginationModule.forRoot(),
    TranslateModule
  ],
  declarations: [
    MainComponent,
    FormComponent
  ]
})
export class ShiftDataMaintenaceModule { }
