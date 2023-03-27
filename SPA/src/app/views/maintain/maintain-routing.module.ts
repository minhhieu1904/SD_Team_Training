import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    children: [
      {
        path: 'shift-data-maintain',
        loadChildren: () => import('./shift-data-maintain/shift-data-maintain.module').then(m => m.ShiftDataMaintainModule)
      },
      {
        path: 'warehouse-basic-data-maintain',
        loadChildren: () => import('./warehouse-basic-data-maintain/warehouse-basic-data-maintain.module').then(m => m.WarehouseBasicDataMaintainModule)
      },
      {
        path: 'department-data-maintain',
        loadChildren: () => import('./department-data-maintain/department-data-maintain.module').then(m => m.DepartmentDataMaintainModule)
      },
      {
        path: "package-data-maintain",
        loadChildren: () => import('./package-data-maintain/package-data-maintain.module').then(m => m.PackageDataMaintainModule)
      }
    ]
  }

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MaintainRoutingModule { }
