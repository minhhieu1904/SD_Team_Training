import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: 'shift-data-maintenance',
    loadChildren: () =>
      import('./shift-data-maintenance/shift-data-maintenance.module').then(
        (m) => m.ShiftDataMaintenanceModule
      ),
  },
  {
    path: 'location-data-maintenance',
    loadChildren: () =>
      import(
        './location-data-maintenance/location-data-maintenance.module'
      ).then((m) => m.LocationDataMaintenanceModule),
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class MaintainRoutingModule {}
