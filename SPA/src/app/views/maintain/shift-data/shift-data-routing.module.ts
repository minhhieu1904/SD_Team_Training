import { RouterModule, Routes } from '@angular/router';
import { NgModule } from "@angular/core";

import { AddShiftDataComponent } from './add-shift-data/add-shift-data.component';
import { EditShiftDataComponent } from './edit-shift-data/edit-shift-data.component';
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
    component: AddShiftDataComponent,
    data: {
      title: '1.1 Shift Data Maintenance'
    }
  },
  {
    path: 'edit',
    component: EditShiftDataComponent,
    data: {
      title: '1.1 Shift Data Maintenance'
    }
  }
]
@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ShiftDataRoutingModule { }
