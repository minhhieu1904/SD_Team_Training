import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { getInfoMenu } from '@utilities/function-utility';

const routes: Routes = [
  {
    path: 'wksh-sum-report',
    loadChildren: () =>
      import('../report/wksh-sum-report/wksh-sum-report.module').then(
        (m) => m.WkshSumReportModule
      ),
    data: {
      role: getInfoMenu('4.1')?.unique,
    },
  },
  {
    path: 'sort-sum-report',
    loadChildren: () =>
      import('../report/sort-sum-report/sort-sum-report.module').then(
        (m) => m.SortSumReportModule
      ),
    data: {
      role: getInfoMenu('4.2')?.unique,
    },
  },
  {
    path: 'storage-sum-report',
    loadChildren: () =>
      import('../report/storage-sum-report/storage-sum-report.module').then(
        (m) => m.StorageSumReportModule
      ),
    data: {
      role: getInfoMenu('4.3')?.unique,
    },
  },
  {
    path: 'qrcode-wip-report',
    loadChildren: () =>
      import('../report/qrcode-wip-report/qrcode-wip-report.module').then(
        (m) => m.QrcodeWipReportModule
      ),
    data: {
      role: getInfoMenu('4.4')?.unique,
    },
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ReportRoutingModule {}
