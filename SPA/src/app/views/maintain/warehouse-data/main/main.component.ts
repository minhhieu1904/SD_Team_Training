
import { Component, OnInit } from '@angular/core';
import { Pagination } from '@utilities/pagination-utility';
import { InjectBase } from '@utilities/inject-base-app';
import { PageChangedEvent } from 'ngx-bootstrap/pagination';
import { IconButton } from '@constants/common.constants';
import { MS_Location, MS_LocationParam } from '@models/maintain/warehouse';
import { WarehouseDataService } from '@services/maintain/warehouse-data.service';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss']
})

export class MainComponent extends InjectBase implements OnInit {

  data: MS_Location[] =[];

  pagination: Pagination = <Pagination>
  {
    pageNumber: 1, pageSize: 10
  }

  params: MS_LocationParam = <MS_LocationParam>
  {
    StoreH: '',
    locationName: ''

  }

  constructor(private service: WarehouseDataService ) { super()}

  ngOnInit(): void {
    this.search();
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
    this.params.StoreH = '';
    this.params.locationName = '';
    this.getData();
  }

  add() {
    this.router.navigate(['maintain/warehouse-basic-data-maintenance/add']);
  }
  edit(model: MS_Location) {
    this.params = {
      StoreH: model.storeH,
      locationName: model.locationName
    }
    this.service.dataSources.next(this.params)
    this.router.navigate(['maintain/warehouse-basic-data-maintenance/edit']);
  }
}
