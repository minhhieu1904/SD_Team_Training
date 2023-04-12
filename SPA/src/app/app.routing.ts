import { AuthGuard } from './_core/guards/auth.guard';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './views/login/login.component';
// Import Containers
import { DefaultLayoutComponent } from './containers';
import { DashboardComponent } from './views/dashboard/dashboard.component';
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
      {
        path: 'maintain',
        loadChildren: () => import('./views/maintain/maintain.module')
          .then(m => m.MaintainModule),
      },
      {
        path: 'report',
        loadChildren: () => import('./views/report/report.module').then(m => m.ReportModule)
      }

    ]
  },
  {
    path: 'login',
    component: LoginComponent,
    data: {
      title: 'Login Page'
    }
  },
];

@NgModule({
  imports: [ RouterModule.forRoot(routes, { relativeLinkResolution: 'legacy' }) ],
  exports: [ RouterModule ]
})
export class AppRoutingModule {}
