import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { getInfoMenu } from '@utilities/function-utility';

const routes: Routes = [
  {
    path: 'authorization-setting',
    loadChildren: () =>
      import(
        './authorization-setting/authorization-setting.module'
      ).then((m) => m.AuthorizationSettingModule),
      data: {
        role: getInfoMenu('1.5')?.unique
    }
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MaintainRoutingModule { }
