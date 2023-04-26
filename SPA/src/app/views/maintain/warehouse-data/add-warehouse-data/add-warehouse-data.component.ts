
import { Component, OnInit } from '@angular/core';
import { IconButton } from '@constants/common.constants';
import { MS_LocationParam } from '@models/maintain/warehouse';
import { WarehouseDataService } from '@services/maintain/warehouse-data.service';
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
