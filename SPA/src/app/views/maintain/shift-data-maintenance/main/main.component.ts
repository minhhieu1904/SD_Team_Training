import { IconButton } from './../../../../_core/constants/common.constants';
import { Router } from '@angular/router';
import { Pagination } from './../../../../_core/utilities/pagination-utility';
import { ShiftDataMaintainService } from './../../../../_core/services/shift-data-maintain.service';
import { Component, OnInit } from '@angular/core';
import { MS_Shift } from '@models/mS_Shift_DTO';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss'],
})
export class MainComponent implements OnInit {
  param: MS_Shift = {} as MS_Shift;

  data: MS_Shift[] = []; // Lấy cục data này qua for đổ ra table
  pagination: Pagination = {
    pageNumber: 1,
    pageSize: 10,
  } as Pagination;
  iconButton = IconButton;

  constructor(
    private service: ShiftDataMaintainService,
    private route: Router
  ) {}

  //gọi hàm trong ngOinit, vừa chạy lên thì ngOnInit load đầu tiên
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
    this.route.navigate(['maintain/shift-data-maintenance/add']);
  }

  //tách riêng từng cái ra cho dê hiểu nhé
  update(item: MS_Shift) {
    //khúc này bắt đầu truyền data qua bên trang update
    let msShiftEdit = { ...item };
    this.service.msShift.next(msShiftEdit);
    this.route.navigate(['maintain/shift-data-maintenance/update']); //nhảy qua trang update
  }

  // clear thì chỉ cần cho cái param về lại giá trị mặc dịnh là xong
  clear() {
    this.param = {} as MS_Shift;
    this.getData();
  }

  //này là bấm chuyển trang, mặc đinh 1 trang có row, khi bấm chuyển sẽ qua trang 2 3 4 tùy theo mình chọn
  pageChanged(e: any) {
    this.pagination.pageNumber = e.page;
    this.getData();
  }
}
