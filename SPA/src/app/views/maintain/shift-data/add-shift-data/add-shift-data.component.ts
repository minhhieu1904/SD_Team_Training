import { Component, OnInit } from '@angular/core';
import { IconButton } from '@constants/common.constants';
import { MS_ShiftParam } from '@models/maintain/shift';
import { Shift_dataService } from '@services/maintain/shift_data.service';
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
        this.router.navigate(['/shift-data-maintenance']);
      },
      error: () => {
        alert('Save not successfully')
      }
    })
  }
  back(){
    this.router.navigate(['/shift-data-maintenance']);
  }
}
