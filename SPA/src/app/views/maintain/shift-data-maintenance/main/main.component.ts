import { Component, OnInit } from '@angular/core';
import { Route, Router } from '@angular/router';
import { MS_Shift, ShiftDataMaintainParam } from '@models/maintain/msshift';
import { Pagination } from '@utilities/pagination-utility';
import { IconButton } from '../../../../_core/constants/common.constants';
import { ShiftDataMaintenanceService } from '../../../../_core/services/shift-data-maintenance.service';
import { InjectBase } from '../../../../_core/utilities/inject-base-app';
import { CaptionConstants, MessageConstants } from '../../../../_core/constants/message.enum'
// import { MS_Shift } from '@models/maintain/shift-data-maintenance';
@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss'],
})
export class MainComponent extends InjectBase implements OnInit {
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
  ) {super()}

  ngOnInit() {
    this.getData();
  }

  getData() {
    this.spinnerService.show();
    this.service.getAll(this.pagination, this.param).subscribe({
      next: (res) => {
        this.data = res.result;
        this.pagination = res.pagination;
        this.spinnerService.hide();
      },
      error: () => {
        this.snotifyService.error(MessageConstants.UN_KNOWN_ERROR, CaptionConstants.ERROR);
        this.spinnerService.hide();
      },
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
    this.pagination.pageNumber === 1 ? this.getData() : this.pagination.pageNumber = 1;
  }

  pageChanged(e) {
    this.pagination.pageNumber = e.page;
    this.getData();
  }
}
