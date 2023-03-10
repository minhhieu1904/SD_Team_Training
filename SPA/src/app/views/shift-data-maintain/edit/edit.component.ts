import { Component, OnInit } from '@angular/core';
import { MS_Shift } from '@models/shift-data-maintain';
import { ShiftDataMaintainService } from '@services/shift-data-maintain.service';
import { InjectBase } from '@utilities/inject-base-app';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.scss']
})
export class EditComponent extends InjectBase implements OnInit {
  
  msShift: MS_Shift = <MS_Shift>{
    manuf : '',
    shift :  '',
    shiftName : ''
  };
  constructor(private service: ShiftDataMaintainService) {
     super()
    }
  backlist(){
    this.router.navigate(["shift-data-maintain"])
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
    this.service.getItem(manuf,shift).subscribe({
      next: res =>{
        this.msShift = res;
        console.log('data cần cập nhật',res);
        
      },
      error: (err) => console.log(err),
      complete:() => console.log('complete')
      
    });

  }
  saveUpdate(){
    this.service.updateShift(this.msShift).subscribe({
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
