import { Component, Inject, OnInit } from '@angular/core';
// import { InjectBase } from '../../../_core/utilities/inject-base-app';
// import { S_warehouse_basic_dataService } from '../../../_core/services/S_warehouse_basic_data.service';
import { MS_Location, WarehouseDataBasic } from '@models/warehouse-basic-data';
import { S_warehouse_basic_dataService } from '@services/S_warehouse_basic_data.service';
import { InjectBase } from '@utilities/inject-base-app';
import { Pagination } from '@utilities/pagination-utility';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss']
})
export class MainComponent extends InjectBase implements OnInit {
  data: MS_Location[] = [];
  pagination: Pagination = <Pagination>{
    pageNumber: 1, 
    pageSize: 10
  };
  param: WarehouseDataBasic = <WarehouseDataBasic>{
    StoreH: '',
    LocationName: ''
  }
  constructor(private service : S_warehouse_basic_dataService) { super(); }
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
    this.router.navigate(["maintain/warehouse-basic-data/add"])
  }

  pageChanged(e: any) { 
    this.pagination.pageNumber = e.page;
    this.getData()

  }

  edit(ms_Location: MS_Location){
    // console.log('duwx lieu can ' , ms_Shift);
    // truyền đi cùng dữ liệu
    this.router.navigate([`maintain/warehouse-basic-data/edit/${ms_Location.manuf}/${ms_Location.storeH}`])
  }
  clear(){
    this.param.StoreH='';
    this.param.LocationName=''
    this.search()
  }

}
