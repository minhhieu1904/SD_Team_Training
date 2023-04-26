import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PackageDataRoutingModule } from './package-data-routing.module';
import { MainComponent } from './main/main.component';
import { AddPackageDataComponent } from './add-package-data/add-package-data.component';
import { EditPackageDataComponent } from './edit-package-data/edit-package-data.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { TranslateModule } from '@ngx-translate/core';


@NgModule({
  declarations: [
    MainComponent,
    AddPackageDataComponent,
    EditPackageDataComponent
  ],
  imports: [

    CommonModule,
    FormsModule,
    RouterModule,
    ReactiveFormsModule,
    PaginationModule.forRoot(),
    TranslateModule,

    PackageDataRoutingModule
  ]
})
export class PackageDataModule { }
