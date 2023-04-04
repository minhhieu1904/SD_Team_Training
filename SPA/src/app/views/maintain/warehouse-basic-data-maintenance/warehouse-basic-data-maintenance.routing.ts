import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { WarehouseMainComponent } from '../warehouse-basic-data-maintenance/warehouse-main/warehouse-main.component';
import { WarehouseFormComponent } from '../warehouse-basic-data-maintenance/warehouse-form/warehouse-form.component';

const routes: Routes = [
  {
    path: '',
    component: WarehouseMainComponent
  },
  {
    path: 'warehouse-main',
    component: WarehouseMainComponent
  },
  {
    path: 'add',
    component: WarehouseFormComponent
  },
  {
    path: 'edit',
    component: WarehouseFormComponent
  },
];
@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class WarehouseBasicDataMaintenanceRoutingModule { }
