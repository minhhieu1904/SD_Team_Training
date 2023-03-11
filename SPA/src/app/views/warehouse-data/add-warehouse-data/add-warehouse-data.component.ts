import { IconButton } from './../../../_core/constants/common.constants';
import { MS_LocationParam } from './../../../_core/_models/warehouse_data/warehouse_data';
import { WarehouseDataService } from './../../../_core/services/warehouse-data.service';
import { Component, OnInit } from '@angular/core';
import { InjectBase } from '@utilities/inject-base-app';

@Component({
  selector: 'app-add-warehouse-data',
  templateUrl: './add-warehouse-data.component.html',
  styleUrls: ['./add-warehouse-data.component.scss']
})
export class AddWarehouseDataComponent  extends InjectBase implements OnInit {

  constructor(private service: WarehouseDataService) { super()}

  params: MS_LocationParam = <MS_LocationParam>
  {
    StoreH: '',
    locationName: ''
  }
  ngOnInit(): void {
  }
  iconButton: typeof IconButton = IconButton
  save()
  {
    this.service.add(this.params).subscribe({
      next: res => {
        console.log(res)
        this.router.navigate(['/warehouse-data']);
      },
      error: () =>{
        alert('save not successfully')
      }
    })
  }
  back(){
    this.router.navigate(['/warehouse-data']);
  }
}
