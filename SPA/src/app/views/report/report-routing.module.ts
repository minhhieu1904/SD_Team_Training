import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: 'wksh-sum-report',
    loadChildren: () =>
      import(
        '../report/search-for-packing-scan/search-for-packing-scan.module'
      ).then((m) => m.SearchForPackingScanModule),
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ReportRoutingModule {}
