import { Component, OnInit } from '@angular/core';
import { InjectBase } from '@utilities/inject-base-app';
import { StandardPackingQuantityService } from '@services/maintain/standard-packing-quantity.service';
import { MsPackage } from '@models/maintain/msPackage';
import { Pagination } from '@utilities/pagination-utility';
import { StandardPackingQuantityParam } from '@models/maintain/standardPackingQuantityParam';
import { url } from '@constants/url.constants';
import { CaptionConstants, MessageConstants } from '@constants/message.enum';
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
    this.router.navigate([`${url.maintain.standard_packing_quantity}/add`]);
  }

  edit(msPackage: MsPackage) {
    this.router.navigate([
      `${url.maintain.standard_packing_quantity}/edit/${msPackage.manuf}/${msPackage.packageNo}`,
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
    this.spinnerService.show();
    this.service.getData(this.pagination, this.param).subscribe({
      next: (result) => {
        this.spinnerService.hide();
        this.pagination = result.pagination;
        this.msPackage = result.result;
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
