import { Component, OnInit } from '@angular/core';
import { MsLocation } from '@models/msLocation';
import { WarehouseBasicDataService } from '@services/maintain/warehouse-basic-data.service';
import { InjectBase } from '@utilities/inject-base-app';


@Component({
  selector: 'app-add',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.scss']
})
export class AddComponent extends InjectBase implements OnInit {

  msLocation : MsLocation = <MsLocation>{
    manuf: 'N',
    storeH: '',
    locationName: ''
  }
  constructor(private service: WarehouseBasicDataService) {
    super();
  }

  ngOnInit(): void {
  }

  back(){
    this.router.navigate(["warehouse-basic-data"])
  }

  add(){
    this.service.add(this.msLocation).subscribe({
      next: result => {
        if(result.isSuccess){
          this.back()
        } else {
          alert("Vui lòng thử lại")
        }
      }, error: () => { 
        // Thông báo lỗi
        alert('Lỗi hệ thống');
      }
    })
  }
}
