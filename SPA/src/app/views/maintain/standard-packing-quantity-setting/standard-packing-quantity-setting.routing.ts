import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PackageMainComponent } from './package-main/package-main.component';
import { PackageFormComponent } from './package-form/package-form.component';

const routes: Routes = [
  {
    path: '',
    component: PackageMainComponent,
  },
  {
    path: 'edit',
    component: PackageFormComponent,
  },
  {
    path: 'add',
    component: PackageFormComponent,
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class StandardPackingQuantitySettingRoutingModule {}
