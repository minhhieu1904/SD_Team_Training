import { Component, OnInit } from '@angular/core';
import { InjectBase } from '@utilities/inject-base-app';
import { StandardPackingQuantityService } from '@services/standard-packing-quantity.service';
import { MS_Package, standardPackingQuantityParam } from '@models/MS_Package';
import { Pagination } from '@utilities/pagination-utility';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss']
})
export class MainComponent extends InjectBase implements OnInit {
  data: MS_Package[] = [];
  pagination: Pagination = <Pagination>{
    pageNumber: 1, 
    pageSize: 10
  };
  param: standardPackingQuantityParam = <standardPackingQuantityParam>{
    packageNo: '',
    packageQty: 0
  }
  constructor(private service: StandardPackingQuantityService) {
    super();
  }

  ngOnInit(): void {
    this.getData();
  }
  getData() { 
    this.spinnerService.show();
    this.service.getData(this.pagination,this.param).subscribe({
      next: res => { 
        this.data = res.result;
        this.pagination = res.pagination;
        this.spinnerService.hide();
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
    this.router.navigate(["maintain/standard-packing-quantity/add"])
  }

  pageChanged(e: any) { 
    this.pagination.pageNumber = e.page;
    this.getData()

  }

  edit(ms_Package: MS_Package){
    // console.log('duwx lieu can ' , ms_Shift);
    // truyền đi cùng dữ liệu
    this.router.navigate([`maintain/standard-packing-quantity/edit/${ms_Package.manuf}/${ms_Package.packageNo}`])
  }
  clear(){
    this.param.packageNo='';
    this.param.packageQty = 0
    this.search()
  }
}
