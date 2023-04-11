import { NgSelectModule } from '@ng-select/ng-select';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ModalModule } from 'ngx-bootstrap/modal';
import { AuthorizationSettingRoutingModule } from './authorization-setting-routing.module';
import { MainComponent } from './main/main.component';
import { FormsModule } from '@angular/forms';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { FormComponent } from './form/form.component';
import { UpdateComponent } from './update/update.component';


@NgModule({
  declarations: [
    MainComponent,
    FormComponent,
    UpdateComponent
  ],
  imports: [
    CommonModule,
    AuthorizationSettingRoutingModule,
    FormsModule,
    PaginationModule.forRoot(),
    NgSelectModule,
    ModalModule.forRoot()
  ]
})
export class AuthorizationSettingModule { }
