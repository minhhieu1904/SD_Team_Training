import { P500Component } from './views/error/500.component';
import { P404Component } from './views/error/404.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

// Import Containers
import { DefaultLayoutComponent } from './containers';
import { AuthGuard } from '@guards/auth.guard';
import { DashboardComponent } from './views/dashboard/dashboard.component'
import { LoginComponent } from './views/login/login.component';
export const routes: Routes = [
  {
    path: '',
    redirectTo: 'dashboard',
    pathMatch: 'full',
  },
  {
    path: '',
    canActivate: [AuthGuard],
    component: DefaultLayoutComponent,
    data: {
      title: 'Home'
    },
    children: [
      {
        path: 'dashboard',
        component: DashboardComponent
      },
      {
        path: 'maintain',
        loadChildren: () => import('./views/maintain/maintain.module').then(m => m.MaintainModule)
      },
      {
        path: 'transaction',
        loadChildren: () => import('./views/transaction/transaction.module').then(m => m.TransactionModule)
      },
      {
        path: 'report',
        loadChildren: () => import('./views/report/report.module').then(m => m.ReportModule)
      }
    ]
  },
  {
    path: '404',
    component: P404Component
  },
  {
    path: '500',
    component: P500Component
  },
  {
    path: 'login',
    component: LoginComponent
  },
  { path: '**', component: P404Component }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
