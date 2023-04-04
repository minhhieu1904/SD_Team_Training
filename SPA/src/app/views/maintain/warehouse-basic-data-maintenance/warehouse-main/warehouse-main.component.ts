import { Component, OnInit } from '@angular/core';
import { MS_WareHouse } from '../../../../_core/models/maintain/mswarehouse';
import { IconButton } from '../../../../_core/constants/common.constants';
import { InjectBase } from '@utilities/inject-base-app';
import { WarehouseBasicDataMaintenanceService } from './../../../../_core/services/warehouse-basic-data-maintenance.service';
import { Pagination } from '@utilities/pagination-utility';
import { CaptionConstants, MessageConstants } from '@constants/message.enum';
import { tap } from 'rxjs';

@Component({
  selector: 'app-warehouse-main',
  templateUrl: './warehouse-main.component.html',
  styleUrls: ['./warehouse-main.component.scss'],
})
export class WarehouseMainComponent extends InjectBase implements OnInit {
  param: MS_WareHouse = <MS_WareHouse>{
    warehouseID: '',
    warehouseName: '',
  };
  pagination: Pagination = <Pagination>{
    pageNumber: 1,
    pageSize: 10,
  };

  data: MS_WareHouse[] = [];

  icon = IconButton;

  constructor(private service: WarehouseBasicDataMaintenanceService) {
    super();
  }

  ngOnInit() {
    this.getAll();
  }

  getAll() {
    this.spinnerService.show();
    this.service.getAll(this.pagination, this.param).subscribe({
      next: (result) => {
        this.spinnerService.hide();
        this.data = result.result;
        this.pagination = result.pagination;
      },
    });
  }

  pageChanged(e) {
    this.pagination.pageNumber = e.page;
    this.getAll();
  }

  search() {
    if (this.param.warehouseID === '') {
      this.spinnerService.show();
      this.pagination.pageNumber === 1;
      this.service
        .search(
          this.pagination.pageNumber,
          this.pagination.pageSize,
          this.param.warehouseName || this.param.warehouseID
        )
        .subscribe({
          next: (result) => {
            this.spinnerService.hide();
            this.data = result.result;
          },
        });
    } else {
      this.pagination.pageNumber === 1
        ? this.getAll()
        : (this.pagination.pageNumber = 1);
    }
    this.param.warehouseID = '';
    this.param.warehouseName = '';
  }

  clear() {
    this.param.warehouseID = '';
    this.param.warehouseName = '';
    this.getAll();
  }

  addNew() {
    this.router.navigate(['/maintain/warehouse-basic-data-maintenance/add']);
  }

  edit(model: MS_WareHouse) {
    this.param = {
      warehouse: model.warehouse,
      warehouseID: model.warehouseID,
      warehouseName: model.warehouseName,
    };
    this.service.dataSource.next(this.param);
    this.router.navigate(['/maintain/warehouse-basic-data-maintenance/edit']);
  }
}
