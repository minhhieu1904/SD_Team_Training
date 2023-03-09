import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Pagination } from 'src/app/_core/utilities/pagination-utility'
import { MSShift, MSShiftParam } from 'src/app/_core/models/ms-shift';
import { MsShiftService } from 'src/app/_core/services/ms-shift.service';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss']
})
export class MainComponent implements OnInit {
  msshifts: MSShift[] = [];
  
  pagination: Pagination = <Pagination>{
    pageNumber: 1,
    pageSize: 3
  };

  param: MSShift = <MSShift>{
    manuf: '',
    shift: '',
    shiftName: ''
  }
  constructor(private service: MsShiftService, private router: Router) { }

  // Chạy lần đầu khi trang được load lên
  ngOnInit(): void {
    this.getData();
  }
  
  //Lay du lieu
  getData() {
    this.service.getData(this.pagination, this.param).subscribe({
      next: res => {
        this.msshifts = res.result;
        this.pagination = res.pagination;
      }
    }) 
  }

  //Chuyen trang
  addShift() {
    this.router.navigate(['/maintain/shift-data-maintain/add'])
  }
  //Truyền vào item cần cập nhật, hàm này nhận vào 1 cái msShift nè em , nên là ở ngoài mình cũng phải truyền vào item : MsShift
  updateShift(msShift: MSShift) {
    console.log('đây là đối tượng cần cập nhật:', msShift);
    // thiệt lập đường dẫn với data truyền vào 
    // ở đây có 2 khóa chính nên ta truyền vào 2 khóa chính : manuf và shift
    this.router.navigate([`maintain/shift-data-maintain/edit/${msShift.manuf}/${msShift.shift}`])
  }

  //Tim kiem
  search(){
    this.pagination.pageNumber = 1;
    this.getData();
  }


  pageChanged(e:any){
    this.pagination.pageNumber=e.page;
    this.getData();
  }

  clear() {
    this.param.shift = "";
    this.param.shiftName = "";
    this.getData() == null;
  }

}
