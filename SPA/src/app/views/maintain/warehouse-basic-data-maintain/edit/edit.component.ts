import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MSLocation } from '@models/mS_Location';
import { MsLocationService } from '@services/ms-location.service';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.scss']
})
export class EditComponent implements OnInit {
  msLocationUpdate: MSLocation = <MSLocation>{
    manuf: '',
    storeH: '',
    locationName: ''
  };




  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private service: MsLocationService
  ) { }

  ngOnInit(): void {
    // 2. Khi edit hiện lên lấy lại điều kiện để lấy item cần cập nhật
    let manuf = this.route.snapshot.paramMap.get('manuf');
    let storeH = this.route.snapshot.paramMap.get('storeH');
    console.log(manuf, storeH);
    
    this.getMsLocation(manuf, storeH);
  }

  backList() {
    this.router.navigate(['maintain/warehouse-basic-data-maintain']);
  }

  getMsLocation(manuf: string, storeH: string) {
    this.service.getItem(manuf, storeH).subscribe({
      next: result => {
        
        console.log(result);
        
        this.msLocationUpdate = result;
      },
      error: err => console.log(err),
      complete: () => console.log('complete')
    })
  }

  /// Hàm này được dùng khi bấm nút cập nhật, nên không có khai báo trong onInit()
  updateMsLocation() {
    this.service.updateWarehouse(this.msLocationUpdate).subscribe({
      next: res => {
        if(res.isSuccess)
        alert('Update thanh cong')
      },
      error: () => {
        alert('Update khong thanh cong')
      },
      complete: () => {
        this.backList()
      }
    })
  }
}
