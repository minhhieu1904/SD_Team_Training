import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: 'department-data-maintenace',
    loadChildren: () =>
      import(
        './department-data-maintenance/department-data-maintenance.module'
      ).then((m) => m.DepartmentDataMaintenanceModule),
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class MaintainRoutingModule {}
