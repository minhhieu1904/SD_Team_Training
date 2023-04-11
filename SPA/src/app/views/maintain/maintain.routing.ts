import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [
  {
    path: 'shift-data-maintenance',
    loadChildren: () => import('./shift-data-maintenance/shift-data-maintenance.module').then(m => m.ShiftDataMaintenaceModule)
  },
  {
    path: 'warehouse-basic-data-maintenance',
    loadChildren: () => import('./warehouse-basic-data-maintenance/warehouse-basic-data-maintenance.module').then(m => m.WarehouseBasicDataMaintenanceModule)
  },
  {
    path: 'department-data-maintenance',
    loadChildren: () => import('./department-data-maintenance/department-data-maintenance.module').then(m => m.DepartmentDataMaintenanceModule)
  },
  {
    path: 'standard-packing-quantity-setting',
    loadChildren: () => import('./standard-packing-quantity-setting/standard-packing-quantity-setting.module').then(m => m.StandardPackingQuantitySettingModule)
  },
  {
    path: 'authorization-setting',
    loadChildren: () => import('./authorization-setting/authorization-setting.module').then(m => m.AuthorizationSettingModule)
  }
];
@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MaintainRoutingModule { }
