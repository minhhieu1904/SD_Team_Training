import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppGuard } from '@guards/app.guard';
import { getInfoMenu } from '@utilities/function-utility';

const routes: Routes = [
  {
    path: 'shift-data-maintenance',
    canLoad: [AppGuard],
    loadChildren: () =>
      import('./shift-data-maintain/shift-data-maintain.module').then((m) => m.ShiftDataMaintainModule),
      data: {
        role: getInfoMenu('1.1')?.unique
    }
  },{
    path: 'warehouse-basic-data-maintenance',
    canLoad: [AppGuard],
    loadChildren: () =>
      import(
        './warehouse-data-basic/warehouse-data-basic.module'
      ).then((m) => m.WareHouseDataBasicModule),
      data: {
        role: getInfoMenu('1.2')?.unique
    }
  },{
    path: 'department-data-maintenance',
    canLoad: [AppGuard],
    loadChildren: () =>
      import(
        './department-data-maintain/department-data-maintain.module'
      ).then((m) => m.DepartmentDataMaintainModule),
      data: {
        role: getInfoMenu('1.3')?.unique
    }
  },{
    path: 'standard-packing-quantity-setting',
    canLoad: [AppGuard],
    loadChildren: () =>
      import(
        './standard-packing-quantity/standard-packing-quantity.module'
      ).then((m) => m.StandardPackingQuantityModule),
      data: {
        role: getInfoMenu('1.4')?.unique
    }
  },
  {
    path: 'authorization-setting',
    canLoad: [AppGuard],
    loadChildren: () =>
      import(
        './authorization-setting/authorization-setting.module'
      ).then((m) => m.AuthorizationSettingModule),
      data: {
        role: getInfoMenu('1.5')?.unique
    }
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MaintainRoutingModule { }
