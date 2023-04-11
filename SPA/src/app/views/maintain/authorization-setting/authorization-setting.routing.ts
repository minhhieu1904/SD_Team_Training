import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { UserMainComponent } from './user-main/user-main.component';
import { UserFormComponent } from './user-form/user-form.component';

const routes: Routes = [
  {
    path: '',
    component: UserMainComponent
  },
  {
    path: 'edit',
    component: UserFormComponent
  },
  {
    path: 'add',
    component: UserFormComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})

export class AuthorizationSettingRoutingModule { }
