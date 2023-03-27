import { Component, OnInit } from '@angular/core';
import { MsPackage } from '../../../../_core/models/msPackage'
import { Pagination, PaginationParam } from '@utilities/pagination-utility';
import { StandardPackingQuantityService } from '../../../../_core/services/maintain/standard-packing-quantity.service';
import { InjectBase } from '../../../../_core/utilities/inject-base-app';


@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss']
})
export class MainComponent extends InjectBase implements OnInit {
  MsPackage: MsPackage[] = [];
  pagination: Pagination = <Pagination>{
    pageNumber: 1,
    pageSize: 10
  }
  param: MsPackage = <MsPackage>{
    manuf: 'N',
    packageNo: '',
    packageQty: 0
  }

  constructor(private service : StandardPackingQuantityService) { super(); }

  ngOnInit(): void {
    this.getData()

  }
  getData(){
    this.service.getData(this.pagination, this.param).subscribe({
      next: res => {
        this.MsPackage = res.result;
        this.pagination = res.pagination;
      }, error: () => {}
    })
  }
  add(){
    this.router.navigate(["standard-packing-quantity/add"])
  }
  edit(msPackage: MsPackage){
    this.router.navigate([`standard-packing-quantity/edit/
    ${msPackage.manuf}/${msPackage.packageNo}`])
  }
  clear(){
    this.param.packageNo = '';
    this.param.packageQty = 0;
    this.getData(); 
  }

  search(){
    this.pagination.pageNumber = 1;
    this.getData();
  }
  pageChanged(e: any){
    this.pagination.pageNumber = e.page;
    this.getData();
  }
}
