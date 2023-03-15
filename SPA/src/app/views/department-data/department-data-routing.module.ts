import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddDepartmentDataComponent } from './add-department-data/add-department-data.component';
import { EditDepartmentDataComponent } from './edit-department-data/edit-department-data.component';
import { MainComponent } from './main/main.component';

const routes: Routes = [
  {
    path: '',
    component: MainComponent,
    data: {
      title: 'Main'
    }
  },
  {
    path: 'add',
    component: AddDepartmentDataComponent,
    data: {
      title: 'Add'
    }
  },
  {
    path: 'edit',
    component: EditDepartmentDataComponent,
    data: {
      title: 'Edit'
    }
  }
]

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DepartmentDataRoutingModule { }
