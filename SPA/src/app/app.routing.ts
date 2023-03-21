import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

// Import Containers
import { DefaultLayoutComponent } from './containers';
export const routes: Routes = [
  {
    path: '',
    component: DefaultLayoutComponent,
    data: {
      title: 'Home'
    },
    children: [
      {
        path: 'shift-data-maintain',
        loadChildren: () => import('./views/shift-data-maintain/shift-data-maintain.module').then(m => m.ShiftDataMaintainModule)
      },
      {
        path: 'department-data-maintenance',
        loadChildren: () => import('./views/department-data-maintenance/department-data-maintenance.module').then(m => m.DepartmentDataMaintenanceModule)
      },
      {
        path: 'warehouse-basic-data',
        loadChildren: () => import('./views/warehouse-basic-data/warehouse-basic-data.module').then(m => m.WarehouseBasicDataModule)
      },
      {
        path: 'standard-packing-quantity',
        loadChildren: () => import('./views/standard-packing-quantity/standard-packing-quantity.module').then(m => m.StandardPackingQuantityModule)
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
