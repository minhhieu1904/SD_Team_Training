import { Component, OnInit } from '@angular/core';
import { MsLocation } from "../../../_core/models/msLocation";
import { WarehouseBasicDataParam } from "../../../_core/models/warehouseBasicDataParam";
import { InjectBase } from '@utilities/inject-base-app';
import { PaginationParam, Pagination } from '@utilities/pagination-utility';
import { WarehouseBasicDataService } from "../../../_core/services/maintain/warehouse-basic-data.service";

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

  param: WarehouseBasicDataParam = <WarehouseBasicDataParam> {
    storeH: '',
    locationName: ''
  }
  constructor(private service: WarehouseBasicDataService) {
    super();
   }

  ngOnInit(): void {
    this.getDataPagination()
  }

  add(){
    this.router.navigate(['warehouse-basic-data/add'])
  }

  edit(msLocation: MsLocation){
    this.router.navigate([`warehouse-basic-data/edit/${msLocation.manuf}/${msLocation.storeH}`])
  }

  search(){
    this.pagination.pageNumber=1;
    this.getDataPagination();
  }

  clear(){
    this.param.storeH = '';
    this.param.locationName = '';
    this.search()
  }

  getDataPagination(){
    this.service.getData(this.pagination, this.param).subscribe({
      next: result => {
        this.pagination = result.pagination;
        this.msLocation = result.result;
      }
    })
  }

  pageChange(e: any){
    this.pagination.pageNumber = e.page;
    this.getDataPagination()
  }
}
