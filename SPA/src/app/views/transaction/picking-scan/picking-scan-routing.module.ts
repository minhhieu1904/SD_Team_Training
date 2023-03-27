import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FormComponent } from './form/form.component';
import { MainComponent } from './main/main.component';
import { PickingComponent } from './picking/picking.component';

const routes: Routes = [
  {
    path: '',
    component: MainComponent,
    data: {
      title: 'Picking Scan'
    }
  },
  {
    path: 'add/:storeH/:parNo',
    component: FormComponent,
    data: {
      title: 'Add'
    }
  },
  {
    path: 'edit',
    component: FormComponent,
    data: {
      title: 'Edit'
    }
  },
  {
    path: 'picking',
    component: PickingComponent,
    data: {
      title: 'Picking'
    }
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PickingScanRoutingModule { }
