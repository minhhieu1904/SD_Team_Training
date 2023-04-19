import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
const routes: Routes = [
  {
    path: 'search-for-order-data',
    loadChildren: () =>
      import(
        '../transaction/search-for-order-data/search-for-order-data.module'
      ).then((m) => m.SearchForOrderDataModule),
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class TransactionRoutingModule {}
