import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

// Import Containers
import { DefaultLayoutComponent } from './containers';
import { LoginComponent } from './views/login/login/login.component';
export const routes: Routes = [
  {
    path: '',
    component: DefaultLayoutComponent,
    data: {
      title: 'Home'
    },
    children: [
      {
        path: 'maintain/shift-data-maintain',
        loadChildren: () => import('./views/shift-data-maintain/shift-data-maintain.module').then(m => m.ShiftDataMaintainModule)
      },
      {
        path: 'maintain/department-data-maintenance',
        loadChildren: () => import('./views/department-data-maintenance/department-data-maintenance.module').then(m => m.DepartmentDataMaintenanceModule)
      },
      {
        path: 'maintain/warehouse-basic-data',
        loadChildren: () => import('./views/warehouse-basic-data/warehouse-basic-data.module').then(m => m.WarehouseBasicDataModule)
      },
      {
        path: 'maintain/standard-packing-quantity',
        loadChildren: () => import('./views/standard-packing-quantity/standard-packing-quantity.module').then(m => m.StandardPackingQuantityModule)
      },
      {
        path: 'maintain/authorization-setting',
        loadChildren: () => import('./views/authorization-setting/authorization-setting.module').then(m => m.AuthorizationSettingModule)
      }
    ]
  },
  {
    path: 'login',
    component: LoginComponent,
    data: {
      title: 'Login Page'
    }
  },{
    path: 'default',
    component: DefaultLayoutComponent,
    data: {
      title: 'Default Page'
    }
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
