import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { DepartmentMainComponent } from './department-main/department-main.component'
import { DepartmentFormComponent } from './department-form/department-form.component';

const routes: Routes = [
  {
    path: '',
    component: DepartmentMainComponent
   },
   {
    path: 'edit',
    component: DepartmentFormComponent
   },
   {
    path: 'add',
    component: DepartmentFormComponent
   },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})

export class DepartmentDataMaintenanceRoutingModule {}
