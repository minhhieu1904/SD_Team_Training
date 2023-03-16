import { OperationResult } from './../../../../_core/utilities/operation-result';
import { NgSnotifyService } from './../../../../_core/services/ng-snotify.service';
import { Router } from '@angular/router';
import { MS_Location } from './../../../../_core/models/mS_Location_DTO';
import { Component, OnInit } from '@angular/core';
import { LocationDataMaintenanceService } from '@services/warehouse-basic-data-maintenance.service';

@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.scss'],
})
export class FormComponent implements OnInit {
  model: MS_Location = {} as MS_Location;

  constructor(
    private service: LocationDataMaintenanceService,
    private route: Router,
    private snotify: NgSnotifyService
  ) {}

  ngOnInit(): void {}

  saveChange() {
    this.snotify.confirm('Ban co muon them moi khong?', 'Them moi', () => {
      this.service.addNew(this.model).subscribe({
        next: (res: OperationResult) => {
          if (res.isSuccess) {
            this.back();
            this.snotify.success('Add success', 'Success');
          } else {
            this.snotify.error('Add error', 'Error');
          }
        },
        error: () => {
          this.snotify.error('Add error', 'Error');
        },
        complete: () => {},
      });
    });
  }

  back() {
    this.route.navigate(['maintain/location-data-maintenance']);
  }
}
