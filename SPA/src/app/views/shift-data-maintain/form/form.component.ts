import { Component, OnInit } from '@angular/core';
import { MS_Shift, ShiftDataMaintainParam } from '@models/shift-data-maintain';
import { ShiftDataMaintainService } from '@services/shift-data-maintain.service';
import { InjectBase } from '@utilities/inject-base-app';

@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.scss']
})
export class FormComponent extends InjectBase implements OnInit {
  
  param: ShiftDataMaintainParam = <ShiftDataMaintainParam>{
    shift: '',
    shift_Name: ''
  }

  paramAdd: MS_Shift = <MS_Shift>{
    manuf:'N',
    shift: '',
    shiftName: ''
  }
  constructor(private service: ShiftDataMaintainService) { 
      super()
   }
  
  ngOnInit() {
    
  }
  backlist(){
    this.router.navigate(["shift-data-maintain"])
  }
  save(){
    this.service.addNew(this.paramAdd).subscribe({
      next: res => { 
        if(res.isSuccess)
        {
          this.backlist();
        }
        
      },
      error: () => { 
        alert(' Them khong thanh cong ');
      },
      complete: () => { 
        alert(' Them thanh cong ');
      }

      
    })
  }
  


  

}
