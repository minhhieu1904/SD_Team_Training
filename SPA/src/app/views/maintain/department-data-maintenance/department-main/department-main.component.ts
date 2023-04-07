import { Component, OnInit } from '@angular/core'
import { DepartmentDataMaintenanceService } from './../../../../_core/services/department-data-maintenance.service'
import { InjectBase } from '@utilities/inject-base-app'
import { MS_DepartmentParam, MS_Department } from '@models/maintain/msdepartment'
import { IconButton } from '@constants/common.constants'
import { Pagination } from './../../../../_core/utilities/pagination-utility'
@Component({
  selector: 'app-department-main',
  templateUrl: './department-main.component.html',
  styleUrls: ['./department-main.component.scss']
})
export class DepartmentMainComponent extends InjectBase implements OnInit {
  constructor(private service: DepartmentDataMaintenanceService) {
    super()
  }

  ngOnInit() {
    this.getAll()
  }

  param: MS_DepartmentParam = <MS_DepartmentParam>{
    parNo: '',
    parName: ''
  }
  pagination: Pagination = <Pagination>{
    pageNumber: 1,
    pageSize: 10
  }
  icon = IconButton
  data: MS_Department[] = []

  search() {
    if(this.param.parNo === ''){
      this.spinnerService.show()
      this.pagination.pageNumber === 1
      this.service.search(this.pagination.pageNumber, this.pagination.pageSize, this.param.parName || this.param.parNo).subscribe({
        next: result => {
          this.spinnerService.hide();
          this.data = result.result;
          }
      })
    }
    else{
      this.pagination.pageNumber === 1
      ? this.getAll()
      : (this.pagination.pageNumber = 1);
    }

    this.param.parNo = ''
    this.param.parName = ''
  }

  addNew() {
    this.router.navigate(['/maintain/department-data-maintenance/add'])
  }

  clear() {
    this.param.parNo = ''
    this.param.parName = ''
    this.getAll()
  }

  edit(model: MS_Department) {
    this.param = {
      parNo: model.parNo,
      parName: model.parName
    }
    this.service.dataSource.next(this.param);
    this.router.navigate(['/maintain/department-data-maintenance/edit'])
  }

  getAll() {
    this.spinnerService.show()
    this.service.getAll(this.pagination, this.param).subscribe({
      next: result => {
        this.spinnerService.hide()
        this.data = result.result
        this.pagination = result.pagination
      }
    })
  }

  pageChanged(e) {
    this.pagination.pageNumber = e.page
    this.getAll()
  }
}
