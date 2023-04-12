import { Component, OnInit } from '@angular/core';
import { DepartmentDataMaintainParam, MsDepartment } from '@models/msDepartment';
import { InjectBase } from '../../../../_core/utilities/inject-base-app';
import { Pagination, PaginationParam } from '@utilities/pagination-utility';
import { DepartmentDataMaintainService } from '../../../../_core/services/maintain/department-data-maintain-services.service';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss']
})
export class MainComponent extends InjectBase implements OnInit {
  data: MsDepartment[] = [];
  pagination: Pagination = <Pagination>{
    pageNumber: 1,
    pageSize: 10
  }
  param: DepartmentDataMaintainParam = <DepartmentDataMaintainParam>{
    parNo: '',
    parName: ''
  }

  constructor(private service : DepartmentDataMaintainService) { super(); }

  ngOnInit(): void {
    this.getData();
  }

  getData(){
    this.service.getData(this.pagination, this.param).subscribe({
      next: res => {
        this.data = res.result;
        this.pagination = res.pagination;
      }, error: () => {}
    })
  }
  add(){
    this.router.navigate(['maintain/department-data-maintenance/add'])
  }
  pageChanged(e: any){
    this.pagination.pageNumber = e.page;
    this.getData();
  }
  edit(msDepartment: MsDepartment){
    this.router.navigate([`maintain/department-data-maintenance/edit/${msDepartment.manuf}/${msDepartment.parNo}`]);
  }
  clear(){
    this.param.parNo='';
    this.param.parName='';
    this.search();
  }
  search(){
    this.pagination.pageNumber = 1;
    this.getData();
  }
}
