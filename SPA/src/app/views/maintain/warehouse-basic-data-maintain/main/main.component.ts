import { Component, OnInit } from '@angular/core';
import { MsLocation } from '@models/msLocation';
import { WarehouseBasicDataParam } from '@models/warehouseBasicDataParam';
import { InjectBase } from '@utilities/inject-base-app';
import { Pagination } from '@utilities/pagination-utility';
import { WarehouseBasicDataService } from '@services/maintain/warehouse-basic-data.service';
import { url } from '@constants/url.constants';
import { CaptionConstants, MessageConstants } from '@constants/message.enum';
@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss'],
})
export class MainComponent extends InjectBase implements OnInit {
  msLocation: MsLocation[] = [];

  pagination: Pagination = <Pagination>{
    pageNumber: 1,
    pageSize: 10,
  };

  param: WarehouseBasicDataParam = <WarehouseBasicDataParam>{
    storeH: '',
    locationName: '',
  };
  constructor(private service: WarehouseBasicDataService) {
    super();
  }

  ngOnInit(): void {
    this.getDataPagination();
  }

  add() {
    this.router.navigate([`${url.maintain.warehouse_basic_data_maintain}/add`]);
  }

  edit(msLocation: MsLocation) {
    this.router.navigate([
      `${url.maintain.warehouse_basic_data_maintain}/edit/${msLocation.manuf}/${msLocation.storeH}`,
    ]);
  }

  search() {
    this.pagination.pageNumber = 1;
    this.getDataPagination();
  }

  clear() {
    this.param.storeH = '';
    this.param.locationName = '';
    this.search();
  }

  getDataPagination() {
    this.spinnerService.show();
    this.service.getData(this.pagination, this.param).subscribe({
      next: (result) => {
        this.spinnerService.hide();
        this.pagination = result.pagination;
        this.msLocation = result.result;
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
