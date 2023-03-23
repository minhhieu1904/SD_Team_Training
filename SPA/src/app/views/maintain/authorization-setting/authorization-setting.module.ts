import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AuthorizationSettingRoutingModule } from './authorization-setting-routing.module';
import { MainComponent } from './main/main.component';
import { AddComponent } from './add/add.component';
import { EditComponent } from './edit/edit.component';
import { FormsModule } from '@angular/forms';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { NgSelectModule } from '@ng-select/ng-select';


@NgModule({
  declarations: [
    MainComponent,
    AddComponent,
    EditComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    NgSelectModule,
    PaginationModule.forRoot(),
    AuthorizationSettingRoutingModule
  ]
})
export class AuthorizationSettingModule { }
