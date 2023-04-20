import { NgModule } from '@angular/core';
import { getInfoMenu } from '@utilities/function-utility';
import { RouterModule, Routes } from '@angular/router';
import { AppGuard } from '@guards/app.guard';
import { RolesConstants_role } from '@constants/role.constants';
const routes: Routes = [
  {
    path: 'shift-data-maintain',
    canLoad: [AppGuard],
    loadChildren: () => import('../maintain/shift-data-maintain/shift-data-maintain.module').then(m => m.ShiftDataMaintainModule),
    data: { 
      unique: RolesConstants_role.ShiftDataMaintenance
    }
  },
  {
    path: 'warehouse-basic-data',
    canLoad: [AppGuard],
    loadChildren: () => import('../maintain/warehouse-basic-data/warehouse-basic-data.module').then(m => m.WarehouseBasicDataModule),
    data: { 
      unique: RolesConstants_role.WarehouseBasicDataMaintenance
    }
  },
  {
    path: 'department-data-maintenance',
    canLoad: [AppGuard],
    loadChildren: () => import('../maintain/department-data-maintenance/department-data-maintenance.module').then(m => m.DepartmentDataMaintenanceModule),
    data: { 
      unique: RolesConstants_role.DepartmentDataMaintenance
    }
  },
  {
    path: 'standard-packing-quantity',
    canLoad: [AppGuard],
    loadChildren: () => import('../maintain/standard-packing-quantity/standard-packing-quantity.module').then(m => m.StandardPackingQuantityModule),
    data: { 
      unique: RolesConstants_role.StandardPackingQuantitySetting
    }
  },
  {
    path: 'authorization-setting',
    canLoad: [AppGuard],
    loadChildren: () => import('../maintain/authorization-setting/authorization-setting.module').then(m => m.AuthorizationSettingModule),
    data: { 
      unique: RolesConstants_role.AuthorizationSetting
    }
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MaintainRoutingModule { }
