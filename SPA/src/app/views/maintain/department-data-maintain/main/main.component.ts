import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Pagination } from '@utilities/pagination-utility';
import { MSDepartment } from '../../../../_core/models/mS_Department'
import { MSDepartmentService } from '../../../../_core/services/ms-department.service'
@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss']
})
export class MainComponent implements OnInit {
  data: MSDepartment[] = [];
  
  pagination: Pagination = <Pagination> {
    pageNumber: 1,
    pageSize: 3
  }

  params: MSDepartment = <MSDepartment> {
    parNo: '',
    parName: ''
  }  
  
  constructor(
    private service: MSDepartmentService,
    private router: Router
    ) { }

  ngOnInit(): void {
    this.getData();
  }

  getData() {
    this.service.getDataPaging(this.pagination, this.params).subscribe({
      next: res => {
        this.data = res.result;
        this.pagination = res.pagination;

      }
    })
  }

  updateItemDPM(msDepartment: MSDepartment) {    
    this.router.navigate([`maintain/department-data-maintain/edit/${msDepartment.manuf}/${msDepartment.parNo}`]);
  }

  pageChanged(e: any) {
    this.pagination.pageNumber = e.page;
    this.getData();
  }

  add() {
    this.router.navigate(['maintain/department-data-maintain/add']);
  }

  search() {
    this.pagination.pageNumber = 1;
    this.getData();
  }

  clear() {
    this.params.parNo = "";
    this.params.parName = "";
    this.getData() == null;
  }
}
