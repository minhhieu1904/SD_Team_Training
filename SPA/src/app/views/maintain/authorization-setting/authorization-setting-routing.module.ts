import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MainComponent } from '../authorization-setting/main/main.component';
import { AddComponent } from '../authorization-setting/add/add.component';
import { EditComponent } from '../authorization-setting/edit/edit.component';
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
    path: 'edit/:account',
    component: EditComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class AuthorizationSettingRoutingModule {}
