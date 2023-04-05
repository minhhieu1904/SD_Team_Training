
import { Component, OnInit } from '@angular/core';
import { Pagination } from '@utilities/pagination-utility';
import { InjectBase } from '@utilities/inject-base-app';
import { PageChangedEvent } from 'ngx-bootstrap/pagination';
import { IconButton } from '@constants/common.constants';
import { MS_Department, MS_DepartmentParam } from '@models/department';
import { DepartmentdataService } from '@services/Maintain/department_data.service';


@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss']
})
export class MainComponent extends InjectBase implements OnInit {


  data: MS_Department[] =[];

  pagination: Pagination = <Pagination>
  {
    pageNumber: 1, pageSize: 10
  }

  params: MS_DepartmentParam = <MS_DepartmentParam>
  {
    parNo: '',
    parName: ''
  }

  constructor(private service: DepartmentdataService ) { super()}

  ngOnInit(): void {
    this.getData();
  }


  getData(){
    // theo thứ tự truyền vào
    this.service.getData(this.params, this.pagination).subscribe({
      next: res => {
        console.log(this.params)
        this.data = res.result;
        this.pagination = res.pagination;
      },
      error: () => {
        alert(' Error .....')
    }
    })
  }

  search(){
    this.getData();
  }

  pageChanged(event: PageChangedEvent) {
    this.pagination.pageNumber = event.page;
    this.getData();
  }

  iconButton: typeof IconButton = IconButton

  clear() {
    this.params.parNo = '';
    this.params.parName = '';
    this.getData();
  }

  add() {
    this.router.navigate(['maintain/department-data-maintenance/add']);
  }
  edit(model: MS_DepartmentParam) {
    this.params = {
      parNo: model.parNo,
      parName: model.parName
    }
    this.service.dataSources.next(this.params)
    this.router.navigate(['maintain/department-data-maintenance/edit']);
  }


}
