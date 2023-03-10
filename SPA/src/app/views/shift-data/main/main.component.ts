import { IconButton } from './../../../_core/constants/common.constants';
import { Component, OnInit } from '@angular/core';
import { MS_Shift, MS_ShiftParam } from '@models/shift_data/shift_Data';
import { Shift_dataService } from '@services/shift_data.service';
import { InjectBase } from '@utilities/inject-base-app';
import { Pagination } from '@utilities/pagination-utility';
import { PageChangedEvent } from 'ngx-bootstrap/pagination';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss']
})
export class MainComponent extends InjectBase implements OnInit {

  data: MS_Shift[] =[];

  pagination: Pagination = <Pagination>
  {
    pageNumber: 1, pageSize: 10
  }

  params: MS_ShiftParam = <MS_ShiftParam>
  {
      shift: '',
      shiftName: ''

  }

  constructor(private service: Shift_dataService) { super()}

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
    this.params.shift = '';
    this.params.shiftName = '';
    this.getData();
  }

  add() {
    this.router.navigate(['/shift-data/add']);
  }
  edit(model: MS_Shift) {
    this.params = {
      shift: model.shift,
      shiftName: model.shiftName
    }
    this.service.dataSource.next(this.params)
    this.router.navigate(['/shift-data/edit']);
  }
}
