import { Component, OnInit } from '@angular/core';
import { MsShift } from '@models/maintain/msShift';
import { ShiftDataMaintainService } from '@services/maintain/shift-data-maintain.service';
import { InjectBase } from '@utilities/inject-base-app';

@Component({
  selector: 'app-add',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.scss']
})
export class AddComponent extends InjectBase implements OnInit {

  msShift: MsShift = <MsShift>{
    manuf: 'N',
    shift: '',
    shiftName: ''
  };

  constructor(private service: ShiftDataMaintainService) { 
    super()
  }

  ngOnInit(): void {
  }

  backlist() {
    this.router.navigate(["shift-data-maintain"])
  }

  add(){
    this.service.add(this.msShift).subscribe({
      next: result => {
        if(result.isSuccess)
          this.backlist()
        else
          alert('Vui lòng thử lại');
      },
      error: () => { 
        // Thông báo lỗi
        alert('Lỗi hệ thống');
      }
    })
  }

}
