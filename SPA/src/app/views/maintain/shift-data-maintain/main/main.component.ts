import { Component, OnInit } from '@angular/core';
import { MsShift, ShiftDataMaintainParam } from '@models/maintain/msShift';
import { Pagination } from '@utilities/pagination-utility';
import { ShiftDataMaintainService } from '@services/maintain/shift-data-maintain.service';
import { InjectBase } from '@utilities/inject-base-app';
import { url } from '@constants/url.constants';
import { CaptionConstants, MessageConstants } from '@constants/message.enum';
import { IconButton } from '@constants/common.constants';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss'],
})
export class MainComponent extends InjectBase implements OnInit {
  //#region attribute
  iconButton = IconButton;
  msShift: MsShift[] = [];
  pagination: Pagination = <Pagination>{
    pageNumber: 1,
    pageSize: 10,
  };

  param: ShiftDataMaintainParam = <ShiftDataMaintainParam>{
    shift: '',
    shift_Name: '',
  };
  //#endregion

  constructor(private service: ShiftDataMaintainService) {
    super();
  }

  ngOnInit(): void {
    this.getDataPagination();
  }

  //#region function
  add() {
    this.router.navigate([`${url.maintain.shift_data_maitain}/add`]);
  }

  edit(msShift: MsShift) {
    this.router.navigate([
      `${url.maintain.shift_data_maitain}/edit/${msShift.manuf}/${msShift.shift}`,
    ]);
  }

  clear() {
    this.param.shift = '';
    this.param.shift_Name = '';
    this.search();
  }

  search() {
    this.pagination.pageNumber === 1
      ? this.getDataPagination()
      : (this.pagination.pageNumber = 1);
  }

  getDataPagination() {
    this.spinnerService.show();
    this.service.getData(this.pagination, this.param).subscribe({
      next: (result) => {
        this.spinnerService.hide();
        this.pagination = result.pagination;
        this.msShift = result.result;
      },
      error: () => {
        this.spinnerService.hide();
        this.snotifyService.error(
          this.translateService.instant('System.Message.SystemError'),
          this.translateService.instant('System.Caption.Error')
        );
      },
    });
  }

  pageChanged(e: any) {
    this.pagination.pageNumber = e.page;
    this.getDataPagination();
  }
  //#endregion
}
