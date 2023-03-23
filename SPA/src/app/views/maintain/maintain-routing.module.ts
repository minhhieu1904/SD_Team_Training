import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: 'standard-parking-quantity-setting',
    loadChildren: () => import('./standard-packing-quantity-setting/standard-packing-quantity-setting.module').then(
      (m) => m.StandardPackingQuantitySettingModule
    ),
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MaintainRoutingModule { }
