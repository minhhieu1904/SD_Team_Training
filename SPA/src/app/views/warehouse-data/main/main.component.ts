import { WarehouseDataService } from './../../../_core/services/warehouse-data.service';

import { MS_Location, MS_LocationParam } from './../../../_core/_models/warehouse_data/warehouse_data';
import { Component, OnInit } from '@angular/core';
import { Pagination } from '@utilities/pagination-utility';
import { InjectBase } from '@utilities/inject-base-app';
import { LocaleData } from 'ngx-bootstrap/chronos';
import { PageChangedEvent } from 'ngx-bootstrap/pagination';
import { IconButton } from '@constants/common.constants';

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
    storeH: '',
    locationName: ''

  }

  constructor(private service: WarehouseDataService ) { super()}

  ngOnInit(): void {
    this.getData();
  }


  getData(){
    // theo thứ tự truyền vào
    this.service.getData(this.params, this.pagination).subscribe({
      next: res => {
        console.log(res)
        this.data = res.result;
        this.pagination = res.pagination;
      },
      error: () => {}
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
    this.params.storeH = '';
    this.params.locationName = '';
    this.getData();
  }

  add() {
    this.router.navigate(['/ware-house/add']);
  }
  edit(model: MS_Location) {
    this.params = {
      storeH: model.storeH,
      locationName: model.locationName
    }
    this.service.dataSources.next(this.params)
    this.router.navigate(['/ware-house/edit']);
  }
}
