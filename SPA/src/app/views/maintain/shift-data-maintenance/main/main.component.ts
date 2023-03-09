import { ShiftDataMaintainService } from './../../../../_core/services/shift-data-maintain.service';
import { Component, OnInit } from '@angular/core';
import { MS_Shift_DTO } from '@models/mS_Shift_DTO';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss']
})
export class MainComponent implements OnInit {
param: MS_Shift_DTO
  constructor(
    private service: ShiftDataMaintainService
  ) { }

  ngOnInit(): void {
  }
  getData(){
    //this.service.getData()
  }


}
