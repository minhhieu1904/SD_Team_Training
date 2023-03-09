import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: 'shift-data-maintenance',
    loadChildren:()=> import('./shift-data-maintenance/shift-data-maintenance.module').then (m=> m.ShiftDataMaintenanceModule)
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MaintainRoutingModule { }
