import { RouterModule, Routes } from '@angular/router';
import { NgModule } from "@angular/core";
import { MainComponent } from './main/main.component';
import { AddShiftDataComponent } from './add-shift-data/add-shift-data.component';
import { EditShiftDataComponent } from './edit-shift-data/edit-shift-data.component';

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
      title: 'Add'
    }
  },
  {
    path: 'edit',
    component: EditShiftDataComponent,
    data: {
      title: 'Edit'
    }
  }
]
@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ShiftDataRoutingModule { }
