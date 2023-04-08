import { Package_dataService } from '@services/maintain/package_data.service';

import { Component, OnInit } from '@angular/core';
import { InjectBase } from '@utilities/inject-base-app';
import { IconButton } from '@constants/common.constants';

import { MS_PackageParam } from '@models/maintain/package';

@Component({
  selector: 'app-add-package-data',
  templateUrl: './add-package-data.component.html',
  styleUrls: ['./add-package-data.component.scss']
})
  export class AddPackageDataComponent  extends InjectBase implements OnInit {

    constructor(private service: Package_dataService) { super()}

    params: MS_PackageParam = <MS_PackageParam>
    {
      packageNo: '',
      packageQty: 0
    }
    ngOnInit(): void {
    }
    iconButton: typeof IconButton = IconButton
    save()
    {
      this.service.add(this.params).subscribe({
        next: res => {
          console.log(res)
          this.router.navigate(['maintain/standard-packing-quantity-setting']);
        },
        error: () =>{
          alert('save not successfully')
        }
      })
    }
    back(){
      this.router.navigate(['maintain/standard-packing-quantity-setting']);
    }
  }

