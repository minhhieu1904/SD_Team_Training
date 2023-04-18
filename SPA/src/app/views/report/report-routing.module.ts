import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: 'wksh-sum-report',
    loadChildren: () => import('../report/wksh-sum-report/wksh-sum-report.module').then(m => m.WkshSumReportModule)
  },
  {
    path: 'sort-sum-report',
    loadChildren: () => import('../report/sort-sum-report/sort-sum-report.module').then(m => m.SortSumReportModule)
  },
  {
    path: 'storage-sum-report',
    loadChildren: () => import('../report/storage-sum-report/storage-sum-report.module').then(m => m.StorageSumReportModule)
  },
  {
    path: 'qrcode-wip-report',
    loadChildren: () => import('../report/qrcode-wip-report/qrcode-wip-report.module').then(m => m.QrcodeWipReportModule)
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ReportRoutingModule { }
