import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UsersDataRoutingModule } from './users-data-routing.module';
import { MainComponent } from './main/main.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { AddUserDataComponent } from './add-user-data/add-user-data.component';
import { EditUserDataComponent } from './edit-user-data/edit-user-data.component';
import { NgSelectModule } from '@ng-select/ng-select';
import { ModalModule } from 'ngx-bootstrap/modal';


@NgModule({
  declarations: [
    MainComponent,
    AddUserDataComponent,
    EditUserDataComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    RouterModule,
    NgSelectModule,
    ReactiveFormsModule,
    PaginationModule.forRoot(),
    UsersDataRoutingModule,
    ModalModule.forRoot()
  ]
})
export class UsersDataModule { }
