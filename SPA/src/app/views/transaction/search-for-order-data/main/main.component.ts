import { DestroyService } from './../../../../_core/services/common/destroy.service';
import { Component, OnInit, ViewChild } from '@angular/core';
import { IconButton } from '@constants/sd-team.utility';
import { SearchForOrderDataParam } from '@helpers/transaction/searchForOrderDataParam';
import {
  CompareQtyResult,
  OrderDataPrint,
  OrderPrintResult,
  SearchForOrderDataViewModel,
} from '@models/transaction/search-for-order-data';

import { CommonService } from '@services/common/common.service';

import { SearchForOrderDataService } from '@services/transaction/search-for-order-data.service';
import { InjectBase } from '@utilities/inject-base-app';
import { KeyValueUtility } from '@utilities/key-value-utility';
import { OperationResult } from '@utilities/operation-result';
import { Pagination } from '@utilities/pagination-utility';
import { takeUntil } from 'rxjs';
import { QrcodePrinterComponent } from 'src/app/views/commons/qrcode-printer/qrcode-printer.component';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss'],
  providers: [DestroyService],
})
export class MainComponent extends InjectBase implements OnInit {
  @ViewChild('qrcodeprinter') qrcodeprinter: QrcodePrinterComponent;
  data: SearchForOrderDataViewModel[] = [];
  dataPrint: OrderDataPrint = <OrderDataPrint>{
    packageQty: 5,
  };
  param: SearchForOrderDataParam = <SearchForOrderDataParam>{
    brandname: 'All',
    tolcls: 'All',
    endcod: 'N',
    is_Remark: false,
  };
  orderPrintResult: OrderPrintResult[] = [];
  pagination: Pagination = <Pagination>{
    pageNumber: 1,
    pageSize: 10,
  };
  listTolcls: KeyValueUtility[] = [];
  listBrandName: KeyValueUtility[] = [];
  listPackage: KeyValueUtility[] = [];
  listStatus: KeyValueUtility[] = [
    { key: 'N', value: 'N' },
    { key: 'Y', value: 'Y' },
    { key: 'C', value: 'C' },
  ];
  listDataChecked: SearchForOrderDataViewModel[] = [];

  checkAll: boolean = false;
  iconButton = IconButton;
  isCheckAll: boolean = true;
  isPackingQty: boolean = true;
  isBalance: boolean = true;
  isQtyOfPrint: boolean = true;

  constructor(
    private searchForOrderDataService: SearchForOrderDataService,
    private commonService: CommonService
  ) {
    super();
  }

  ngOnInit(): void {
    this.listStatus.unshift({
      key: 'All',
      value: this.translateService.get('System.SelectAll'),
    });
    this.listStatus[0].value =
      this.translateService.instant('System.SelectAll');
    this.getListBrandName();
    this.getListPackage();
    this.getData();
    this.getListTolcls();
    this.translateService.onLangChange
      .pipe(takeUntil(this.destroyService.destroys$))
      .subscribe((event) => {
        if (this.listBrandName[0])
          this.listBrandName[0].value =
            this.translateService.instant('System.SelectAll');

        if (this.listStatus[0])
          this.listStatus[0].value =
            this.translateService.instant('System.SelectAll');

        if (this.listTolcls[0])
          this.listTolcls[0].value =
            this.translateService.instant('System.SelectAll')
      });
  }
  // get list Brandname data
  getListBrandName() {
    this.spinnerService.show();
    this.commonService.getListBrandName().subscribe({
      next: (res) => {
        this.listBrandName = res;
        this.listBrandName.unshift({
          key: 'All',
          value: this.translateService.get('System.SelectAll'),
        });
        this.listBrandName[0].value =
          this.translateService.instant('System.SelectAll');
        this.spinnerService.hide();
      },
      error: () => {
        this.snotifyService.error(
          this.translateService.instant('System.Message.UnknowError'),
          this.translateService.instant('System.Caption.Error')
        );
        this.spinnerService.hide();
      },
    });
  }

