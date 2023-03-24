import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddComponent } from '../../../views/maintain/department-data-maintain/add/add.component';
import { EditComponent } from '../../../views/maintain/department-data-maintain/edit/edit.component';
import { MainComponent } from '../../../views/maintain/department-data-maintain/main/main.component';

const routes: Routes = [
  {
    path: '',
    component: MainComponent,
  },
  {
    path: 'add',
    component: AddComponent,
  },
  {
    path: 'edit/:manuf/:parNo',
    component: EditComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class DepartmentDataMaintainRoutingModule {}
