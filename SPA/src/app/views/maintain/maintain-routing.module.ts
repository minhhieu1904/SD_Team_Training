import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';


const routes: Routes = [
  {
    path: 'shift-data-maintenance',
    loadChildren: () => import('./shift-data-maintain/shift-data-maintain.module').then(m => m.ShiftDataMaintainModule)
  },
  {
    path: 'warehouse-basic-data-maintenance',

    loadChildren: () => import('./warehouse-basic-data-maintain/warehouse-basic-data-maintain.module').then(m => m.WarehouseBasicDataMaintainModule)
  },
  {
    path: 'department-data-maintenance',

    loadChildren: () => import('./department-data-maintain/department-data-maintain.module').then(m => m.DepartmentDataMaintainModule)
  },
  {
    path: 'standard-packing-quantity-setting',

    loadChildren: () => import('./package-data-maintain/package-data-maintain.module').then(m => m.PackageDataMaintainModule)
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
