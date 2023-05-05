import { Component, OnInit } from '@angular/core';
import { MsLocation, WareHouseBasicDataParam } from "@models/maintain/msLocation";
import { InjectBase } from '@utilities/inject-base-app';
import { PaginationParam, Pagination } from '@utilities/pagination-utility';
import { WareHouseBasicDataService } from "@services/maintain/warehouse-basic-data.service";

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss']
})
export class MainComponent extends InjectBase implements  OnInit {
  msLocation : MsLocation[] = [];

  pagination: Pagination = <Pagination>{
    pageNumber : 1,
    pageSize: 10
  }

  param: WareHouseBasicDataParam = <WareHouseBasicDataParam> {
    storeH: '',
    locationName: ''
  }
  constructor(private service: WareHouseBasicDataService) {
    super();
   }

  ngOnInit(): void {
    this.getData()
  }

  add(){
    this.router.navigate(['maintain/warehouse-basic-data-maintenance/add'])
  }

  edit(msLocation: MsLocation){
    this.router.navigate([`maintain/warehouse-basic-data-maintenance/edit/${msLocation.manuf}/${msLocation.storeH}`])
  }

  search(){
    this.pagination.pageNumber=1;
    this.getData();
  }

  clear(){
    this.param.storeH = '';
    this.param.locationName = '';
    this.search()
  }

  getData(){
    this.service.getData(this.pagination, this.param).subscribe({
      next: result => {
        this.pagination = result.pagination;
        this.msLocation = result.result;
      }
    })
  }

  pageChange(e: any){
    this.pagination.pageNumber = e.page;
    this.getData()
  }
}
