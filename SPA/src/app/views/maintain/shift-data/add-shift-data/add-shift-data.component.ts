import { Component, OnInit } from '@angular/core';
import { IconButton } from '@constants/common.constants';
import { LangConstants } from '@constants/lang-constant';
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
      next: () => {
        this.router.navigate(['/shift-data-maintenance']);
      },
      error: () => {
        this.spinnerService.hide();
        this.snotifyService.error(this.translateService.instant('System.Message.UnknowError'), this.translateService.instant('System.Caption.Error'))}
    })
  }
  back(){
    this.router.navigate(['/shift-data-maintenance']);
  }
}
