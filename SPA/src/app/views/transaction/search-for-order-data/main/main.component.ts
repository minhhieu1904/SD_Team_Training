import { CompareQtyResult } from '@models/Transaction/SearchForOrderData/Search_For_Order_Data';
import { OperationResult } from '@utilities/operation-result';
import { DestroyService } from '@services/common/destroy.service';
import { IconButton } from '@constants/common.constants';
import { QrcodePrinterComponent } from './../../../qrcode-printer/qrcode-printer.component';
import { KeyValueUtility } from '@utilities/key-value-utility';
import { Pagination } from '@utilities/pagination-utility';
import { SearchForOrderDataViewModel, OrderDataPrint, SearchForOrderDataParam, OrderPrintResult } from '@models/Transaction/SearchForOrderData/Search_For_Order_Data';
import { InjectBase } from '@utilities/inject-base-app';
import { Component, OnInit, ViewChild } from '@angular/core';
import { takeUntil } from 'rxjs';
import { SearchForOrderDataService } from '@services/Transaction/SearchForOrderData/search-for-order-data.service';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss'],
  providers: [DestroyService]

})
export class MainComponent extends InjectBase implements OnInit {

  @ViewChild('qrcodeprinter') qrcodeprinter: QrcodePrinterComponent;
  data: SearchForOrderDataViewModel[] = [];
  orderDataPrint: OrderDataPrint = <OrderDataPrint>{
    packageQty: 5,
  };
  param: SearchForOrderDataParam = <SearchForOrderDataParam>{
    brandname: 'All',
    endcod: 'N',
    is_Remark: false,
  };
  orderPrintResult: OrderPrintResult[] = [];
  pagination: Pagination = <Pagination>{
    pageNumber: 1,
    pageSize: 100,
  };
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
  isPrtno: boolean = true;
  isCheckAll: boolean = true;
  isPackingQty: boolean = true;
  isBalance: boolean = true;
  isQtyOfPrint: boolean = true;

