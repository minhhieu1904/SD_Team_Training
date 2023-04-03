import { FormComponent } from './form/form.component';
import { MainComponent } from './main/main.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UpdateComponent } from './update/update.component';

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
    component: FormComponent,
    data: {
      title: 'Add'
    }
  },
  {
    path: 'update',
    component: UpdateComponent,
    data: {
      title: 'Update'
    }
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AuthorizationSettingRoutingModule { }
