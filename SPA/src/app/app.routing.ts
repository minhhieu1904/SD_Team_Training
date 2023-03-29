import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

// Import Containers
import { DefaultLayoutComponent } from './containers';
export const routes: Routes = [
  {
    path: '',
    component: DefaultLayoutComponent,
    data: {
      title: 'Home',
    },
    children: [
      {
        path: 'shift-data-maintain',
        loadChildren: () =>
          import(
            './views/maintain/shift-data-maintain/shift-data-maintain.module'
          ).then((m) => m.ShiftDataMaintainModule),
      },
      {
        path: 'warehouse-basic-data',
        loadChildren: () =>
          import(
            './views/maintain/warehouse-data-basic/warehouse-data-basic.module'
          ).then((m) => m.WareHouseDataBasicModule),
      },
      {
        path: 'department-data-maintain',
        loadChildren: () => 
        import('./views/maintain/department-data-maintain/department-data-maintain.module')
        .then(m => m.DepartmentDataMaintainModule)
      },
      {
        path: 'standard-packing-quantity',
        loadChildren: () => import('./views/maintain/standard-packing-quantity/standard-packing-quantity.module')
        .then(m => m.StandardPackingQuantityModule)
      }
    ],
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
