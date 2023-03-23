import { Component, OnInit } from '@angular/core';
import { MsShift, ShiftDataMaintainParam } from '@models/maintain/msShift';
import { PaginationParam } from '@utilities/pagination-utility';
import { ShiftDataMaintainService } from "../../../../_core/services/maintain/shift-data-maintain.service";

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss']
})
export class MainComponent implements OnInit {

  msShift: MsShift[] = [];
  pagination: PaginationParam = <PaginationParam>{
    pageNumber: 1,
    pageSize: 10
  }

  param: ShiftDataMaintainParam = <ShiftDataMaintainParam>{
    shift: '',
    shift_Name: ''
  }

  constructor(private service: ShiftDataMaintainService) { }

  ngOnInit(): void {
    this.getDataPagination();
  }


  getDataPagination(){
    this.service.getData(this.pagination, this.param).subscribe({
      // Làm gì kế tiếp
      next: Ketqua => {
        this.pagination = Ketqua.pagination;
        this.msShift = Ketqua.result;
        console.log(this.msShift);
      }
    })
  }
}
