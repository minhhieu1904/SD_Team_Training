import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TransactionRoutingModule } from './transaction.routing';
import { PackingScanRoutingModule } from './packing-scan/packing-scan.routing'
@NgModule({
  imports: [
    CommonModule,
    TransactionRoutingModule,
    PackingScanRoutingModule,
  ],
  declarations: []
})
export class TransactionModule { }
