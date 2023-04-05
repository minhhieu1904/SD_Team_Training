import { Component, OnInit } from '@angular/core';
import { CaptionConstants, MessageConstants } from '@constants/message.enum';
import { url } from '@constants/url.constants';
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
    this.router.navigate([`${url.maintain.department_data_maintain}/add`]);
  }

  edit(msDepartment: MsDepartment) {
    this.router.navigate([
      `${url.maintain.department_data_maintain}/edit/${msDepartment.manuf}/${msDepartment.parNo}`,
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
    this.spinnerService.show();
    this.service.getData(this.pagination, this.param).subscribe({
      next: (result) => {
        this.spinnerService.hide();
        this.pagination = result.pagination;
        this.msDepartment = result.result;
      },
      error: () => {
        this.spinnerService.hide();
        this.snotifyService.error(
          MessageConstants.SYSTEM_ERROR_MSG,
          CaptionConstants.ERROR
        );
      },
    });
  }

  pageChanged(e: any) {
    this.pagination.pageNumber = e.page;
    this.getDataPagination();
  }
}
