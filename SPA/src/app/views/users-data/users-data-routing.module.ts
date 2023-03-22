import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddUserDataComponent } from './add-user-data/add-user-data.component';
import { EditUserDataComponent } from './edit-user-data/edit-user-data.component';
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
    component: AddUserDataComponent,
    data: {
      title: 'Add'
    }
  },
  {
    path: 'edit',
    component: EditUserDataComponent,
    data: {
      title: 'Edit'
    }
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UsersDataRoutingModule { }
