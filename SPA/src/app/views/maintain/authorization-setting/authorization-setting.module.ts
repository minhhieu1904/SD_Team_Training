import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AuthorizationSettingRoutingModule } from './authorization-setting-routing.module';
import { MainComponent } from './main/main.component';
import { FormsModule } from '@angular/forms';
import { NgSelectModule } from '@ng-select/ng-select';
import { ModalModule } from 'ngx-bootstrap/modal';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { EditComponent } from './edit/edit.component';
import { AddComponent } from './add/add.component';


@NgModule({
  declarations: [
    MainComponent,
    EditComponent,
    AddComponent
  ],
  imports: [
    CommonModule,
    AuthorizationSettingRoutingModule,
    FormsModule,
    PaginationModule.forRoot(),
    NgSelectModule,
    ModalModule.forRoot(),
  ]
})
export class AuthorizationSettingModule { }
