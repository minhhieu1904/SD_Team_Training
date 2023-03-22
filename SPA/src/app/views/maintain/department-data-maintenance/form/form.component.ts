import { IconButton } from './../../../../_core/constants/common.constants';
import { NgSnotifyService } from './../../../../_core/services/ng-snotify.service';
import { Router } from '@angular/router';
import { DepartmentDataMaintainService } from './../../../../_core/services/department-data-maintain.service';
import { MS_Department } from './../../../../_core/models/mS_Department_DTO';
import { Component, OnInit } from '@angular/core';
import { OperationResult } from '@utilities/operation-result';

@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.scss'],
})
export class FormComponent implements OnInit {
  model: MS_Department = {} as MS_Department;
  iconButton = IconButton;

  constructor(
    private service: DepartmentDataMaintainService,
    private route: Router,
    private snotify: NgSnotifyService
  ) {}

  ngOnInit(): void {}

  saveChange(type?: string) {
    this.snotify.confirm('Bạn có muốn thêm mới không?', 'Thêm mới', () => {
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
    this.route.navigate(['maintain/department-data-maintenace']);
  }
}
