import { Component, OnInit } from '@angular/core';
import { MS_Location, WarehouseDataBasic } from '@models/warehouse-basic-data';
import { S_warehouse_basic_dataService } from '@services/S_warehouse_basic_data.service';
import { InjectBase } from '@utilities/inject-base-app';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.scss']
})
export class EditComponent extends InjectBase implements OnInit {
  msLocation: MS_Location = <MS_Location>{
    manuf : '',
    storeH :  '',
    locationName : ''
  };
  constructor(private service: S_warehouse_basic_dataService) {super() }
  backlist(){
    this.router.navigate(["warehouse-basic-data"])
  }
  ngOnInit(): void {
    let manuf = '';
    let storeH = '';
    this.route.params.subscribe(params => {
      manuf = params['manuf'];
      storeH = params['storeH'];
      // console.log(manuf,storeH);
      
      this.getMsLocation(manuf,storeH)
    });
  }
  getMsLocation(manuf : string, storeH : string){
    this.service.getItem(manuf,storeH).subscribe({
      next: res =>{
        this.msLocation = res;
        console.log('data cần cập nhật',res);
        
      },
      error: (err) => console.log(err),
      complete:() => console.log('complete')
      
    });
  }
  saveUpdate(){
    this.service.update(this.msLocation).subscribe({
      next: res => { 
        if(res.isSuccess)
        {
          this.backlist();
        }
        
      },
      error: () => { 
        alert(' Cập nhật không thành công ');
      },
      complete: () => { 
        alert(' Cập nhật thành công ');
      }

      
    })
  }
}
