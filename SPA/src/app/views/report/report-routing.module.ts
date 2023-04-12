import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: 'search-for-order-data',
    loadChildren: () => import('./wksh-sum-report/wksh-sum-report.module').then(m => m.WkshSumReportModule)
  },
  {
    path: 'search-for-cycle-packing-data',
    loadChildren: () => import('./sort-sum-report/sort-sum-report.module').then(m => m.SortSumReportModule)
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ReportRoutingModule { }
