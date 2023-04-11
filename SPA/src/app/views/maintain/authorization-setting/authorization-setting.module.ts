import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthorizationSettingRoutingModule } from './authorization-setting.routing';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { UserFormComponent } from './user-form/user-form.component'
import { UserMainComponent } from './user-main/user-main.component'
import { NgSelectModule } from '@ng-select/ng-select';
import { ModalModule } from 'ngx-bootstrap/modal';

@NgModule({
  imports: [
    CommonModule,
    AuthorizationSettingRoutingModule,
    PaginationModule.forRoot(),
    FormsModule,
    NgSelectModule,
    ModalModule.forRoot(),
    ReactiveFormsModule,
  ],
  declarations: [
    UserFormComponent,
    UserMainComponent
  ]
})
export class AuthorizationSettingModule { }
