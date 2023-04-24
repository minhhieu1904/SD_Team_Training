import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppGuard } from '@guards/app.guard';
import { getInfoMenu } from '@utilities/function-utility';

const routes: Routes = [
  {
    path: 'wksh-sum-report',
    canLoad: [AppGuard],
    loadChildren: () =>
      import('./wksh-sum-report/wksh-sum-report.module').then(
        (m) => m.WkshSumReportModule
      ),
      data: {
        role: getInfoMenu('4.1')?.unique
    }
  },
  {
    path: 'sort-sum-report',
    canLoad: [AppGuard],
    loadChildren: () =>
      import('./sort-sum-report/sort-sum-report.module').then(
        (m) => m.SortSumReportModule
      ),
      data: {
        role: getInfoMenu('4.2')?.unique
    }
  },
  {
    path: 'storage-sum-report',
    canLoad: [AppGuard],
    loadChildren: () =>
      import('./storage-sum-report/storage-sum-report.module').then(
        (m) => m.StorageSumReportModule
      ),
      data: {
        role: getInfoMenu('4.3')?.unique
    }
  },
  {
    path: 'qrcode-wip-report',
    canLoad: [AppGuard],
    loadChildren: () =>
      import('./qrcode-wip-report/qrcode-wip-report.module').then(
        (m) => m.QrcodeWipReportModule
      ),
      data: {
        role: getInfoMenu('4.4')?.unique
    }
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ReportRoutingModule {}
