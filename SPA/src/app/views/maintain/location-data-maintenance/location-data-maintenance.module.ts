import { PaginationModule } from 'ngx-bootstrap/pagination';
import { LocationDataMaintenanceRoutingModule } from './location-data-maintenance-routing.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MainComponent } from './main/main.component';
import { FormComponent } from './form/form.component';
import { FormsModule } from '@angular/forms';
import { UpdateComponent } from './update/update.component';

@NgModule({
  declarations: [MainComponent, FormComponent, UpdateComponent],
  imports: [
    CommonModule,
    LocationDataMaintenanceRoutingModule,
    FormsModule,
    PaginationModule.forRoot(),
  ],
})
export class LocationDataMaintenanceModule {}
