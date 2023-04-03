import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { combineLatest } from 'rxjs';

const routes: Routes = [
  {
    path: 'authorization-setting',
    loadChildren:() => import('./authorization-setting/authorization-setting.module').then(m => m.AuthorizationSettingModule)
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MaintainRoutingModule { }
