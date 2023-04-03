import { Component, Inject, OnInit } from '@angular/core';
import { MsDepartment } from '@models/maintain/msDepartment';
import { DepartmentDataMaintainService } from '@services/maintain/department-data-maintain.service';
import { InjectBase } from '@utilities/inject-base-app';

@Component({
  selector: 'app-add',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.scss'],
})
export class AddComponent extends InjectBase implements OnInit {
  msDepartment: MsDepartment = <MsDepartment>{
    manuf: 'N',
    parNo: '',
    parName: '',
  };
  constructor(private service: DepartmentDataMaintainService) {
    super();
  }

  ngOnInit() {}

  back() {
    this.router.navigate(['maintain/department-data-maintain']);
  }

  add() {
    this.service.add(this.msDepartment).subscribe({
      next: (result) => {
        if (result.isSuccess) this.back();
        else alert('Vui lòng thử lại');
      },
      error: () => {
        alert('Lỗi hệ thống');
      },
    });
  }
}
