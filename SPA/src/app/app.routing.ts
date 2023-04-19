import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './views/login/login/login.component';
// Import Containers
import { DefaultLayoutComponent } from './containers';
export const routes: Routes = [
  {
    path: '',
    component: DefaultLayoutComponent,
    data: {
      title: 'Home',
    },
    children: [
      {
        path: 'maintain',
        loadChildren: () =>
          import('./views/maintain/maintain.module').then(
            (m) => m.MaintainModule
          ),
      },
      {
        path: 'transaction',
        loadChildren: () =>
          import('./views/transaction/transaction.module').then(
            (m) => m.TransactionModule
          ),
      },
      {
        path: 'report',
        loadChildren: () =>
          import('./views/report/report.module').then((m) => m.ReportModule),
      },
    ],
  },
  {
    path: 'login',
    component: LoginComponent,
    data: {
      title: 'Login Page',
    },
  },
  {
    path: 'default',
    component: DefaultLayoutComponent,
    data: {
      title: 'Trang Chủ',
    },
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
