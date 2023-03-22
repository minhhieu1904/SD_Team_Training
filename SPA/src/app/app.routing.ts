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
        path: 'shift-data',
        loadChildren: () => import('./views/shift-data/shift-data.module').then(m => m.ShiftDataModule )
      },
      {
        path: 'warehouse-data',
        loadChildren: () => import('./views/warehouse-data/warehouse-data.module').then(m => m.WarehouseDataModule )
      },
      {
        path: 'department-data',
        loadChildren: () => import('./views/department-data/department-data.module').then(m => m.DepartmentDataModule )
      },
      {
        path: 'package-data',
        loadChildren: () => import('./views/package-data/package-data.module').then(m => m.PackageDataModule )
      },
      {
        path: 'users-data',
        loadChildren: () => import('./views/users-data/users-data.module').then(m => m.UsersDataModule )
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
