import { Component, Inject, OnInit } from '@angular/core';
import { DepartmentDataParam } from '@models/maintain/departmentDataParam';
import { MsDepartment } from '@models/maintain/msDepartment';
import { DepartmentDataMaintainService } from '@services/maintain/department-data-maintain.service';
import { InjectBase } from '@utilities/inject-base-app';
import { Pagination } from '@utilities/pagination-utility';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss'],
})
export class MainComponent extends InjectBase implements OnInit {
  msDepartment: MsDepartment[] = [];

  pagination: Pagination = <Pagination>{
    pageNumber: 1,
    pageSize: 10,
  };

  param: DepartmentDataParam = <DepartmentDataParam>{
    parNo: '',
    parName: '',
  };
  constructor(private service: DepartmentDataMaintainService) {
    super();
  }

  ngOnInit() {
    this.getDataPagination();
  }

  add() {
    this.router.navigate(['maintain/department-data-maintain/add']);
  }

  edit(msDepartment: MsDepartment) {
    this.router.navigate([
      `maintain/department-data-maintain/edit/${msDepartment.manuf}/${msDepartment.parNo}`,
    ]);
  }

  clear() {
    this.param.parNo = '';
    this.param.parName = '';
    this.getDataPagination();
  }

  search() {
    this.pagination.pageNumber = 1;
    this.getDataPagination();
  }

  getDataPagination() {
    this.service.getData(this.pagination, this.param).subscribe({
      next: (result) => {
        this.pagination = result.pagination;
        this.msDepartment = result.result;
        console.log(this.msDepartment);
      },
    });
  }

  pageChanged(e: any) {
    this.pagination.pageNumber = e.page;
    this.getDataPagination();
  }
}
