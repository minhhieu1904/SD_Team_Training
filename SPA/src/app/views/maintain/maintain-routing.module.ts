import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: "package-data-maintain",
    loadChildren: () => import ('./package-data-maintain/package-data-maintain.module').then( m => m.PackageDataMaintainModule)
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MaintainRoutingModule { }
