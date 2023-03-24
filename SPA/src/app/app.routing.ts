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
            './views/maintain/warehouse-basic-data-maintain/warehouse-basic-data.module'
          ).then((m) => m.WarehouseBasicDataModule),
      },
      {
        path: 'department-data-maintain',
        loadChildren: () =>
          import(
            './views/maintain/department-data-maintain/department-data-maintain.module'
          ).then((m) => m.DepartmentDataMaintainModule),
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
