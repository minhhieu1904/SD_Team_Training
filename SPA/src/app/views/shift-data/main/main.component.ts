import { Component, OnInit } from '@angular/core';
import { MS_Shift, MS_ShiftParam } from '@models/shift_data/shift_Data';
import { Shift_dataService } from '@services/shift_data.service';
import { InjectBase } from '@utilities/inject-base-app';
import { Pagination } from '@utilities/pagination-utility';

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

  }

}
