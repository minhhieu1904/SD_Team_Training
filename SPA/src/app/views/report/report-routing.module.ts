import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: 'wksh-sum-report',
    loadChildren: () => import('./wksh-sum-report/wksh-sum-report.module').then(m => m.WkshSumReportModule)
  },
  {
    path: 'sort-sum-report',
    loadChildren: () => import('./sort-sum-report/sort-sum-report.module').then(m => m.SortSumReportModule)
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ReportRoutingModule { }
