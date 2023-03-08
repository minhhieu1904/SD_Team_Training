import { Component, OnInit } from '@angular/core';
import { MS_Shift } from '../../../_core/models/shift-data-maintain';
import { ShiftDataMaintainService } from '../../../_core/services/shift-data-maintain.service';
import { InjectBase } from '../../../_core/utilities/inject-base-app';
@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss']
})
export class MainComponent extends InjectBase implements OnInit {
  model: MS_Shift = <MS_Shift>{}
  constructor(private service: ShiftDataMaintainService) { super()}

  ngOnInit() {
    this.getData();
  }


  getData() { 
    this.service.getData().subscribe({
      next: res => { 
        console.log(res)
        this.model = res
      },
      error: () => { 
      }
    })
  }

  search() { 
    console.log(this.model)

  }

}
