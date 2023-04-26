import { Component, OnInit } from '@angular/core';
import { InjectBase } from '@utilities/inject-base-app';
import { IconButton } from '@constants/common.constants';
import { MS_PackageParam } from '@models/maintain/package';
import { Package_dataService } from '@services/maintain/package_data.service';
@Component({
  selector: 'app-edit-package-data',
  templateUrl: './edit-package-data.component.html',
  styleUrls: ['./edit-package-data.component.scss']
})


  export class EditPackageDataComponent extends InjectBase  implements OnInit {



    params: MS_PackageParam = <MS_PackageParam> {
      packageNo: '',
      packageQty: 0
    }
    constructor(private service: Package_dataService) {super() }

    ngOnInit(): void {
      // lấy dữ liệu từ data truyền vào load dữ liệu cho trang edit
      this.service.currentDataSource.subscribe(res => {
        if(res) {
          this.params.packageNo = res.packageNo,
          this.params.packageQty = res.packageQty
        }
      })
    }

    iconButton: typeof IconButton = IconButton
    save()
    {
      this.service.upDate(this.params).subscribe({
        next: () => {
          this.spinnerService.hide();
          this.snotifyService.success(this.translateService.instant('System.Message.UpdateOKMsg'), this.translateService.instant('System.Caption.Success'));
          this.router.navigate(['maintain/standard-packing-quantity-setting']);
        },
        error: () =>{
          this.spinnerService.hide();
          this.snotifyService.error(this.translateService.instant('System.Message.UnknowError'), this.translateService.instant('System.Caption.Error')
         );
         }
      })
    }
    back(){
      this.router.navigate(['maintain/standard-packing-quantity-setting']);
    }


  }
