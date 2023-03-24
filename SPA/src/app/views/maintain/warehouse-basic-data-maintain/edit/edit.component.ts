import { Component, OnInit } from '@angular/core';
import { MsLocation } from '@models/msLocation';
import { WarehouseBasicDataService } from '@services/maintain/warehouse-basic-data.service';
import { InjectBase } from '@utilities/inject-base-app';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.scss']
})
export class EditComponent extends InjectBase implements OnInit {

  msLocation: MsLocation = <MsLocation> {
    manuf : 'N',
    storeH: '',
    locationName: ''
  }
  constructor(private service: WarehouseBasicDataService) {
    super();
   }

  ngOnInit(): void {
    let manuf = '';
    let storeH = '';

    this.route.params.subscribe(params => {
      manuf = params['manuf'];
      storeH = params['storeH'];
      this.getMsLocation(manuf,storeH)
    });
  }

  getMsLocation(manuf : string, storeH : string){
    this.service.getDataOnly(manuf,storeH).subscribe({
      next: result =>{
        this.msLocation = result;
      },
      error: (err) => console.log(err) 
    });

  }

  back() {
    this.router.navigate(["warehouse-basic-data"])
  }

  update() {
    this.service.update(this.msLocation).subscribe({
      next: res => { 
        if(res.isSuccess)
        {
          this.back();
        } else {
          alert('Cập nhật thất bại')
        }
      }
    })
}
}
