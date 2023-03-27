import { OrderDataStatusAdjust, OrderDataStatusAdjustUpdate } from './../../../../_core/models/transaction/order-data-status-adjust';
import { Component, OnInit } from '@angular/core';
import { CaptionConstants, MessageConstants } from '@constants/message.enum';
import { IconButton } from '@constants/sd-team.utility';
import { OrderDataStatusAdjustParam } from '@models/transaction/order-data-status-adjust';
import { CommonService } from '@services/common/common.service';
import { DestroyService } from '@services/common/destroy.service';
import { OrderDataStatusAdjustService } from '@services/transaction/order-data-status-adjust.service';
import { InjectBase } from '@utilities/inject-base-app';
import { KeyValueUtility } from '@utilities/key-value-utility';
import { Pagination } from '@utilities/pagination-utility';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrl: './main.component.scss',
  providers: [DestroyService]
})
export class MainComponent extends InjectBase implements OnInit {
  iconButton = IconButton;
  isCheckAll: boolean = false;
  isHasItemsChecked = false;

  param: OrderDataStatusAdjustParam = <OrderDataStatusAdjustParam>{
    planningNo: '',
    purno: '',
    tolcls: ''
  }

  paramSS: OrderDataStatusAdjustParam = <OrderDataStatusAdjustParam>{
    planningNo: '',
    purno: '',
    tolcls: ''
  }

  listTolcls: KeyValueUtility[] = [];
  orderDataStatusAdjustAll: OrderDataStatusAdjust[] = [];
  orderDataStatusAdjusts: OrderDataStatusAdjust[] = [];
  orderDataStatusAdjustChoosed: OrderDataStatusAdjust[] = [];

  pagination: Pagination = <Pagination>{
    pageNumber: 1,
    pageSize: 10
  }

  constructor(
    private service: OrderDataStatusAdjustService,
    private commonService: CommonService
  ) { super() }

  ngOnInit(): void {
    this.getListTolcls();
  }

  getListTolcls() {
    this.spinnerService.show();
    this.commonService.getListTolcls().subscribe({
      next: res => {
        this.listTolcls = res;
      },
      error: () => {
        this.snotifyService.error(MessageConstants.UN_KNOWN_ERROR, CaptionConstants.ERROR)
      }
    }).add(() => this.spinnerService.hide());
  }

  getData() {
    this.spinnerService.show();
    this.service.getData(this.pagination, this.param).subscribe({
      next: res => {
        this.pagination = res.pagination.pagination;
        this.orderDataStatusAdjustAll = res.allData;

        if (this.functionUtility.checkEmpty(this.paramSS.planningNo)
          && this.functionUtility.checkEmpty(this.paramSS.purno)
          && this.functionUtility.checkEmpty(this.paramSS.tolcls)) {
          this.paramSS = { ...this.param }
          this.orderDataStatusAdjustChoosed = res.allData;
          this.orderDataStatusAdjustChoosed.forEach(order => {
            order.isChecked = false;
            order.isDisabledReOpen = ((order.orderQuantity == order.printedQuantity) && order.status == 'Y' ? true : false);
          });
        }
        else {
          if (this.param.planningNo != this.paramSS.planningNo || this.param.purno != this.paramSS.purno || this.param.tolcls != this.paramSS.tolcls) {
            this.paramSS = { ...this.param }
            this.orderDataStatusAdjustChoosed = res.allData;
            this.orderDataStatusAdjustChoosed.forEach(order => {
              order.isChecked = false;
              order.isDisabledReOpen = ((order.orderQuantity == order.printedQuantity) && order.status == 'Y') ? true : false;
            })
          }
        }
        this.orderDataStatusAdjusts = res.pagination.result;

        this.orderDataStatusAdjusts.forEach(order => {
          let ord = this.orderDataStatusAdjustChoosed.find(item => order.planningNo == item.planningNo && order.purchaseNo == item.purchaseNo
            && order.size == item.size && order.wkshNo == item.wkshNo && order.prtno == item.prtno);
          if (ord && ord.isChecked) order.isChecked = true;
          else order.isChecked = false;

          order.isDisabledReOpen = ((order.orderQuantity == order.printedQuantity) && order.status == 'Y') ? true : false
        });
        this.checkStatus();
      },
      error: (err) => {
        this.snotifyService.error(MessageConstants.UN_KNOWN_ERROR, CaptionConstants.ERROR)
      },
    }).add(() => this.spinnerService.hide())
  }

