import { Component, OnInit, ViewChild } from '@angular/core';
import { IconButton } from '@constants/common.constants';
import { SearchForOrderDataParam } from '@models/transaction/searchForOrderDataParam';
import { SearchForOrderDataDTO, OrderDataPrint, OrderPrintResult } from '@models/transaction/searchForOrderDataDTO';
import { KeyValueUtility } from '@utilities/key-value-utility';
import { InjectBase } from '@utilities/inject-base-app';
import { SearchForOrderDataService } from '@services/transaction/search-for-order-data.service';
import { CaptionConstants, MessageConstants } from '@constants/message.enum';
import { Pagination } from '@utilities/pagination-utility';
import { OperationResult } from '@utilities/operation-result';
import { QrcodePrinterComponent } from '../../../commons/qrcode-printer/qrcode-printer.component';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss']
})
export class MainComponent extends InjectBase implements OnInit {
  @ViewChild('qrcodeprinter') qrcodeprinter: QrcodePrinterComponent;
  param: SearchForOrderDataParam = <SearchForOrderDataParam>{
    brandname: 'All',
    endcod: 'N',
    remark: false,
  };
  data: SearchForOrderDataDTO[] = [];
  orderPrintResult: OrderPrintResult[] = [];
  listBrandName: KeyValueUtility[] = [];
  listStatus: KeyValueUtility[] = [];
  listPackage: KeyValueUtility[] = [];
  listDataChecked: SearchForOrderDataDTO[] = [];
  dataPrint: OrderDataPrint = <OrderDataPrint>{
    packageQty: 5,
  };
  pagination: Pagination = <Pagination>{
    pageNumber: 1,
    pageSize: 10,
  };
  iconButton = IconButton;
  isCheckAll: boolean = true;
  checkAll: boolean = false;
  isQtyOfPrint: boolean = true;
  isBalance: boolean = true;
  isPackingQty: boolean = true;
  isPrtno: boolean = true;
  constructor(private service: SearchForOrderDataService) {super()}

