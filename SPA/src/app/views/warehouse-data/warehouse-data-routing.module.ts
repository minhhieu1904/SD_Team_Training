import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddWarehouseDataComponent } from './add-warehouse-data/add-warehouse-data.component';
import { EditWarehouseDataComponent } from './edit-warehouse-data/edit-warehouse-data.component';
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
    component: AddWarehouseDataComponent,
    data: {
      title: 'Add'
    }
  },
  {
    path: 'edit',
    component: EditWarehouseDataComponent,
    data: {
      title: 'Edit'
    }
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class WarehouseDataRoutingModule { }
