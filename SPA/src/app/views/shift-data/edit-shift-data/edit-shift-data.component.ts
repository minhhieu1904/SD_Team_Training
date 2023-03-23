import { Component, OnInit } from '@angular/core';
import { IconButton } from '@constants/common.constants';
import { MS_Shift, MS_ShiftParam } from '@models/shift_data/shift_Data';
import { Shift_dataService } from '@services/shift_data.service';
import { InjectBase } from '@utilities/inject-base-app';

@Component({
  selector: 'app-edit-shift-data',
  templateUrl: './edit-shift-data.component.html',
  styleUrls: ['./edit-shift-data.component.scss']
})
export class EditShiftDataComponent extends InjectBase  implements OnInit {
  params: MS_Shift = <MS_Shift>
  {
      manuf: '',
      shift: '',
      shiftName: ''

  }
  constructor(private service: Shift_dataService) { super() }

  ngOnInit(): void {
    this.service.currentDataSource.subscribe(res => {
      if(res) {
        this.params.shift = res.shift,
        this.params.shiftName = res.shiftName
      }
    })

    // let shift='';
    // let manuf= '';
    // this.route.params.subscribe(params => {
    //   manuf =params['manuf'];
    //   shift =params['shift'];
    //   this.getMsShift(manuf,shift)
    // });
  }

  save() {
    this.service.upDate(this.params).subscribe({
      next: res => {
        console.log(res)
        this.router.navigate(['/shift-data']);
      },
      error: () => {
        alert('Save not successfully')
      }
    })
  }
  back(){
    this.router.navigate(['/shift-data']);
  }
  iconButton: typeof IconButton = IconButton
}