  constructor(
    private searchForOrderDataService: SearchForOrderDataService,
  ) {
    super();
    this.translateService.setDefaultLang('en');

    // the lang to use, if the lang isn't available, it will use the current loader to get them
   this.translateService.use('en');
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
    this.translateService.onLangChange
      .pipe(takeUntil(this.destroyService.destroys$))
      .subscribe((event) => {
        if (this.listBrandName[0])
          this.listBrandName[0].value =
            this.translateService.instant('System.SelectAll');

        if (this.listStatus[0])
          this.listStatus[0].value =
            this.translateService.instant('System.SelectAll');
      });
  }
  // get list Brandname data
  getListBrandName() {
    this.spinnerService.show();
    this.searchForOrderDataService.getListBrandname().subscribe({
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

  // get list Package data
  getListPackage() {
    this.spinnerService.show();
    this.searchForOrderDataService.getListPackage().subscribe({
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
      .getDataPagination(this.pagination, this.param)
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
  // search only current page = 1
  search() {
    this.isPrtno = !this.param.prtno;
    this.pagination.pageNumber == 1
      ? this.getData()
      : (this.pagination.pageNumber = 1);
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
    };
    this.listBrandName[0].value =
      this.translateService.instant('System.SelectAll');
    this.listStatus[0].value =
      this.translateService.instant('System.SelectAll');
    this.search();
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
      if (item.kind == '4' && item.remark != null) {
        this.data
          .filter((val) => val.isDisabled == false)
          .map((val) =>
            val == item
              ? (val.isChecked = !val.isChecked)
              : (val.isChecked = false)
          );
        this.data.filter((val) => val.isChecked == true).length > 0
          ? (this.orderDataPrint.printQty = item.wkshqty)
          : (this.orderDataPrint.printQty = 0);
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
    this.orderDataPrint = <OrderDataPrint>{
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
    this.orderDataPrint.printQty = event == 0 ? 1 : event;
  }
  // Print Data
  print() {
    this.listDataChecked = [...this.data.filter((item) => item.isChecked)];
    this.orderDataPrint.items = []; // data to print
    let error: boolean = false; // use check error
    if (
      this.listDataChecked.length == 1 &&
      this.listDataChecked[0].kind == '4' &&
      this.listDataChecked[0].remark != null
    ) {
      console.log("a");

      let dataChecked: SearchForOrderDataViewModel = this.listDataChecked[0];
      this.orderDataPrint.is_Remark = true;
      // Check empty value Empno
      if (!this.orderDataPrint.empno) {
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
      this.orderDataPrint.items.push({
        manno: dataChecked.manno,
        purno: dataChecked.purno,
        size: dataChecked.size,
        wkshno: dataChecked.wkshno,
        wkshqty: dataChecked.wkshqty,
        prtno: dataChecked.prtno,
        max_qty_can_print: dataChecked.available_quantity_for_Print,
      });
      console.log(this.orderDataPrint);
      if (!error) this.printOrder();
    } else {
      this.orderDataPrint.is_Remark = false;
      // Check data print length

      if (this.listDataChecked?.length < 1) {
        console.log("Check data print length");
        error = true;
        return this.snotifyService.error(
          this.translateService.instant(
            'Transaction.SearchForOrderData.SelectItems'
          ),
          this.translateService.instant('System.Caption.Error')
        );
      }
      // Check empty value Quantity of print
      if (!this.orderDataPrint.printQty || this.orderDataPrint.printQty < 1) {
        console.log("Check empty value Quantity of print");
        error = true;
        return this.snotifyService.error(
          this.translateService.instant(
            'Transaction.SearchForOrderData.EnterQuantity'
          ),
          this.translateService.instant('System.Caption.Error')
        );
      }
      // Check empty value Empno
      if (!this.orderDataPrint.empno) {
        console.log("Check empty value Empno");
        error = true;
        return this.snotifyService.error(
          'Please enter the empno',
          this.translateService.instant('System.Caption.Error')
        );
      }
      // If Balance == false, check Standard packing quantity vs Quantity of print
      if (
        !this.orderDataPrint.balance &&
        this.orderDataPrint.packageQty > this.orderDataPrint.printQty
      ) {
        console.log("If Balance == false, check Standard packing quantity vs Quantity of print");
        error = true;
        return this.snotifyService.error(
          'Please enter Quantity of print more than Standard packing quantity',
          this.translateService.instant('System.Caption.Error')
        );
      }
      // // Check endcod
      let endcods = this.listDataChecked.filter((item) => item.endcod === 'Y');
      if (endcods.length > 0) {
        console.log("Check endcod");
        error = true;
        return this.snotifyService.error(
          this.translateService.instant(
            'Transaction.SearchForOrderData.FinishedPrint',
            { purno: endcods[0]?.purno }
          ),
          this.translateService.instant('System.Caption.Error')
        );
      }
      // // Check difference of sum wkshqty - sum pqty vs Quantity of print in list data selected
      let sumWkshqty = this.listDataChecked
        .map((x) => x.wkshqty)
        .reduce((a, b) => a + b);
      let sumPqty = this.listDataChecked
        .map((x) => x.pqty)
        .reduce((a, b) => a + b);
      if (sumWkshqty - sumPqty < this.orderDataPrint.printQty) {
        console.log("Check difference ");
        error = true;
        return this.snotifyService.error(
          'Printed quantity over than the sum Wkshqty selected order',
          this.translateService.instant('System.Caption.Error')
        );
      }
      // Balance is checked
      if (this.orderDataPrint.balance) {
        this.listDataChecked.forEach((item) => {
          console.log("Balance is checked ");
          this.orderDataPrint.items.push({
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
            item.available_quantity_for_Print < this.orderDataPrint.packageQty
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
                this.orderDataPrint.packageQty
              ) * this.orderDataPrint.packageQty;
            this.orderDataPrint.items.push({
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
    this.searchForOrderDataService.orderPrint(this.orderDataPrint).subscribe({
      next: async (res: OperationResult) => {
        if (res.isSuccess) {
          this.orderPrintResult = res.data;
          // binding data print result
          await this.qrcodeprinter.print(this.orderPrintResult);
          if (res.data[0].remark != null) {
            this.data.map((val, ind) => {
              if (
                val.manno == res.data[0].manno &&
                val.purno == res.data[0].purno &&
                val.size == res.data[0].size &&
                val.wkshno == res.data[0].wkshno &&
                val.prtno == res.data[0].prtno
              ) {
                this.data.splice(ind, 1);
              }
            });
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
