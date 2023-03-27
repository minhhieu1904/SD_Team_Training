import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AuthorizationSettingRoutingModule } from './authorization-setting-routing.module';
import { FormsModule } from '@angular/forms';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { NgSelectModule } from '@ng-select/ng-select';
import { ModalModule } from 'ngx-bootstrap/modal';
import { TranslateModule } from '@ngx-translate/core';
import { MainComponent } from './main/main.component';
import { FormComponent } from './form/form.component';
import { SettingRoleComponent } from './setting-role/setting-role.component';

@NgModule({
  declarations: [
    MainComponent,
    FormComponent,
    SettingRoleComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    NgSelectModule,
    PaginationModule.forRoot(),
    ModalModule.forRoot(),
    AuthorizationSettingRoutingModule,
    TranslateModule
  ]
})
export class AuthorizationSettingModule { }
