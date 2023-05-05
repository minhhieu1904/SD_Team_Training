import { Component, OnInit } from '@angular/core';
import { MsShift } from '@models/maintain/msShift';
import { ShiftDataMaintainService } from '@services/maintain/shift-data-maintain.service';
import { InjectBase } from '@utilities/inject-base-app';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.scss']
})
export class EditComponent extends InjectBase implements OnInit {
  
  msShift: MsShift = <MsShift>{
    manuf : 'N',
    shift :  '',
    shiftName : ''
  };
  constructor(private service: ShiftDataMaintainService) {
     super()
    }
  backlist(){
    this.router.navigate(["maintain/shift-data-maintenance"])
  }
  ngOnInit() {
    let manuf = '';
    let shift = '';
    this.route.params.subscribe(params => {
      manuf = params['manuf'];
      shift = params['shift'];
      this.getMsShift(manuf,shift)
    });
    
  }
  //lấy dữ liệu
  getMsShift(manuf : string, shift : string){
    this.service.getDataOnly(manuf,shift).subscribe({
      next: result =>{
        this.msShift = result;
      },
      error: (err) => console.log(err),
      complete:() => console.log('complete')
      
    });

  }
  update(){
    this.service.update(this.msShift).subscribe({
      next: result => { 
        if(result.isSuccess)
        {
          this.backlist();
        }else
          alert(' Cập nhật không thành công ');
      }
    })
  }

}
