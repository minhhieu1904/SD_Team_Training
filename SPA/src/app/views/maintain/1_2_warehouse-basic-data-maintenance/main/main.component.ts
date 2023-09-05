import { WarehouseBasicDataMaintenanceService } from '@services/maintain/warehouse-basic-data-maintenance.service';
import { WarehouseParam, WarehouseType } from '@models/maintain/warehouse-basic-data-maintenance';
import { IconButton } from '@constants/common.constants';
import { Pagination } from '@utilities/pagination-utility';
import { InjectBase } from '@utilities/inject-base-app';
import { Component, OnInit } from '@angular/core';
import { PageChangedEvent } from 'ngx-bootstrap/pagination';
import { FormComponent } from '../form/form.component';
import { MS_Location } from '@models/common/mS_Location_DTO';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss']
})
export class MainComponent extends InjectBase implements OnInit {
  param: WarehouseParam = <WarehouseParam>{
    storeH: '',
    locationName: ''
  }
  warehouseType: WarehouseType = <WarehouseType>{}
  pagination: Pagination = <Pagination>{
    pageNumber: 1,
    pageSize: 10
  }
  data: MS_Location[] = [];
  title: string = '';
  bsModalRef?: BsModalRef;
  iconButton = IconButton;
  constructor(
    private service: WarehouseBasicDataMaintenanceService,
    private modalService: BsModalService
  ) {
    super();
  }

  ngOnInit(): void {
    this.route.data.subscribe(res => this.title = res['title'])
    this.getData()
  }

  getData() {
    this.spinnerService.show();
    this.service.getData(this.param, this.pagination).subscribe({
      next: (res) => {
        this.data = res.result;
        this.pagination = res.pagination;
        this.spinnerService.hide();
      },
      error: () => {
        this.snotifyService.error(
          this.translateService.instant('System.Message.SystemError'),
          this.translateService.instant('System.Caption.Error'))
        this.spinnerService.hide()
      },
    })
  }

  pageChanged(event: PageChangedEvent) {
    this.pagination.pageNumber = event.page;
    this.getData();
  }

  search() {
    this.pagination.pageNumber = 1;
    this.getData();
  }

  clear() {
    this.param.storeH = '';
    this.param.locationName = '';
    this.pagination.pageNumber = 1;
    this.getData();
  }

  //Mở form modal (add hoặc edit)
  openModal(type: 'add' | 'edit', msLocation?: MS_Location) {
    //Tạo modal
    this.bsModalRef = this.modalService.show(FormComponent, { //Truyền FormComponent để hiển thị form modal (form.component.html)
      //Truyền dữ liệu ban đầu cho modal
      initialState: {
        //Truyền loại thao tác (add hoặc edit)
        warehouseType: { type, ms_Location: msLocation || { manuf: '', storeH: '', locationName: '' } } //Nếu msShift không có, tạo mới với giá trị MS_Shift
      }
    });

    //Đăng ký sự kiện từ modal khi có sự kiện shiftEmitter được phát ra
    this.bsModalRef.content.warehouseEmitter.subscribe((isSuccess: boolean) => {
      if (isSuccess) this.search();
    });
  }

  //Thêm mới
  add() {
    this.openModal('add');//Không có msShift, tạo mới
  }

  //Chỉnh sửa
  edit(msLocation: MS_Location) {
    this.openModal('edit', { ...msLocation, locationName: '' });//Có msShift, chỉnh sửa
  }
}
