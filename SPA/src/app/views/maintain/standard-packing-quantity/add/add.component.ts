import { Component, OnInit } from '@angular/core';
import { MsPackage } from '@models/maintain/msPackage';
import { StandardPackingQuantityParam } from '@models/maintain/standardPackingQuantityParam';
import { StandardPackingQuantityService } from '@services/maintain/standard-packing-quantity.service';
import { InjectBase } from '@utilities/inject-base-app';

@Component({
  selector: 'app-add',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.scss']
})
export class AddComponent extends InjectBase implements OnInit {

  msPackage : MsPackage = <MsPackage>{
    manuf: 'N',
    packageNo: '',
    packageQty: 0
  }

  constructor(private service: StandardPackingQuantityService) { super() }

  ngOnInit(): void {
  }

  add(){
    this.service.add(this.msPackage).subscribe({
      next: (result) => {
        if (result.isSuccess) this.back();
        else alert('Vui lòng thử lại');
      },
      error: () => {
        alert('Lỗi hệ thống');
      },
    })
  }

  back(){
    this.router.navigate(["standard-packing-quantity"])
  }


}
