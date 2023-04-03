import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MainComponent } from '../shift-data-maintenance/main/main.component';
import { FormComponent } from '../shift-data-maintenance/form/form.component'
const routes: Routes = [
  {
    path: '',
    component: MainComponent
  },
  {
    path: 'home',
    component: MainComponent
  },
  {
    path: 'add',
    component: FormComponent
  },
  {
    path: 'edit',
    component: FormComponent
  },
];
@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ShiftDataMaintenaceRoutingModule { }
