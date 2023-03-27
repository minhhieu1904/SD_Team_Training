import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MainComponent } from './main/main.component';
import { FormComponent } from './form/form.component';
import { SettingRoleComponent } from './setting-role/setting-role.component';


const routes: Routes = [
  {
    path: '',
    component: MainComponent,
    data: {
      title: '1.5 Authorization Setting'
    }
  },
  {
    path: 'add',
    component: FormComponent,
    data: {
      title: '1.5 Add User'
    }
  },
  {
    path: 'edit',
    component: FormComponent,
    data: {
      title: '1.5 Edit User'
    }
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AuthorizationSettingRoutingModule { }
