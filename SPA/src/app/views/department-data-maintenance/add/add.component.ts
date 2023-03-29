import { Component, OnInit } from '@angular/core';
import { MS_Department } from '@models/department-data-maintenance';
import { DepartmentDataMaintenanceService } from '@services/department-data-maintenance.service';
import { InjectBase } from '@utilities/inject-base-app';

@Component({
  selector: 'app-add',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.scss']
})
export class AddComponent extends InjectBase implements OnInit {
  paramAdd: MS_Department = <MS_Department>{
    manuf: 'N',
    parNo: '',
    parName: ''
  }
  constructor(private service: DepartmentDataMaintenanceService) { super()}

  ngOnInit(): void {
  }
  backlist() {
    this.router.navigate(["maintain/department-data-maintenance"])
  }

  save(){
      this.service.addNew(this.paramAdd).subscribe({
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
