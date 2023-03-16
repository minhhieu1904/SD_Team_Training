import { OperationResult } from './../../../../_core/utilities/operation-result';
import { NgSnotifyService } from './../../../../_core/services/ng-snotify.service';
import { SnotifyService } from 'ng-snotify';
import { Router } from '@angular/router';
import { LocationDataMaintenanceService } from '@services/warehouse-basic-data-maintenance.service';
import { MS_Location } from './../../../../_core/models/mS_Location_DTO';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-update',
  templateUrl: './update.component.html',
  styleUrls: ['./update.component.scss'],
})
export class UpdateComponent implements OnInit {
  model: MS_Location = <MS_Location>{};

  constructor(
    private locationDataMainService: LocationDataMaintenanceService,
    private route: Router,
    private snotify: NgSnotifyService
  ) {}

  ngOnInit(): void {}

  loadData() {
    this.locationDataMainService.msLocationCurrent.subscribe({
      next: (res) => {
        if (!res) {
          this.back();
        }
        this.model = res;
      },
      error: () => {},
      complete: () => {},
    });
  }

  back() {
    this.route.navigate(['maintain/location-data-maintenance']);
  }

  saveChange() {
    this.snotify.confirm('Bạn có muốn cập nhật không?', 'Cập nhật', () => {
      this.locationDataMainService.update(this.model).subscribe({
        next: (res: OperationResult) => {
          if (res.isSuccess) {
            this.back();
            this.snotify.success('Update success', 'Success');
          } else {
            this.snotify.error('Update fail', 'Error');
          }
        },
        error: () => {},
        complete: () => {},
      });
    });
  }
}
