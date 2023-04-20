import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RolesConstants_role } from '@constants/role.constants';
import { AppGuard } from '@guards/app.guard';
const routes: Routes = [
  {
    path: 'wksh-sum-report',
    canLoad: [AppGuard],
    loadChildren: () => import('../report/wksh-sum-report/wksh-sum-report.module').then(m => m.WkshSumReportModule),
    data: { 
      unique: RolesConstants_role.wkshSumReport
    }
  },
  {
    path: 'sort-sum-report',
    canLoad: [AppGuard],
    loadChildren: () => import('../report/sort-sum-report/sort-sum-report.module').then(m => m.SortSumReportModule),
    data: { 
      unique: RolesConstants_role.SortSumReport
    }
  },
  {
    path: 'storage-sum-report',
    canLoad: [AppGuard],
    loadChildren: () => import('../report/storage-sum-report/storage-sum-report.module').then(m => m.StorageSumReportModule),
    data: { 
      unique: RolesConstants_role.StorageSumReport
    }
  },
  {
    path: 'qrcode-wip-report',
    canLoad: [AppGuard],
    loadChildren: () => import('../report/qrcode-wip-report/qrcode-wip-report.module').then(m => m.QrcodeWipReportModule),
    data: { 
      unique: RolesConstants_role.QRCodeWIPReport
    }
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ReportRoutingModule { }
