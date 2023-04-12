import { Component, OnInit } from '@angular/core';
import { MsLocation } from '@models/maintain/msLocation';
import { WareHouseBasicDataService } from '@services/maintain/warehouse-basic-data.service';
import { InjectBase } from '@utilities/inject-base-app';


@Component({
    selector: 'app-edit',
    templateUrl: './edit.component.html',
    styleUrls: ['./edit.component.scss']
  })
export class EditComponent extends InjectBase implements OnInit {
    ngOnInit(): void {
        let manuf = '';
        let storeH = ''
        this.route.params.subscribe(params =>{
            manuf = params['manuf'];
            storeH = params['storeH'];
            this.getMsLocation(manuf,storeH);
        });
    }
    msLocation : MsLocation = <MsLocation>{
        manuf: 'N',
        storeH: '',
        locationName: ''
    }
    constructor(private service: WareHouseBasicDataService){
        super()
    }
    getMsLocation(manuf:string, storeH:string){
        this.service.getDataOnly(manuf,storeH).subscribe({
            next:result=>{
                this.msLocation=result;
            }, error:(err)=>console.log(err)
        });
    }
    back(){
        this.router.navigate(['maintain/warehouse-basic-data-maintenance'])
    }
    update(){
        this.service.update(this.msLocation).subscribe({
            // làm gì tiếp theo
            next: result => {
                if(result.isSuccess){
                    this.back()
                }else{
                    alert("Cập nhật thất bại")
                }
            }
        })
    }
  }

