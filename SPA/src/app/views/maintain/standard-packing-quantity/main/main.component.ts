import { Component, Inject, OnInit } from '@angular/core';
import { InjectBase } from '@utilities/inject-base-app';
import { StandardPackingQuantityService } from '@services/maintain/standard-packing-quantity.service';
import { MsPackage } from '@models/maintain/msPackage';
import { Pagination } from '@utilities/pagination-utility';
import { StandardPackingQuantityParam } from '@models/maintain/standardPackingQuantityParam';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss'],
})
export class MainComponent extends InjectBase implements OnInit {
  msPackage: MsPackage[] = [];

  pagination: Pagination = <Pagination>{
    pageNumber: 1,
    pageSize: 10,
  };

  param: StandardPackingQuantityParam = <StandardPackingQuantityParam>{
    manuf: 'N',
    packageNo: '',
    packageQty: 0,
  };

  constructor(private service: StandardPackingQuantityService) {
    super();
  }

  ngOnInit(): void {
    this.getDataPagination();
  }

  add() {
    this.router.navigate(['maintain/standard-packing-quantity/add']);
  }

  edit(msPackage: MsPackage) {
    this.router.navigate([
      `maintain/standard-packing-quantity/edit/${msPackage.manuf}/${msPackage.packageNo}`,
    ]);
  }

  clear() {
    this.param.packageNo = '';
    this.param.packageQty = 0;
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
        this.msPackage = result.result;
        console.log(this.msPackage);
      },
    });
  }

  pageChanged(e: any) {
    this.pagination.pageNumber = e.page;
    this.getDataPagination();
  }
}
