import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { FormComponent } from './form/form.component';
import { MainComponent } from './main/main.component';

const routes: Routes = [
  {  
    path: '',
    component: MainComponent
  },
  {  
    path: 'add',
    component: FormComponent
  },
  {  
    path: 'edit',
    component: FormComponent
  },
];
@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ShiftDataMaintainRoutingModule {}
