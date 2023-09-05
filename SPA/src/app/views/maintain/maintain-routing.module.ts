import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: 'shift-data-maintenance',
    loadChildren: () => import('./1_1_shift-data-maintainance/shift-data-maintainance.module').then(m => m.ShiftDataMaintainanceModule)
  },
  {
    path: 'warehouse-basic-data-maintenance',
    loadChildren: () => import('./1_2_warehouse-basic-data-maintenance/warehouse-basic-data-maintenance.module').then(m => m.WarehouseBasicDataMaintenanceModule)
  },
  {
    path: 'authorization-setting',
    loadChildren: () => import('./1_5_authorization-setting/authorization-setting.module').then(m => m.AuthorizationSettingModule)
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MaintainRoutingModule { }
