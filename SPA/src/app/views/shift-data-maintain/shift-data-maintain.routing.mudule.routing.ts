import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AddComponent } from './add/add.component';
import { EditComponent } from './edit/edit.component';
import { MainComponent } from './main/main.component';

const routes: Routes = [
  {  
    path: '',
    component: MainComponent
  },
  {  
    path: 'add',
    component: AddComponent
  },
  {  
    // ở đây thêm các data cần truyền đi
    path: 'edit/:manuf/:shift',
    component: EditComponent
  },
];
@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ShiftDataMaintainRoutingModule {}