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
  paramAdd: MsShift = <MsShift>{
    manuf: 'N',
    shift: '',
    shiftName: ''
  }
  constructor(private service: ShiftDataMaintainService) {
    super()
  }

  ngOnInit() {

  }

  backlist() {
    this.router.navigate(["maintain/shift-data-maintenance"])
  }

  save(){
      this.service.add(this.paramAdd).subscribe({
        next : result => {
          if(result.isSuccess)
          {
            // hiển thị thông báo thêm mới thành công 

            // Chuyển về trang list
            this.backlist();
          }
        },
        error: (err) => { 
          // Thông báo lỗi
          alert(' Thêm không thành công ');
        },
        complete: () => {
          alert(' Thêm thành công ');
        }

      })
  }

  
}