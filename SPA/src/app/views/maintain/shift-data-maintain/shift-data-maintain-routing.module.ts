import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddComponent } from './add/add.component';
import { EditComponent } from './edit/edit.component';
import { MainComponent } from './main/main.component';
import { getInfoMenu } from '@utilities/function-utility';

const routes: Routes = [
  {
    path: '',
    component: MainComponent,
    // data: {
    //   role: getInfoMenu('1.1')?.unique,
    // },
  },
  {
    path: 'add',
    component: AddComponent,
  },
  {
    path: 'edit/:manuf/:shift',
    component: EditComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ShiftDataMaintainRoutingModule {}