  ngOnInit(): void {

  }
  getListBrandName(){
    this.service.getListBrandName().subscribe({
      next: res => {
        this.listBrandName = res;
        this.listBrandName.unshift({
          key: 'All',
          value: 'All'
        });
        this.listBrandName[0].value = 'All;'
      },error: () => this.snotifyService.error(MessageConstants.SYSTEM_ERROR_MSG, CaptionConstants.ERROR)
    })
  }
  getListStatus(){
    this.service.getListStatus().subscribe({
      next: res => this.listStatus = res,
      error: () => this.snotifyService.error(MessageConstants.SYSTEM_ERROR_MSG, CaptionConstants.ERROR)
    })
  }
  getListPackage(){
    this.service.getListPackage().subscribe({
      next: res => this.listPackage = res,
      error: () => this.snotifyService.error(MessageConstants.SYSTEM_ERROR_MSG, CaptionConstants.ERROR)
    })
  }
  setUIvalue(value: boolean) {
    this.isPackingQty = value;
    this.isCheckAll = value;
    this.isBalance = value;
    this.isQtyOfPrint = value;
  }
  getData(){
    this.spinnerService.show();
    this.service.getData(this.pagination, this.param).subscribe({
      next: res => {
        this.spinnerService.hide();
        this.data = res.result,
        this.pagination = res.pagination,
        this.checkAll = false,
        this.param.remark ? this.setUIvalue(false) : this.setUIvalue(true);
      }, error: () => this.snotifyService.error(MessageConstants.SYSTEM_ERROR_MSG, CaptionConstants.ERROR)
    })
  }
  search(){
    this.isPrtno = !this.param.prtno;
    this.pagination.pageNumber == 1 ? this.getData() : this.pagination.pageNumber = 1
  }
  clear(){
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
  changeChecked(item: SearchForOrderDataDTO) {
    if(!item.isDisabled){
      if(item.kind == '4' && item.remark != null){
        this.data.filter(val => val.isDisabled == false).map(val => val == item ? val.isChecked =!val.isChecked : val.isChecked = false);
        this.data.filter(val => val.isChecked == true).length > 0 ? this.dataPrint.printQty = item.wkshqty : this.dataPrint.printQty = 0;
      }else{
        item.isChecked = !item.isChecked;
        this.checkAll = this.data.filter((item) => !item.isDisabled).every((x) => x.isChecked);
      }
    }
  }
  // Lấy giá trị khác isDisabled, và gán chúng bằng checkAll
  changCheckAll(){
    this.data.filter(item => !item.isDisabled).forEach(item => (item.isChecked = this.checkAll));
  }
  onCheckValuePrint(event: any) {
    this.dataPrint.printQty = event == 0 ? 1 : event;
  }
  print(){
    // Lấy những dòng được chon
    this.listDataChecked = [...this.data.filter(item => item.isChecked)];
    this.dataPrint.items = [];
    let error: boolean = false;
    // Nếu Listdata có trên 1 dòng đc chọn và kind == 4 và remark đc chọn
    if(this.listDataChecked.length == 1 && this.listDataChecked[0].kind == '4' && this.listDataChecked[0].remark !=null){
      let dataChecked: SearchForOrderDataDTO = this.listDataChecked[0];
      this.dataPrint.remark = true;
      // Kiểm tra empno đã nhập hay chưa
      if(!this.dataPrint.empno){
        error = true;
        return this.snotifyService.error('Please enter the empno. Vui lòng nhập mã Nhân viên', CaptionConstants.ERROR);
      }
      // Kiểm tra trạng thái Endcode nếu là 'Y' báo lỗi
      if (dataChecked.endcod === 'Y') {
        error = true;
        return this.snotifyService.error('Transaction.SearchForOrderData.FinishedPrint', CaptionConstants.ERROR);
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

    }else{
      // Kiểm tra có dòng nào được chọn hay không
      if(this.listDataChecked?.length < 1){
        error = true;
        return this.snotifyService.error('Transaction.SearchForOrderData.SelectItems, Vui lòng chọn dòng để in', CaptionConstants.ERROR);
      }
      // Kiểm tra printQty(Số lượng in) phải được nhập và lớn hơn 1
      if(!this.dataPrint.printQty || this.dataPrint.printQty < 1){
        error = true;
        return this.snotifyService.error('Please enter Quantity of print more than 1, Vui lòng nhập số lượng in Lớn hơn 1', CaptionConstants.ERROR);
      }
      // Kiểm tra Empno được nhập hay chưa
      if (!this.dataPrint.empno) {
        error = true;
        return this.snotifyService.error('Please enter the empno, Vui lòng nhập mã nhân viên', CaptionConstants.ERROR);
      }
      // TH 3 If Balance == false, yêu cầu Standard packing quantity bé hơn Quantity of print
      if (!this.dataPrint.balance && this.dataPrint.packageQty > this.dataPrint.printQty) {
        error = true;
        return this.snotifyService.error('Please enter Quantity of print more than Standard packing quantity, Vui lòng nhập Số lượng in nhiều hơn Số lượng đóng gói tiêu chuẩn', CaptionConstants.ERROR);
      }
      // Kiểm tra trạng thái Endcode, nếu là 'Y' báo lỗi
      let endcods = this.listDataChecked.filter((item) => item.endcod === 'Y');
      if (endcods.length > 0) {
        error = true;
        return this.snotifyService.error('Please choose Status is "N", Vui lòng chọn trạng thái N', CaptionConstants.ERROR);
      }
      // TH 3 
      // ĐK 1 Kiểm tra điều kiện sum wkshqty - sum pqty với Quantity of print trong danh sách đã chọn
      let sumWkshqty = this.listDataChecked.map((x) => x.wkshqty).reduce((a, b) => a + b);
      let sumPqty = this.listDataChecked.map((x) => x.pqty).reduce((a, b) => a + b);
      if (sumWkshqty - sumPqty < this.dataPrint.printQty) {
        error = true;
        return this.snotifyService.error('Printed quantity over than the sum Wkshqty selected order, Số lượng in nhiều hơn tổng đơn hàng đã chọn', CaptionConstants.ERROR);
      }
      // ĐK2 Balance được chọn
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
      // Balance không được chọn
      else {
        let order = this.listDataChecked.filter(item => item.avaiablePqty < this.dataPrint.packageQty);
        // Nếu tìm thấy số lượng in nhiều hơn số lượng được phép in, báo lỗi
        if (order.length > 0) {
          error = true;
          return this.snotifyService.error('Transaction.SearchForOrderData.RemainingQuantity',CaptionConstants.ERROR);
        }else {
          this.listDataChecked.forEach((item) => {
            let maxPqty = Math.floor(item.avaiablePqty / this.dataPrint.packageQty) * this.dataPrint.packageQty;
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
  printOrder(){
    this.spinnerService.show();
    //  vô hiệu hóa cuộn trang trên trang web
    document.body.style.overflow = 'hidden';
    // hiển thị lại các class có ID là printData
    document.getElementById('printData').hidden = false;
    this.service.print(this.dataPrint).subscribe({
      next: async (res : OperationResult) => {
        if(res.isSuccess){
          this.orderPrintResult = res.data
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
            this.listDataChecked.forEach(element => {let data = this.orderPrintResult.filter(x =>
                  x.manno == element.manno &&
                  x.purno == element.purno &&
                  x.size == element.size &&
                  x.wkshno == element.wkshno &&
                  x.prtno == element.prtno
              );
              if (data.length > 0) {
                element.pqty += Object.values(data).reduce((t, { qty }) => t + qty, 0);
                element.avaiablePqty = element.wkshqty - element.pqty;
                element.updated_by = data[0].update_by;
                element.update_time = data[0].update_time;
              }
              element.isChecked = false;
            });
          }
          this.checkAll = false;
          this.clearPrint();
        }else {
          // ẩn các class có ID là printData
          document.getElementById('printData').hidden = true;
          this.snotifyService.error( 'Transaction.SearchForOrderData.QuantityExceeded', CaptionConstants.ERROR);
        }
        // quy định cách hiển thị của nội dung trong HTML overflow = 'auto' tự động tạo thanh cuộn để cho phép cuộn qua phần nội dung vượt quá kích thước
        document.body.style.overflow = 'auto';
        this.spinnerService.hide();
      },error: () => {
        document.body.style.overflow = 'auto';
        // ẩn các class có ID là printData
        document.getElementById('printData').hidden = true;
        this.snotifyService.error('System.Message.UnknowError', CaptionConstants.ERROR);
        this.spinnerService.hide();
      },
    })
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
}
