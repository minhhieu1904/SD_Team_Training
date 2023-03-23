import { Component, OnInit } from '@angular/core';
import { MsShift, ShiftDataMaintainParam } from '@models/maintain/msShift';
import { PaginationParam, Pagination } from '@utilities/pagination-utility';
import { ShiftDataMaintainService } from "../../../../_core/services/maintain/shift-data-maintain.service";
import { InjectBase } from '@utilities/inject-base-app';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss']
})
export class MainComponent extends InjectBase implements OnInit {


  msShift: MsShift[] = [];
  pagination: Pagination = <Pagination>{
    pageNumber: 1,
    pageSize: 10
  }

  param: ShiftDataMaintainParam = <ShiftDataMaintainParam>{
    shift: '',
    shift_Name: ''
  }

  constructor(private service: ShiftDataMaintainService) {
    super();
  }

  ngOnInit(): void {
    this.getDataPagination();
  }

  add(){
    this.router.navigate(["shift-data-maintain/add"])
  }

  edit(msShift: MsShift){
    console.log('duwx lieu can ' , msShift);
    // truyền đi cùng dữ liệu
    this.router.navigate([`shift-data-maintain/edit/${msShift.manuf}/${msShift.shift}`])
  }

  clear(){
    this.param.shift='';
    this.param.shift_Name=''
    this.search()
  }

  search() { 
    this.pagination.pageNumber = 1;
    this.getDataPagination();
  }

  getDataPagination(){
    this.service.getData(this.pagination, this.param).subscribe({
      // Làm gì kế tiếp
      next: Ketqua => {
        this.pagination = Ketqua.pagination;
        this.msShift = Ketqua.result;
        // console.log(this.msShift);
      }
    })
  }

  pageChanged(e: any) { 
    this.pagination.pageNumber = e.page;
    this.getDataPagination()
  }
}
