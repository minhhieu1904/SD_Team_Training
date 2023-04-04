import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [
  {
    path: 'shift-data-maintenance',
    loadChildren: () => import('./shift-data-maintenance/shift-data-maintenance.module').then(m => m.ShiftDataMaintenaceModule)
  },
  {
    path: 'warehouse-basic-data-maintenance',
    loadChildren: () => import('./warehouse-basic-data-maintenance/warehouse-basic-data-maintenance.module').then(m => m.WarehouseBasicDataMaintenanceModule)
  }
];
@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MaintainRoutingModule { }
