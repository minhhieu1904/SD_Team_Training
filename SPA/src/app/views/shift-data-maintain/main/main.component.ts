import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Pagination } from '@utilities/pagination-utility';
import { MS_Shift, ShiftDataMaintainParam } from '../../../_core/models/shift-data-maintain';
import { ShiftDataMaintainService } from '../../../_core/services/shift-data-maintain.service';
import { InjectBase } from '../../../_core/utilities/inject-base-app';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss']
})
export class MainComponent extends InjectBase implements OnInit {
  data: MS_Shift[] = [];
  pagination: Pagination = <Pagination>{
    pageNumber: 1, 
    pageSize: 10
  };
  param: ShiftDataMaintainParam = <ShiftDataMaintainParam>{
    shift: '',
    shift_Name: ''
  }
  constructor(private service: ShiftDataMaintainService) { super()}

  ngOnInit() {
    this.getData();
  }


  getData() { 
    this.service.getData(this.pagination,this.param).subscribe({
      next: res => { 
        this.data = res.result;
        this.pagination = res.pagination;
      },
      error: () => { 
      }
    })
  }

  search() { 
    this.pagination.pageNumber = 1;
    this.getData();
  }

  add(){
    this.router.navigate(["shift-data-maintain/add"])
  }

  pageChanged(e: any) { 
    this.pagination.pageNumber = e.page;
    this.getData()

  }

  edit(ms_Shift: MS_Shift){
    // console.log('duwx lieu can ' , ms_Shift);
    // truyền đi cùng dữ liệu
    this.router.navigate([`shift-data-maintain/edit/${ms_Shift.manuf}/${ms_Shift.shift}`])
  }
  clear(){
    this.param.shift='';
    this.param.shift_Name=''
    this.search()
  }

}