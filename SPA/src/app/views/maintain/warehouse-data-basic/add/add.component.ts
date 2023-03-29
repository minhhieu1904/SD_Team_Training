import { Component, OnInit } from '@angular/core';
import { MsLocation } from '@models/maintain/msLocation';
import { WareHouseBasicDataService } from '@services/maintain/warehouse-basic-data.service';
import { InjectBase } from '@utilities/inject-base-app';


@Component({
    selector: 'app-add',
    templateUrl: './add.component.html',
    styleUrls: ['./add.component.scss']
  })
export class AddComponent extends InjectBase implements OnInit {
    ngOnInit(): void {
    }
    msLocation : MsLocation = <MsLocation>{
        manuf: 'N',
        storeH: '',
        locationName: ''
    }
    constructor(private service: WareHouseBasicDataService){
        super()
    }
    back(){
        this.router.navigate(['warehouse-basic-data'])
    }
    add(){
        this.service.add(this.msLocation).subscribe({
            // làm gì tiếp theo
            next: result => {
                if(result.isSuccess){
                    this.back()
                }else{
                    alert("Vui lòng thử lại")
                }
            }, error:()=>{alert('Lỗi hệ thống');}
        })
    }
  }

