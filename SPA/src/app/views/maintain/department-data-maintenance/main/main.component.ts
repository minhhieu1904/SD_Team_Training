import { Component, OnInit } from '@angular/core';
import { DepartmentDataMaintenanceParam, MS_Department } from '@models/department-data-maintenance';
import { DepartmentDataMaintenanceService } from '@services/department-data-maintenance.service';
import { InjectBase } from '@utilities/inject-base-app';
import { Pagination } from '@utilities/pagination-utility';
@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss']
})
export class MainComponent extends InjectBase  implements OnInit {
  data: MS_Department[] = [];
  pagination: Pagination = <Pagination>{
    pageNumber: 1, 
    pageSize: 10
  };
  param: DepartmentDataMaintenanceParam = <DepartmentDataMaintenanceParam>{
    parNo: '',
    parName: ''
  }
  constructor(private service: DepartmentDataMaintenanceService) { super()}

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
        this.spinnerService.hide();
      }
    })
  }
  search() { 
    this.pagination.pageNumber = 1;
    this.getData();
  }

  add(){
    this.router.navigate(["maintain/department-data-maintenance/add"])
  }

  pageChanged(e: any) { 
    this.pagination.pageNumber = e.page;
    this.getData()

  }

  edit(msdepartment: MS_Department){
    // console.log('duwx lieu can ' , ms_Shift);
    // truyền đi cùng dữ liệu
    this.router.navigate([`maintain/department-data-maintenance/edit/${msdepartment.manuf}/${msdepartment.parNo}`])
  }
  clear(){
    this.param.parNo='';
    this.param.parName=''
    this.search()
  }
}
