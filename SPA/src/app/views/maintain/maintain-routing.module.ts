import { AppGuard } from './../../_core/guards/app.guard';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { getInfoMenu } from '@utilities/function-utility';


const routes: Routes = [
  {
    path: 'department-data-maintenance',
    canLoad: [AppGuard],
    loadChildren: () => import('./department-data/department-data.module')
      .then((m) => m.DepartmentDataModule),
    data: {
        role: getInfoMenu('1.3')?.unique
    }
  },
  {

    path: 'shift-data-maintenance',
    canLoad: [AppGuard],
    loadChildren: () => import('./shift-data/shift-data.module')
      .then((m) => m.ShiftDataModule),
      data: {
        role: getInfoMenu('1.1')?.unique
    }
  },
  {
    path: 'warehouse-basic-data-maintenance',
    canLoad: [AppGuard],
    loadChildren: () => import('./warehouse-data/warehouse-data.module')
      .then((m) => m.WarehouseDataModule),
      data: {
        role: getInfoMenu('1.2')?.unique
    }
  },
  {
    path: 'standard-packing-quantity-setting',
    canLoad: [AppGuard],
    loadChildren: () => import('./package-data/package-data.module')
      .then((m) => m.PackageDataModule),
      data: {
        role: getInfoMenu('1.4')?.unique
    }
  },
  {
    path: 'authorization-setting',
    canLoad: [AppGuard],
    loadChildren: () => import('./users-data/users-data.module')
      .then((m) => m.UsersDataModule),
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
