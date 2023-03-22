import { IconButton } from './../../../../_core/constants/common.constants';
import { OperationResult } from './../../../../_core/utilities/operation-result';
import { NgSnotifyService } from './../../../../_core/services/ng-snotify.service';
import { routes } from './../../../../app.routing';
import { DepartmentDataMaintainService } from './../../../../_core/services/department-data-maintain.service';
import { MS_Department } from './../../../../_core/models/mS_Department_DTO';
import { Component, OnInit } from '@angular/core';
import { Route, Router } from '@angular/router';

@Component({
  selector: 'app-update',
  templateUrl: './update.component.html',
  styleUrls: ['./update.component.scss'],
})
export class UpdateComponent implements OnInit {
  model: MS_Department = <MS_Department>{};
  iconButton = IconButton;

  constructor(
    private DepartmentDataMaintainService: DepartmentDataMaintainService,
    private route: Router,
    private snotify: NgSnotifyService
  ) {}

  ngOnInit(): void {}

  loadData() {
    this.DepartmentDataMaintainService.msDepartmentCurrent.subscribe({
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
    this.route.navigate(['maintain/department-data-maintenace']);
  }

  saveChanged() {
    this.snotify.confirm('Bạn có muốn cập nhật không?', 'Cập nhật', () => {
      this.DepartmentDataMaintainService.update(this.model).subscribe({
        next: (res: OperationResult) => {
          if (res.isSuccess) {
            this.back();
            this.snotify.success('Update Success', 'Success');
          } else {
            this.snotify.error('Update Error', 'Error');
          }
        },
        error: () => {},
        complete: () => {},
      });
    });
  }
}
