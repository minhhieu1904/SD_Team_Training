import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [{
  path: '',
  children: [
    {
      path: 'department-data-maintain',
      loadChildren: () => import('./department-data-maintain/department-data-maintain.module').then(m => m.DepartmentDataMaintainModule)
    }
  ]
 }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MaintainRoutingModule { }
