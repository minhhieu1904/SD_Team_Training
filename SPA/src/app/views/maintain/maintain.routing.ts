import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AppGuard } from '@guards/app.guard';
import { getInfoMenu } from '@utilities/function-utility';

const routes: Routes = [
  {
    path: 'shift-data-maintenance',
    canLoad: [AppGuard],
    loadChildren: () =>
      import('./shift-data-maintenance/shift-data-maintenance.module').then(
        (m) => m.ShiftDataMaintenaceModule
      ),
    data: {
      role: getInfoMenu('1.1')?.unique,
    },
  },
  {
    path: 'warehouse-basic-data-maintenance',
    canLoad: [AppGuard],
    loadChildren: () =>
      import(
        './warehouse-basic-data-maintenance/warehouse-basic-data-maintenance.module'
      ).then((m) => m.WarehouseBasicDataMaintenanceModule),
    data: {
      role: getInfoMenu('1.2')?.unique,
    },
  },
  {
    path: 'department-data-maintenance',
    canLoad: [AppGuard],
    loadChildren: () =>
      import(
        './department-data-maintenance/department-data-maintenance.module'
      ).then((m) => m.DepartmentDataMaintenanceModule),
    data: {
      role: getInfoMenu('1.3')?.unique,
    },
  },
  {
    path: 'standard-packing-quantity-setting',
    canLoad: [AppGuard],
    loadChildren: () =>
      import(
        './standard-packing-quantity-setting/standard-packing-quantity-setting.module'
      ).then((m) => m.StandardPackingQuantitySettingModule),
    data: {
      role: getInfoMenu('1.4')?.unique,
    },
  },
  {
    path: 'authorization-setting',
    canLoad: [AppGuard],
    loadChildren: () =>
      import('./authorization-setting/authorization-setting.module').then(
        (m) => m.AuthorizationSettingModule
      ),
    data: {
      role: getInfoMenu('1.5')?.unique,
    },
  },
];
@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class MaintainRoutingModule {}
