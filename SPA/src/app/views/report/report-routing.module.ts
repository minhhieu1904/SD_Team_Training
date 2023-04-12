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
  },
  {
    path: 'storage-sum-report',
    loadChildren: () => import('./storage-sum-report/storage-sum-report.module')
    .then(m => m.StorageSumReportModule)
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ReportRoutingModule { }
