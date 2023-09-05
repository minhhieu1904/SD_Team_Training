import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AuthorizationSettingRoutingModule } from './authorization-setting-routing.module';
import { MainComponent } from './main/main.component';
import { FormComponent } from './form/form.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { TranslateModule } from '@ngx-translate/core';
import { ModalModule } from 'ngx-bootstrap/modal';
import { NgSelectModule } from '@ng-select/ng-select';
import { ModalComponent } from './modal/modal.component';

@NgModule({
  declarations: [
    MainComponent,
    FormComponent,
    ModalComponent
  ],
  imports: [
    CommonModule,
    AuthorizationSettingRoutingModule,
    FormsModule,
    PaginationModule,
    TranslateModule,
    ModalModule.forRoot(),
    NgSelectModule,
    ReactiveFormsModule,
  ]
})
export class AuthorizationSettingModule { }
