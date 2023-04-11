import { Component, OnInit } from '@angular/core';
import { MS_Package } from '@models/maintain/mspackage';
import { InjectBase } from '@utilities/inject-base-app';
import { IconButton } from './../../../../_core/constants/common.constants';
import { StandardPackingQuantitySettingService } from './../../../../_core/services/standard-packing-quantity-setting.service'
import { Pagination } from '@utilities/pagination-utility';

@Component({
  selector: 'app-package-main',
  templateUrl: './package-main.component.html',
  styleUrls: ['./package-main.component.scss'],
})
export class PackageMainComponent extends InjectBase implements OnInit {
  icon = IconButton;
  data: MS_Package[] = [];

  param: MS_Package = <MS_Package>{
    packageNo: '',
    packageQty: 0,
  };
  pagination = <Pagination>{
    pageNumber: 1,
    pageSize: 10
  }
  constructor(private service: StandardPackingQuantitySettingService) {
    super();
  }

  ngOnInit() {
    this.getAll();
  }
  getAll() {
    this.spinnerService.show();
    this.service.getAll(this.pagination, this.param).subscribe({
      next: result => {
        this.spinnerService.hide();
        console.log(result);
        this.data = result.result;
        this.pagination = result.pagination;
      }
    })
  }
  pageChanged(e) {
    this.pagination.pageNumber = e.page;
    this.getAll();
  }
  addNew() {
    this.router.navigate(['/maintain/standard-packing-quantity-setting/add'])
  }
  edit(model: MS_Package) {
    this.param = {
      manuf: model.manuf,
      packageNo: model.packageNo,
      packageQty: model.packageQty
    }
    this.service.dataSource.next(this.param);
    this.router.navigate(['/maintain/standard-packing-quantity-setting/edit'])
  }
  clear() {
    this.param.packageNo = '';
    this.param.packageQty = 0;
    this.getAll();
  }
  search() {
    if (this.param.packageQty == 0) {
      this.spinnerService.show();
      this.pagination.pageNumber === 1;
      this.service.search(this.pagination.pageNumber, this.pagination.pageSize, this.param.packageNo).subscribe({
        next: result => {
          this.spinnerService.hide();
          this.data = result.result;
        }
      })
    }
    else {
      this.pagination.pageNumber === 1
        ? this.getAll()
        : (this.pagination.pageNumber = 1);
    }

    this.param.packageNo = '';
    this.param.packageQty = 0;
  }
}
