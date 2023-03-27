import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: 'ksh-sum-report',
    loadChildren: () => import('./report-wksh-sum/report-wksh-sum.module').then(m => m.ReportWkshSumModule)
  },
  {
    path: 'sort-sum-report',
    loadChildren: () => import('./report-sort-sum/report-sort-sum.module').then(m => m.ReportSortSumModule)
  },
  {
    path: 'storage-sum-report',
    loadChildren: () => import('./report-storage-sum/report-storage-sum.module').then(m => m.ReportStorageSumModule)
  },
  {
    path: 'qrcode-wip-report',
    loadChildren: () => import('./report-qrcode-wip/report-qrcode-wip.module').then(m => m.ReportQrcodeWipModule)
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ReportRoutingModule { }
