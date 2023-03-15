import { Package_dataService } from './../../../_core/services/package_data.service';
import { MS_Package, MS_PackageParam } from './../../../_core/_models/Package-data/Package-data';
import { Component, OnInit } from '@angular/core';
import { Pagination } from '@utilities/pagination-utility';
import { InjectBase } from '@utilities/inject-base-app';
import { PageChangedEvent } from 'ngx-bootstrap/pagination';
import { IconButton } from '@constants/common.constants';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss']
})
export class MainComponent extends InjectBase implements OnInit {

  data: MS_Package[] =[];

  pagination: Pagination = <Pagination>
  {
    pageNumber: 1, pageSize: 10
  }

  params: MS_PackageParam = <MS_PackageParam>
  {
    packageNo : '',
    packageQty: 0

  }

  constructor(private service: Package_dataService ) { super()}

  ngOnInit(): void {
    this.search();
  }


  getData(){
    this.service.getData(this.params, this.pagination).subscribe({
      next: res => {
        console.log(this.params)
        this.data = res.result;
        this.pagination = res.pagination;
      },
      error: () => {
        alert(' Error ')
    }
    })
  }

  search(){
    this.pagination.pageNumber === 1 ? this.getData() : this.pagination.pageNumber = 1;
  }

  pageChanged(event: PageChangedEvent) {
    this.pagination.pageNumber = event.page;
    this.getData();
  }

  iconButton: typeof IconButton = IconButton

  clear() {
    this.params.packageNo = '';
    this.params.packageQty = 0;
    this.getData();
  }

  add() {
    this.router.navigate(['/package-data/add']);
  }
  edit(model: MS_PackageParam) {
    this.params = {
      packageNo: model.packageNo,
      packageQty: model.packageQty
    }
    this.service.dataSources.next(this.params)
    this.router.navigate(['/package-data/edit']);
  }

}
