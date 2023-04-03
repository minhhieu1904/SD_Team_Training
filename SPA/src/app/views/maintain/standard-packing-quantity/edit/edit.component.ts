import { Component, OnInit } from '@angular/core';
import { MsPackage } from '@models/maintain/msPackage';
import { StandardPackingQuantityService } from '@services/maintain/standard-packing-quantity.service';
import { InjectBase } from '@utilities/inject-base-app';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.scss'],
})
export class EditComponent extends InjectBase implements OnInit {
  msPackage: MsPackage = <MsPackage>{
    manuf: 'N',
    packageNo: '',
    packageQty: 0,
  };
  constructor(private service: StandardPackingQuantityService) {
    super();
  }

  ngOnInit(): void {
    let manuf = '';
    let packageNo = '';

    this.route.params.subscribe((params) => {
      manuf = params['manuf'];
      packageNo = params['packageNo'];
      this.getMsPackage(manuf, packageNo);
    });
  }
  getMsPackage(manuf: string, packageNo: string) {
    this.service.getDataOnly(manuf, packageNo).subscribe({
      next: (result) => {
        this.msPackage = result;
        console.log(this.msPackage);
      },
      error: (err) => console.log(err),
    });
  }

  update() {
    this.service.update(this.msPackage).subscribe({
      next: (result) => {
        if (result.isSuccess) this.back();
        else alert('Cập nhật thất bại');
      },
    });
  }
  back() {
    this.router.navigate(['maintain/standard-packing-quantity']);
  }
}
