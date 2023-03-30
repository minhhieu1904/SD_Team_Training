import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
const routes: Routes = [
  {
    path: 'shift-data-maintain',
    loadChildren: () => import('../maintain/shift-data-maintain/shift-data-maintain.module').then(m => m.ShiftDataMaintainModule)
  },
  {
    path: 'warehouse-basic-data',
    loadChildren: () => import('../maintain/warehouse-basic-data/warehouse-basic-data.module').then(m => m.WarehouseBasicDataModule)
  },
  {
    path: 'department-data-maintenance',
    loadChildren: () => import('../maintain/department-data-maintenance/department-data-maintenance.module').then(m => m.DepartmentDataMaintenanceModule)
  },
  {
    path: 'standard-packing-quantity',
    loadChildren: () => import('../maintain/standard-packing-quantity/standard-packing-quantity.module').then(m => m.StandardPackingQuantityModule)
  },
  {
    path: 'authorization-setting',
    loadChildren: () => import('../maintain/authorization-setting/authorization-setting.module').then(m => m.AuthorizationSettingModule)
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MaintainRoutingModule { }
