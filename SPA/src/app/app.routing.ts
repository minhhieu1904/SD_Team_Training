import { NgModule } from '@angular/core';
import { AuthGuard } from '../app/_core/guards/auth/auth.guard';
import { Routes, RouterModule } from '@angular/router';

// Import Containers
import { DefaultLayoutComponent } from './containers';
import { LoginComponent } from './views/login/login/login.component';
export const routes: Routes = [
  {
    path: '',
    canActivate: [AuthGuard],
    component: DefaultLayoutComponent,
    data: {
      title: 'Home'
    },
    children: [
      {
        path: 'maintain',
        loadChildren: () => import('./views/maintain/maintain.module').then(m => m.MaintainModule)

      },
      {
        path: 'report',
        loadChildren: () => import('./views/report/report.module').then(m => m.ReportModule)

      },
      {
        path: 'transaction',
        loadChildren: () => import('./views/transaction/transaction.module').then(m => m.TransactionModule)

      }
    ]
  },
  {
    path: 'login',
    component: LoginComponent,
    data: {
      title: 'Login Page'
    }
  },{
    path: 'default',
    component: DefaultLayoutComponent,
    data: {
      title: 'Default Page'
    }
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
