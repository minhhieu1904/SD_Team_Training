import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: 'search-for-order-data',
    // canLoad: [AppGuard],
    loadChildren: () => import('../transaction/search-for-order-data/search-for-order-data.module').then(m => m.SearchForOrderDataModule),
    // data: { 
    //   unique: RolesConstants_role.wkshSumReport
    // }
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TransactionRoutingModule { }
