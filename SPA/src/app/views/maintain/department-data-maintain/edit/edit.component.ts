import { Component, OnInit } from '@angular/core';
import { MsDepartment } from '@models/maintain/msDepartment';
import { DepartmentDataMaintainService } from '@services/maintain/department-data-maintain-services.service';
import { InjectBase } from '@utilities/inject-base-app';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.scss'],
})
export class EditComponent extends InjectBase implements OnInit {
  msDepartment: MsDepartment = <MsDepartment>{
    manuf: 'N',
    parNo: '',
    parName: '',
  };
  constructor(private service: DepartmentDataMaintainService) {
    super();
  }

  ngOnInit() {
    let manuf = '';
    let parNo = '';

    this.route.params.subscribe((params) => {
      manuf = params['manuf'];
      parNo = params['parNo'];
      console.log(parNo);
      this.getMsDepartment(manuf, parNo);
    });
  }
  getMsDepartment(manuf: string, parNo: string) {
    this.service.getDataOnly(manuf, parNo).subscribe({
      next: (result) => {
        this.msDepartment = result;
        console.log(this.msDepartment);
      },
      error: (err) => console.log(err),
    });
  }

  back() {
    this.router.navigate(['maintain/department-data-maintenance']);
  }

  update() {
    this.service.update(this.msDepartment).subscribe({
      next: (result) => {
        if (result.isSuccess) this.back();
        else alert('Cập nhật thất bại');
      },
    });
  }
}
