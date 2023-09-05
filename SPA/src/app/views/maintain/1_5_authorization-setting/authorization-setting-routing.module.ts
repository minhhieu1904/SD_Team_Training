import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MainComponent } from './main/main.component';
import { FormComponent } from './form/form.component';

const routes: Routes = [
  {
    path: '',
    component: MainComponent,
    data: {}
  },
  {
    path: 'add',
    component: FormComponent,
    data: {}
  },
  {
    path: 'edit',
    component: FormComponent,
    data: {}
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AuthorizationSettingRoutingModule { }
