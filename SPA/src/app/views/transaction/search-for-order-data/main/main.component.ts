import { Component, Directive, OnInit, ViewChild } from '@angular/core';
import { SearchForOrderDataService } from '@services/transaction/search-for-order-data.service';
import { InjectBase } from '@utilities/inject-base-app';
import {
  OrderDataPrint,
  OrderPrintResult,
  SearchForOrderDataDTO,
} from '@models/transaction/searchForOrderDataDTO';
import { Pagination } from '@utilities/pagination-utility';
import { SearchForOrderDataParam } from '@models/transaction/searchForOrderDataParam';
import { BsDatepickerConfig } from 'ngx-bootstrap/datepicker';
import { IconButton } from '@constants/common.constants';
import { CaptionConstants, MessageConstants } from '@constants/message.enum';
import { QrcodePrinterComponent } from '../../../commons/qrcode-printer/qrcode-printer.component';
import { KeyValueUtility } from '@utilities/key-value-utility';
import { OperationResult } from '@utilities/operation-result';


@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss'],
})
export class MainComponent extends InjectBase implements OnInit {
  @ViewChild('qrcodeprinter') qrcodeprinter: QrcodePrinterComponent;

  //#region Modal
  
  //#endregion

  data: SearchForOrderDataDTO[] = [];

  dataPrint: OrderDataPrint = <OrderDataPrint>{
    packageQty: 5,
  };
  orderPrintResult: OrderPrintResult[] = [];
  listBrandName: KeyValueUtility[] = [];
  listPackage: KeyValueUtility[] = [];
  listStatus: KeyValueUtility[] = [
    // { key: 'N', value: 'N' },
    // { key: 'Y', value: 'Y' },
    // { key: 'C', value: 'C' },
  ];
  listDataChecked: SearchForOrderDataDTO[] = [];
  param: SearchForOrderDataParam = <SearchForOrderDataParam>{
    brandname: 'All',
    endcod: 'N',
    remark: false,
  };
  pagination: Pagination = <Pagination>{
    pageNumber: 1,
    pageSize: 10,
  };
  bsConfig?: Partial<BsDatepickerConfig>;
  iconButton = IconButton;
  checkAll: boolean = false;
  isPrtno: boolean = true;
  isCheckAll: boolean = true;
  isPackingQty: boolean = true;
  isBalance: boolean = true;
  isQtyOfPrint: boolean = true;

  constructor(
    private service: SearchForOrderDataService,
  ) {
    super();
  }

  ngOnInit(): void {
    this.listStatus.unshift({
      key: 'All',
      value: 'All',
    });
    this.listStatus[0].value = 'All';
    this.getListBrandName();
    this.getListStatus();
    this.getListPackage();

    this.getData();
  }