  // get List Tolcls data
  getListTolcls() {
    this.spinnerService.show();
    this.commonService.getListTolcls().subscribe({
      next: (res) => {
        this.listTolcls = res;
        this.listTolcls.unshift({
          key: 'All',
          value: this.translateService.get('System.SelectAll'),
        });
        this.listTolcls[0].value =
          this.translateService.instant('System.SelectAll');
        this.spinnerService.hide();
      },
      error: () => {
        this.snotifyService.error(
          this.translateService.instant('System.Message.UnknowError'),
          this.translateService.instant('System.Caption.Error')
        );
        this.spinnerService.hide();
      },
    });
  }

  // get list Package data
  getListPackage() {
    this.spinnerService.show();
    this.searchForOrderDataService.GetListPackage().subscribe({
      next: (res) => {
        let check = res.find((x) => x.key === 5);
        if (!check) {
          res.push({ key: 5, value: 5 });
          res.sort((a, b) => a.key - b.key);
        }
        this.listPackage = res;
        this.spinnerService.hide();
      },
      error: () => {
        this.snotifyService.error(
          this.translateService.instant('System.Message.UnknowError'),
          this.translateService.instant('System.Caption.Error')
        );
        this.spinnerService.hide();
      },
    });
  }
  // get data pagination
  getData() {
    this.clearPrint();
    this.spinnerService.show();
    this.searchForOrderDataService
      .getData(this.pagination, this.param)
      .subscribe({
        next: (res) => {
          this.pagination = res.pagination;
          this.data = res.result;
          this.checkAll = false;
          this.param.is_Remark ? this.setUIvalue(false) : this.setUIvalue(true);
          // set x, y scroll position of table
          let table = document.getElementById('table-order-main');
          table.scrollLeft = 0;
          table.scrollTop = 0;

          this.spinnerService.hide();
        },
        error: () => {
          this.snotifyService.error(
            this.translateService.instant('System.Message.UnknowError'),
            this.translateService.instant('System.Caption.Error')
          );
          this.spinnerService.hide();
        },
      });
  }
  setUIvalue(value: boolean) {
    this.isPackingQty = value;
    this.isCheckAll = value;
    this.isBalance = value;
    this.isQtyOfPrint = value;
  }

  checkSearch() {
    let checkTolcls = true;
    if (this.param.tolcls == 'All') {
      checkTolcls = false;
      this.snotifyService.error(this.translateService.instant(
        'Transaction.SearchForOrderData.PleaseChooseTolcls'
      ), this.translateService.instant('System.Caption.Error'))
    }
    return checkTolcls;
  }

  // search only current page = 1
  search() {
    if (this.checkSearch()) {
      this.pagination.pageNumber == 1 ? this.getData() : (this.pagination.pageNumber = 1);
    }
  }
  // clear search params and get data pagination
  clearSearch() {
    this.param = <SearchForOrderDataParam>{
      bitnbr: '',
      brandname: 'All',
      cusid: '',
      cusna: '',
      endcod: 'N',
      manno: '',
      prtno: '',
      rmodel: '',
      size: '',
      tolcls: 'All'
    };
    this.isCheckAll = false;
    this.data.length = 0;
    this.listDataChecked.length = 0
    this.listBrandName[0].value =
      this.translateService.instant('System.SelectAll');
    this.listStatus[0].value =
      this.translateService.instant('System.SelectAll');
    this.listTolcls[0].value = this.translateService.instant('System.SelectAll');
  }
  // change current page
  pageChanged(event: any) {
    this.pagination.pageNumber = event.page;
    this.getData();
  }
  // Checkbox All function
  changCheckAll() {
    this.data
      .filter((item) => !item.isDisabled)
      .forEach((item) => (item.isChecked = this.checkAll));
  }

