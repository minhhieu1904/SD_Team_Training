import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddPackageDataComponent } from './add-package-data/add-package-data.component';
import { EditPackageDataComponent } from './edit-package-data/edit-package-data.component';
import { MainComponent } from './main/main.component';

const routes: Routes = [
  {
    path: '',
    component: MainComponent,
    data: {
      title: 'Main'
    }
  },
  {
    path: 'add',
    component: AddPackageDataComponent,
    data: {
      title: 'Add'
    }
  },
  {
    path: 'edit',
    component: EditPackageDataComponent,
    data: {
      title: 'Edit'
    }
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PackageDataRoutingModule { }
