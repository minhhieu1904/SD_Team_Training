import { Component, OnInit } from '@angular/core';
import { IconButton } from '@constants/common.constants';
import { MS_ShiftParam } from '@models/shift_data/shift_Data';
import { Shift_dataService } from '@services/shift_data.service';
import { InjectBase } from '@utilities/inject-base-app';

@Component({
  selector: 'app-add-shift-data',
  templateUrl: './add-shift-data.component.html',
  styleUrls: ['./add-shift-data.component.scss']
})
export class AddShiftDataComponent extends InjectBase implements OnInit {
  params: MS_ShiftParam = <MS_ShiftParam>
  {
      shift: '',
      shiftName: ''

  }

  constructor(private service: Shift_dataService) { super() }

  ngOnInit(): void {
  }
  iconButton: typeof IconButton = IconButton
  save() {
    this.service.add(this.params).subscribe({
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
}