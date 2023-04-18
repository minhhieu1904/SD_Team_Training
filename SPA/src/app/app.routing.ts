import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from './_core/guards/auth.guard';
import { DashboardComponent } from './views/dashboard/dashboard.component';
import { LoginComponent } from './views/login/login.component';

// Import Containers
import { DefaultLayoutComponent } from './containers';
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
      title: 'Home',
    },
    children: [
      {
        path: 'dashboard',
        component: DashboardComponent,
      },
      // Maintain
      {
        path: 'maintain',
        loadChildren: () =>
          import('./views/maintain/maintain.module').then(
            (m) => m.MaintainModule
          ),
      },
      // Transaction....
      {
        path: 'transaction',
        loadChildren: () =>
          import('./views/transaction/transaction.module').then(
            (m) => m.TransactionModule
          ),
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
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { relativeLinkResolution: 'legacy' })],
  exports: [RouterModule],
})
export class AppRoutingModule {}
