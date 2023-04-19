import { getInfoMenu } from '@utilities/function-utility';
import { AppGuard } from './../../_core/guards/app.guard';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [ {
  path: 'search-for-order-data',
  canLoad: [AppGuard],
  loadChildren: () => import('./search-for-order-data/search-for-order-data.module')
    .then((m) => m.SearchForOrderDataModule),
  data: {
      role: getInfoMenu('2.1')?.unique
  }
},];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TransactionRoutingModule { }
