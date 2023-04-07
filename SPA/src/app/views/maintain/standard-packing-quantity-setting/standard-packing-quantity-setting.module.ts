import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StandardPackingQuantitySettingRoutingModule } from './standard-packing-quantity-setting.routing';
import { FormsModule } from '@angular/forms';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { PackageMainComponent } from './package-main/package-main.component';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    StandardPackingQuantitySettingRoutingModule,
    PaginationModule.forRoot()
  ],
  declarations: [
    PackageMainComponent
  ]
})
export class StandardPackingQuantitySettingModule { }
