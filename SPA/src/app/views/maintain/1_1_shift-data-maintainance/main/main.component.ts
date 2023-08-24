import { IconButton } from '@constants/common.constants';
import { Pagination } from '@utilities/pagination-utility';
import { InjectBase } from '@utilities/inject-base-app';
import { Component, OnInit } from '@angular/core';
import { MS_Shift } from '@models/common/mS_Shift_DTO';
import { ShiftParam, ShiftType } from '@models/maintain/shift-data-maintenance';
import { ShiftDataMaintainanceService } from '@services/maintain/shift-data-maintainance.service';
import { PageChangedEvent } from 'ngx-bootstrap/pagination';
import { BsModalRef, BsModalService, ModalOptions } from 'ngx-bootstrap/modal';
import { FormComponent } from '../form/form.component';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss']
})
export class MainComponent extends InjectBase implements OnInit {
  param: ShiftParam = <ShiftParam>{
    shift: '',
    shiftName: ''
  }
  shiftType: ShiftType = <ShiftType>{}
  pagination: Pagination = <Pagination>{
    pageNumber: 1,
    pageSize: 10
  }
  data: MS_Shift[] = [];
  title: string = '';
  bsModalRef?: BsModalRef;
  iconButton = IconButton;
  constructor(
    private service: ShiftDataMaintainanceService,
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
    this.param.shift = '';
    this.param.shiftName = '';
    this.pagination.pageNumber = 1;
    this.getData();
  }

  //Mở form modal (add hoặc edit)
  openModal(type: 'add' | 'edit', msShift?: MS_Shift) {
    //Tạo modal
    this.bsModalRef = this.modalService.show(FormComponent, { //Truyền FormComponent để hiển thị form modal (form.component.html)
      //Truyền dữ liệu ban đầu cho modal
      initialState: {
        //Truyền loại thao tác (add hoặc edit)
        shiftType: { type, ms_Shift: msShift || { manuf: '', shift: '', shiftName: '' } } //Nếu msShift không có, tạo mới với giá trị MS_Shift
      }
    });

    //Đăng ký sự kiện từ modal khi có sự kiện shiftEmitter được phát ra
    this.bsModalRef.content.shiftEmitter.subscribe((isSuccess: boolean) => {
      if (isSuccess) this.search();
    });
  }

  //Thêm mới
  add() {
    this.openModal('add');//Không có msShift, tạo mới
  }

  //Chỉnh sửa
  edit(msShift: MS_Shift) {
    this.openModal('edit', { ...msShift, shiftName: '' });//Có msShift, chỉnh sửa
  }
}
