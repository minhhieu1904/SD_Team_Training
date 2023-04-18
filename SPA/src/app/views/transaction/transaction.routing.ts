import { Routes, RouterModule } from '@angular/router';
import { getInfoMenu } from './../../_core/utilities/function-utility';
import { NgModule } from '@angular/core';
import { AppGuard } from '@guards/app.guard';

const routes: Routes = [
  {

    path: 'search-for-order-data',
    canLoad: [AppGuard],
    loadChildren: () => import('./search-for-order-data/search-for-order-data.module').then(m => m.SearchForOrderDataModule),
    data: {
      role: getInfoMenu('2.1')?.unique,
    }
   },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})

export class TransactionRoutingModule { }