  // Checked item
  changeChecked(item: SearchForOrderDataViewModel) {
    if (!item.isDisabled) {
      if (item.remark != null) {
        this.data
          .filter((val) => val.isDisabled == false)
          .map((val) =>
            val == item
              ? (val.isChecked = !val.isChecked)
              : (val.isChecked = false)
          );
        this.data.filter((val) => val.isChecked == true).length > 0
          ? (this.dataPrint.printQty = item.wkshqty)
          : (this.dataPrint.printQty = 0);
      } else {
        item.isChecked = !item.isChecked;
        this.checkAll = this.data
          .filter((item) => !item.isDisabled)
          .every((x) => x.isChecked);
      }
    }
  }
  // Clear print param
  clearPrint() {
    this.dataPrint = <OrderDataPrint>{
      balance: false,
      items: [],
      packageQty: 5,
      printQty: null,
      empno: '',
      isB_grade: false,
    };
    this.listDataChecked = [];
  }
  onCheckValuePrint(event: any) {
    this.dataPrint.printQty = event == 0 ? 1 : event;
  }

  // Print Data
  print() {
    this.listDataChecked = [...this.data.filter((item) => item.isChecked)];
    this.dataPrint.items = []; // data to print
    let error: boolean = false; // use check error
    if (
      this.listDataChecked.length == 1 &&
      this.listDataChecked[0].remark != null
    ) {
      let dataChecked: SearchForOrderDataViewModel = this.listDataChecked[0];
      this.dataPrint.is_Remark = true;
      // Check empty value Empno
      if (!this.dataPrint.empno) {
        error = true;
        return this.snotifyService.error(
          'Please enter the empno',
          this.translateService.instant('System.Caption.Error')
        );
      }
      // Check endcod
      if (dataChecked.endcod === 'Y') {
        error = true;
        return this.snotifyService.error(
          this.translateService.instant(
            'Transaction.SearchForOrderData.FinishedPrint'
          ),
          this.translateService.instant('System.Caption.Error')
        );
      }
      this.dataPrint.items.push({
        manno: dataChecked.manno,
        purno: dataChecked.purno,
        size: dataChecked.size,
        wkshno: dataChecked.wkshno,
        wkshqty: dataChecked.wkshqty,
        prtno: dataChecked.prtno,
        max_qty_can_print: dataChecked.available_quantity_for_Print,
      });
      console.log(this.dataPrint);
      if (!error) this.printOrder();
    } else {
      this.dataPrint.is_Remark = false;
      // Check data print length
      if (this.listDataChecked?.length < 1) {
        error = true;
        return this.snotifyService.error(
          this.translateService.instant(
            'Transaction.SearchForOrderData.SelectItems'
          ),
          this.translateService.instant('System.Caption.Error')
        );
      }
      // Check empty value Quantity of print
      if (!this.dataPrint.printQty || this.dataPrint.printQty < 1) {
        error = true;
        return this.snotifyService.error(
          this.translateService.instant(
            'Transaction.SearchForOrderData.EnterQuantity'
          ),
          this.translateService.instant('System.Caption.Error')
        );
      }
      // Check empty value Empno
      if (!this.dataPrint.empno) {
        error = true;
        return this.snotifyService.error(
          'Please enter the empno',
          this.translateService.instant('System.Caption.Error')
        );
      }
      // If Balance == false, check Standard packing quantity vs Quantity of print
      if (
        !this.dataPrint.balance &&
        this.dataPrint.packageQty > this.dataPrint.printQty
      ) {
        error = true;
        return this.snotifyService.error(
          'Please enter Quantity of print more than Standard packing quantity',
          this.translateService.instant('System.Caption.Error')
        );
      }
      // Check endcod
      let endcods = this.listDataChecked.filter((item) => item.endcod === 'Y');
      if (endcods.length > 0) {
        error = true;
        return this.snotifyService.error(
          this.translateService.instant(
            'Transaction.SearchForOrderData.FinishedPrint',
            { purno: endcods[0]?.purno }
          ),
          this.translateService.instant('System.Caption.Error')
        );
      }
      // Check difference of sum wkshqty - sum pqty vs Quantity of print in list data selected
      let sumWkshqty = this.listDataChecked.map((x) => x.wkshqty).reduce((a, b) => a + b);
      let sumPqty = this.listDataChecked.map((x) => x.pqty).reduce((a, b) => a + b);

      if (sumWkshqty - sumPqty < this.dataPrint.printQty) {
        error = true;
        return this.snotifyService.error('Printed quantity over than the sum Wkshqty selected order',
          this.translateService.instant('System.Caption.Error')
        );
      }

      // Balance is checked
      if (this.dataPrint.balance) {
        this.listDataChecked.forEach((item) => {
          this.dataPrint.items.push({
            manno: item.manno,
            purno: item.purno,
            size: item.size,
            wkshno: item.wkshno,
            wkshqty: item.wkshqty,
            prtno: item.prtno,
            max_qty_can_print: item.available_quantity_for_Print,
          });
        });
      }
      // Balance is NOT checked
      else {
        let order = this.listDataChecked.filter(
          (item) =>
            item.available_quantity_for_Print < this.dataPrint.packageQty
        );
        if (order.length > 0) {
          error = true;
          return this.snotifyService.error(
            this.translateService.instant(
              'Transaction.SearchForOrderData.RemainingQuantity',
              { purno: order[0]?.purno }
            ),
            this.translateService.instant('System.Caption.Error')
          );
        } else {
          this.listDataChecked.forEach((item) => {
            let max_qty_can_print =
              Math.floor(
                item.available_quantity_for_Print /
                this.dataPrint.packageQty
              ) * this.dataPrint.packageQty;
            this.dataPrint.items.push({
              manno: item.manno,
              purno: item.purno,
              size: item.size,
              wkshno: item.wkshno,
              wkshqty: item.wkshqty,
              prtno: item.prtno,
              max_qty_can_print: max_qty_can_print,
            });
          });
        }
      }
      if (!error) this.printOrder();
    }
  }
  printOrder() {
    this.spinnerService.show();
    // hidden overflow body
    document.body.style.overflow = 'hidden';
    // remove hidden of list data print
    document.getElementById('printData').hidden = false;
    this.searchForOrderDataService.print(this.dataPrint).subscribe({
      next: async (res: OperationResult) => {
        if (res.isSuccess) {
          this.orderPrintResult = res.data;
          // binding data print result
          await this.qrcodeprinter.print(this.orderPrintResult);
          if (res.data[0].remark != null) {
            this.data.map(
              (val, ind) => {
                if (val.manno == res.data[0].manno &&
                  val.purno == res.data[0].purno &&
                  val.size == res.data[0].size &&
                  val.wkshno == res.data[0].wkshno &&
                  val.prtno == res.data[0].prtno) {
                  this.data.splice(ind, 1)
                }
              }
            );
          } else {
            this.listDataChecked.forEach((element) => {
              let data = this.orderPrintResult.filter(
                (x) =>
                  x.manno == element.manno &&
                  x.purno == element.purno &&
                  x.size == element.size &&
                  x.wkshno == element.wkshno &&
                  x.prtno == element.prtno
              );
              if (data.length > 0) {
                element.pqty += Object.values(data).reduce(
                  (t, { qty }) => t + qty,
                  0
                );
                element.available_quantity_for_Print =
                  element.wkshqty - element.pqty;
                element.updated_by = data[0].update_by;
                element.update_time = data[0].update_time;
              }
              element.isChecked = false;
            });
          }
          this.checkAll = false;
          this.clearPrint();
        } else {
          // hidden list data print
          document.getElementById('printData').hidden = true;
          let value = res.data as CompareQtyResult;
          this.snotifyService.error(
            this.translateService.instant(
              'Transaction.SearchForOrderData.QuantityExceeded',
              { qty: value.qty, pqty: value.pqty, printQty: value.printQty }
            ),
            this.translateService.instant('System.Caption.Error')
          );
        }
        // remove overflow hidden of body
        document.body.style.overflow = 'auto';
        this.spinnerService.hide();
      },
      error: () => {
        // remove overflow hidden of body
        document.body.style.overflow = 'auto';
        // hidden list data print
        document.getElementById('printData').hidden = true;
        this.snotifyService.error(
          this.translateService.instant('System.Message.UnknowError'),
          this.translateService.instant('System.Caption.Error')
        );
        this.spinnerService.hide();
      },
    });
  }
}