  checkStatus() {
    this.isCheckAll = this.orderDataStatusAdjustChoosed.every(order => order.isChecked || (!order.isChecked && order.isDisabledReOpen));
    this.isHasItemsChecked = this.orderDataStatusAdjustChoosed.some(order => order.isChecked)
  }

  validation() {
    if (this.functionUtility.checkEmpty(this.param.planningNo) || this.functionUtility.checkEmpty(this.param.tolcls)) {
      this.snotifyService.error('Please enter required', CaptionConstants.ERROR);
      return false;
    }
    return true;
  }

  updateCancelOrReOpen(isCancel: boolean) {
    let model = <OrderDataStatusAdjustUpdate>{
      endCod: isCancel ? 'Y' : 'N',
      orderDataStatusAdjusts: this.orderDataStatusAdjustChoosed.filter(x => x.isChecked)
    }

    this.spinnerService.show();
    this.service.updateCancelOrReOpen(model).subscribe({
      next: res => {
        this.spinnerService.hide();
        if (res.isSuccess) {
          this.snotifyService.success(MessageConstants.UPDATED_ERROR_MSG, CaptionConstants.SUCCESS)

          this.orderDataStatusAdjustChoosed.forEach(x => x.isChecked = false);

          this.getData()
        } else
          this.snotifyService.error(MessageConstants.UPDATED_ERROR_MSG, CaptionConstants.ERROR)
      }
    })
  }

  pageChanged(event: any) {
    this.pagination.pageNumber = event.page;
    this.getData();
  }

  isNullOrEmpty = (value: string) => this.functionUtility.checkEmpty(value);

  onSearch() {
    this.orderDataStatusAdjusts = [];
    if (this.validation()) {
      this.pagination.pageNumber == 1 ? this.getData() : this.pagination.pageNumber = 1;
      this.getData();
    }
  }

  onCancelOrReOpen(isCancel: boolean) {
    if (this.orderDataStatusAdjustChoosed.filter(x => x.isChecked).length == 0)
      return this.snotifyService.warning('Please choose items', CaptionConstants.WARNING);

    this, this.updateCancelOrReOpen(isCancel);
  }

  onChooseItem(item: OrderDataStatusAdjust) {
    this.orderDataStatusAdjusts.forEach(x => {
      if (x.planningNo == item.planningNo &&
        x.purchaseNo == item.purchaseNo &&
        x.size == item.size &&
        x.wkshNo == item.wkshNo &&
        x.prtno == item.prtno) {
        if ((x.orderQuantity == x.printedQuantity) && x.status == 'Y')
          x.isChecked = false;
        else x.isChecked = !x.isChecked;
      }
    })

    this.checkStatus();
  }


  onChooseAll() {
    this.orderDataStatusAdjusts.forEach(x => {
      if ((x.orderQuantity == x.printedQuantity) && x.status == 'Y')
        x.isChecked = false;
      else x.isChecked = this.isCheckAll;
    });

    this.orderDataStatusAdjustChoosed.forEach(x => {
      if ((x.orderQuantity == x.printedQuantity) && x.status == 'Y')
        x.isChecked = false;
      else x.isChecked = this.isCheckAll;
    });

    this.isHasItemsChecked = this.orderDataStatusAdjustChoosed.some(x => x.isChecked);
  }
}
