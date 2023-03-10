import { RouterModule, Routes } from '@angular/router';
import { NgModule } from "@angular/core";
import { MainComponent } from './main/main.component';

const routes: Routes = [
  {
    path: '',
    component: MainComponent,
    data: {
      title: ''
    }
  }
]
@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ShiftDataRoutingModule { }
