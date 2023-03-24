import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { MainComponent} from "../warehouse-basic-data/main/main.component";
import { AddComponent} from "../warehouse-basic-data/add/add.component";
import { EditComponent} from "../warehouse-basic-data/edit/edit.component";

const routes: Routes = [{
  path: '',
  component: MainComponent
},
{
  path: 'add',
  component: AddComponent
},  {
  path: 'edit/:manuf/:storeH',
  component: EditComponent
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class WarehouseBasicDataRoutingModule { }
