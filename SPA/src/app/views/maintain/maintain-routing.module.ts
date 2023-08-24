import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: 'shift-data-maintenance',
    loadChildren: () => import('./1_1_shift-data-maintainance/shift-data-maintainance.module').then(m => m.ShiftDataMaintainanceModule)
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MaintainRoutingModule { }
