import { WarehouseDataService } from './../../../_core/services/warehouse-data.service';
import { MS_Location, MS_LocationParam } from './../../../_core/_models/warehouse_data/warehouse_data';
import { InjectBase } from '@utilities/inject-base-app';
import { Component, OnInit } from '@angular/core';
import { IconButton } from '@constants/common.constants';

@Component({
  selector: 'app-edit-warehouse-data',
  templateUrl: './edit-warehouse-data.component.html',
  styleUrls: ['./edit-warehouse-data.component.scss']
})
export class EditWarehouseDataComponent extends InjectBase  implements OnInit {



  params: MS_LocationParam = <MS_LocationParam> {
    StoreH: '',
    locationName: ''
  }
  constructor(private service: WarehouseDataService) {super() }

  ngOnInit(): void {
    // lấy dữ liệu từ data truyền vào load dữ liệu cho trang edit
    this.service.currentDataSource.subscribe(res => {
      if(res) {
        this.params.StoreH = res.StoreH,
        this.params.locationName = res.locationName
      }
    })
  }

  iconButton: typeof IconButton = IconButton
  save()
  {
    this.service.upDate(this.params).subscribe({
      next: res => {
        console.log(res)
        alert('save  successfully')
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
