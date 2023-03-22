import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

// Import Containers
import { DefaultLayoutComponent } from './containers';
export const routes: Routes = [
  {
    path: '',
    component: DefaultLayoutComponent,
    data: {
      title: 'Home'
    },
    children: [
      {
        path: 'shift-data-maintain',
        loadChildren: () => import('./views/maintain/shift-data-maintain/shift-data-maintain.module').then(m => m.ShiftDataMaintainModule)
      },
      // {
      //   path: 'items',
      //   loadChildren: () => import('./items/items.module').then(m => m.ItemsModule)
      // }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
