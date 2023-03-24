import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [   {
       path: 'shift-data-maintain',
       loadChildren: () =>
         import(
           '../maintain/shift-data-maintain/shift-data-maintain.module'
         ).then((m) => m.ShiftDataMaintainModule),
     },
     {
       path: 'warehouse-basic-data-maintain',
       loadChildren: () =>
         import(
           '../maintain/warehouse-basic-data-maintain/warehouse-basic-data.module'
         ).then((m) => m.WarehouseBasicDataModule),
     },
     {
       path: 'department-data-maintain',
       loadChildren: () =>
         import(
           '../maintain/department-data-maintain/department-data-maintain.module'
         ).then((m) => m.DepartmentDataMaintainModule),
     },
     {
       path: 'standard-packing-quantity',
       loadChildren: () =>
         import(
           '../maintain/standard-packing-quantity/standard-packing-quantity.module'
         ).then((m) => m.StandardPackingQuantityModule),
     },
     {
       path: 'authorization-setting',
       loadChildren: () =>
         import(
           '../maintain/authorization-setting/authorization-setting.module'
         ).then((m) => m.AuthorizationSettingModule),
     },];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MaintainRoutingModule { }
