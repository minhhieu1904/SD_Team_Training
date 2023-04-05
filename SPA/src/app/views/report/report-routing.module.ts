import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { getInfoMenu } from '@utilities/function-utility';

const routes: Routes = [
  {
    path: 'wksh-sum-report',
    loadChildren: () => import('./report-wksh-sum/report-wksh-sum.module')
      .then((m) => m.ReportWkshSumModule),
    data: {
        role: getInfoMenu('4.1')?.unique
    }
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ReportRoutingModule { }
