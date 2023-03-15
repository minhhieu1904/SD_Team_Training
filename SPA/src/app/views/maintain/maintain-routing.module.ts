import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    children: [
      {
        path: 'warehouse-basic-data-maintain',
        loadChildren: () => import ('./warehouse-basic-data-maintain/warehouse-basic-data-maintain.module').then(m => m.WarehouseBasicDataMaintainModule)
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MaintainRoutingModule { }
