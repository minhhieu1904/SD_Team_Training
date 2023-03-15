import { MS_Shift, MS_ShiftParam } from './_core/_models/shift_data/shift_Data';
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
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