  getListBrandName() {
    this.spinnerService.show();
    this.service.getListBrandName().subscribe({
      next: (result) => {
        this.spinnerService.hide();
        this.listBrandName = result;
        this.listBrandName.unshift({
          key: 'All',
          value: 'All',
        });
        this.listBrandName[0].value = 'All';
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

  getListStatus() {
    this.spinnerService.show();
    this.service.getListStatus().subscribe({
      next: (result) => {
        this.spinnerService.hide();
        this.listStatus = result;
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

  getListPackage() {
    this.spinnerService.show();
    this.service.getListPackage().subscribe({
      next: (result) => {
        this.spinnerService.hide();
        this.listPackage = result;
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

  getData() {
    this.spinnerService.show();
    this.service.getData(this.pagination, this.param).subscribe({
      next: (result) => {
        this.spinnerService.hide();
        this.data = result.result;
        this.pagination = result.pagination;
        this.checkAll = false;
        this.param.remark ? this.setUIvalue(false) : this.setUIvalue(true);
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

  setUIvalue(value: boolean) {
    this.isPackingQty = value;
    this.isCheckAll = value;
    this.isBalance = value;
    this.isQtyOfPrint = value;
  }

  search() {
    this.isPrtno = !this.param.prtno;
    this.pagination.pageNumber == 1
      ? this.getData()
      : (this.pagination.pageNumber = 1);
  }

  clear() {
    this.param = {
      brandname: 'All',
      rmodel: '',
      bitnbr: '',
      size: '',
      cusid: '',
      manno: '',
      endcod: 'N',
      cusna: '',
      prtno: '',
      remark: false,
    };
    this.listBrandName[0].value = 'All';
    this.listStatus[0].value = 'All';
    this.search();
  }

  pageChanged(e: any) {
    this.pagination.pageNumber = e.page;
    this.getData();
  }

  changCheckAll() {
    // Lấy giá trị khác isDisabled, và gán chúng bằng checkAll
    this.data
      .filter((item) => !item.isDisabled)
      .forEach((item) => (item.isChecked = this.checkAll));
  }

  changeChecked(item: SearchForOrderDataDTO) {
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

  print() {
    // Lấy những dòng dữ liệu đc đánh dấu
    this.listDataChecked = [...this.data.filter((item) => item.isChecked)];
    this.dataPrint.items = [];
    let error: boolean = false;
    // Nếu remark được chọn
    if (
      this.listDataChecked.length == 1 &&
      this.listDataChecked[0].kind == '4' &&
      this.listDataChecked[0].remark != null
    ) {
      let dataChecked: SearchForOrderDataDTO = this.listDataChecked[0];
      this.dataPrint.remark = true;
      // Kiểm tra đã nhập Empno, nếu chưa nhập báo lỗi
      if (!this.dataPrint.empno) {
        error = true;
        return this.snotifyService.error(
          'Please enter the empno',
          CaptionConstants.ERROR
        );
      }
      // Kiểm tra Endcode kiểm tra status, nếu là 'Y' báo lỗi
      if (dataChecked.endcod === 'Y') {
        error = true;
        return this.snotifyService.error(
          'Transaction.SearchForOrderData.FinishedPrint',
          CaptionConstants.ERROR
        );
      }

      this.dataPrint.items.push({
        manno: dataChecked.manno,
        purno: dataChecked.purno,
        size: dataChecked.size,
        wkshno: dataChecked.wkshno,
        wkshqty: dataChecked.wkshqty,
        prtno: dataChecked.prtno,
        maxPqty: dataChecked.avaiablePqty,
      });
      if (!error) this.printOrder();
    } else {
      this.dataPrint.remark = false;
      // TH 2 và TH 3
      // Kiểm tra nếu không có dòng nào đánh dấu báo lỗi
      if (this.listDataChecked?.length < 1) {
        error = true;
        return this.snotifyService.error(
          'Transaction.SearchForOrderData.SelectItems',
          CaptionConstants.ERROR
        );
      }
      // Kiểm tra printQty phải được nhập và lớn hơn 1
      if (!this.dataPrint.printQty || this.dataPrint.printQty < 1) {
        error = true;
        return this.snotifyService.error(
          'Please enter Quantity of print more than 1',
          CaptionConstants.ERROR
        );
      }
      // Kiểm tra Empno phải được nhập
      if (!this.dataPrint.empno) {
        error = true;
        return this.snotifyService.error(
          'Please enter the empno',
          CaptionConstants.ERROR
        );
      }
      // TH 3 If Balance == false, yêu cầu Standard packing quantity bé hơn Quantity of print
      if (
        !this.dataPrint.balance &&
        this.dataPrint.packageQty > this.dataPrint.printQty
      ) {
        error = true;
        return this.snotifyService.error(
          'Please enter Quantity of print more than Standard packing quantity',
          CaptionConstants.ERROR
        );
      }
      // Kiểm tra Endcode kiểm tra status, nếu là 'Y' báo lỗi
      let endcods = this.listDataChecked.filter((item) => item.endcod === 'Y');
      if (endcods.length > 0) {
        error = true;
        return this.snotifyService.error(
          'Please choose Status is "N"',
          CaptionConstants.ERROR
        );
      }
      // ĐK 1 TH 3 
      // Check difference of sum wkshqty - sum pqty vs Quantity of print in list data selected
      let sumWkshqty = this.listDataChecked
        .map((x) => x.wkshqty)
        .reduce((a, b) => a + b);
      let sumPqty = this.listDataChecked
        .map((x) => x.pqty)
        .reduce((a, b) => a + b);
      if (sumWkshqty - sumPqty < this.dataPrint.printQty) {
        error = true;
        return this.snotifyService.error(
          'Printed quantity over than the sum Wkshqty selected order',
          CaptionConstants.ERROR
        );
      }
      // TH 2 Balance is checked
      if (this.dataPrint.balance) {
        this.listDataChecked.forEach((item) => {
          this.dataPrint.items.push({
            manno: item.manno,
            purno: item.purno,
            size: item.size,
            wkshno: item.wkshno,
            wkshqty: item.wkshqty,
            prtno: item.prtno,
            maxPqty: item.avaiablePqty,
          });
        });
      }
      // Balance is NOT checked
      else {
        let order = this.listDataChecked.filter(
          (item) => item.avaiablePqty < this.dataPrint.packageQty
        );
        // Nếu tìm thấy số lượng in nhiều hơn số lượng được phép in, báo lỗi
        if (order.length > 0) {
          error = true;
          return this.snotifyService.error(
            'Transaction.SearchForOrderData.RemainingQuantity',
            CaptionConstants.ERROR
          );
        } else {
          this.listDataChecked.forEach((item) => {
            let maxPqty =
              Math.floor(item.avaiablePqty / this.dataPrint.packageQty) *
              this.dataPrint.packageQty;
            this.dataPrint.items.push({
              manno: item.manno,
              purno: item.purno,
              size: item.size,
              wkshno: item.wkshno,
              wkshqty: item.wkshqty,
              prtno: item.prtno,
              maxPqty: maxPqty,
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
    this.service.print(this.dataPrint).subscribe({
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
                element.avaiablePqty = element.wkshqty - element.pqty;
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
          this.snotifyService.error(
            'Transaction.SearchForOrderData.QuantityExceeded',
            CaptionConstants.ERROR
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
          'System.Message.UnknowError',
          CaptionConstants.ERROR
        );
        this.spinnerService.hide();
      },
    });
  }
}
