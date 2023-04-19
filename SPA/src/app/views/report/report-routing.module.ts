import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppGuard } from '@guards/app.guard';
import { getInfoMenu } from '@utilities/function-utility';

const routes: Routes = [
  {

    path: 'wksh-sum-report',
    canLoad: [AppGuard],

    loadChildren: () => import('./report-wksh-sum/report-wksh-sum.module')
      .then((m) => m.ReportWkshSumModule),
    data: {
        role: getInfoMenu('4.1')?.unique
    }
  },
  {
    path: 'sort-sum-report',
    canLoad: [AppGuard],

    loadChildren: () => import('./report-sort-sum/report-sort-sum.module')
      .then((m) => m.ReportSortSumModule),
    data: {
        role: getInfoMenu('4.2')?.unique
    }
  },  {
    path: 'storage-sum-report',
    canLoad: [AppGuard],

    loadChildren: () => import('./report-storage-sum/report-storage-sum.module')
      .then((m) => m.ReportStorageSumModule),
    data: {
        role: getInfoMenu('4.3')?.unique
    }
  },
  {
    path: 'qrcode-wip-report',
    canLoad: [AppGuard],

    loadChildren: () => import('./report-qrcode-wip/report-qrcode-wip.module')
      .then((m) => m.ReportQrcodeWipModule),
    data: {
        role: getInfoMenu('4.4')?.unique
    }
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ReportRoutingModule { }
