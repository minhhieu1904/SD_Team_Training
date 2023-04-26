
import { InjectBase } from '@utilities/inject-base-app';
import { Component, OnInit } from '@angular/core';
import { IconButton } from '@constants/common.constants';
import { MS_LocationParam } from '@models/maintain/warehouse';
import { WarehouseDataService } from '@services/maintain/warehouse-data.service';

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
      next: () => {
        this.spinnerService.hide();
        this.snotifyService.error(this.translateService.instant('System.Message.UpdateOKMsg'), this.translateService.instant('System.Caption.Success'));
        this.router.navigate(['maintain/warehouse-basic-data-maintenance']);
      },
      error: () =>{
        this.spinnerService.hide();
        this.snotifyService.error(this.translateService.instant('System.Message.UnknowError'), this.translateService.instant('System.Caption.Error'))
      }
    })
  }
  back(){
    this.router.navigate(['maintain/warehouse-basic-data-maintenance']);
  }


}
