import { MS_Package } from './../../../../_core/models/mS_Package_DTO';
import { StandardPackingQuantitySettingService } from './../../../../_core/services/standard-packing-quantity-setting.service';
import { IconButton } from './../../../../_core/constants/common.constants';
import { Router } from '@angular/router';
import { Pagination } from './../../../../_core/utilities/pagination-utility';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss'],
})
export class MainComponent implements OnInit {
  param: MS_Package = <MS_Package>{
    packageQty:0
  };

  data: MS_Package[] = [];
  pagination: Pagination = {
    pageNumber: 1,
    pageSize: 10,
  } as Pagination;
  iconButton = IconButton;

  constructor(
    private service: StandardPackingQuantitySettingService,
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
    //this.service.getData()
  }

  search() {
    this.pagination.pageNumber === 1
      ? this.getData()
      : (this.pagination.pageNumber = 1);
  }

  addNew() {
    this.route.navigate(['maintain/standard-parking-quantity-setting/add']);
  }

  update(item: MS_Package) {
    let msPackageEdit = { ...item };
    this.service.msPackage.next(msPackageEdit);
    this.route.navigate(['maintain/standard-parking-quantity-setting/update']);
  }

  clear() {
    this.param = {} as MS_Package;
    this.param.packageQty = 0;
    this.getData();
  }

  //này là bấm chuyển trang, mặc đinh 1 trang có row, khi bấm chuyển sẽ qua trang 2 3 4 tùy theo mình chọn
  pageChanged(e: any) {
    this.pagination.pageNumber = e.page;
    this.getData();
  }
}
