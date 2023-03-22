import { DepartmentDataMaintainService } from './../../../../_core/services/department-data-maintain.service';
import { IconButton } from './../../../../_core/constants/common.constants';
import { Pagination } from './../../../../_core/utilities/pagination-utility';
import { MS_Department } from '../../../../_core/models/mS_Department_DTO';
import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss'],
})
export class MainComponent implements OnInit {
  param: MS_Department = {} as MS_Department;

  data: MS_Department[] = [];
  pagination: Pagination = {
    pageNumber: 1,
    pageSize: 5,
  } as Pagination;
  iconButton = IconButton;

  constructor(
    private service: DepartmentDataMaintainService,
    private route: Router
  ) {}

  ngOnInit(): void {
    this.getData();
  }

  getData() {
    this.service.getData(this.param, this.pagination).subscribe({
      next: (res) => {
        console.log(res);
        if (res) {
          this.data = res.result;
          this.pagination = res.pagination;
        }
      },
      error: () => {},
      complete: () => {},
    });
  }

  search() {
    this.pagination.pageNumber === 1
      ? this.getData()
      : (this.pagination.pageNumber = 1);
  }

  clear() {
    this.param = {} as MS_Department;
    this.getData();
  }

  addNew() {
    this.route.navigate(['maintain/department-data-maintenace/add']);
  }

  pageChanged(e: any) {
    this.pagination.pageNumber = e.page;
    this.getData();
  }

  update(item: MS_Department) {
    let msDepartmentEdit = { ...item };
    this.service.msDepartment.next(msDepartmentEdit);
    this.route.navigate(['maintain/department-data-maintenace/update']);
  }
}
