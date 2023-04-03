import { Component, OnInit } from '@angular/core';
import { Route, Router } from '@angular/router';
import { MS_Shift, ShiftDataMaintainParam } from '@models/maintain/msshift';
import { Pagination } from '@utilities/pagination-utility';
import { IconButton } from '../../../../_core/constants/common.constants';
import { ShiftDataMaintenanceService } from '../../../../_core/services/shift-data-maintenance.service';
// import { MS_Shift } from '@models/maintain/shift-data-maintenance';
@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss'],
})
export class MainComponent implements OnInit {
  param: ShiftDataMaintainParam = <ShiftDataMaintainParam>{
    shift: '',
    shiftName: '',
  };

  pagination = <Pagination>{
    pageNumber: 1,
    pageSize: 10,
  };

  data: MS_Shift[] = [];
  icon = IconButton;

  constructor(
    private service: ShiftDataMaintenanceService,
    private router: Router
  ) {}

  ngOnInit() {
    this.getData();
  }

  getData() {
    this.service.getAll(this.pagination, this.param).subscribe({
      next: (res) => {
        this.data = res.result;
        this.pagination = res.pagination;
      },
      error: () => {},
    });
  }

  addNew() {
    this.router.navigate(['maintain/shift-data-maintenance/add']);
  }

  clear() {
    this.param.shift = '';
    this.param.shiftName = '';
    this.getData();
  }

  edit(model: MS_Shift) {
    this.param = {
      shift: model.shift,
      shiftName: model.shiftName,
    };
    this.service.dataSource.next(this.param);
    this.router.navigate(['maintain/shift-data-maintenance/edit']);
  }

  search() {
    this.getData();
  }

  pageChanged(e) {
    this.pagination.pageNumber = e.page;
    this.getData();
  }
}
