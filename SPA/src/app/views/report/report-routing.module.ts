import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
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
