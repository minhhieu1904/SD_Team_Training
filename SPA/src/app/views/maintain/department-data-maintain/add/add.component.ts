import { Component, Inject, OnInit } from '@angular/core';
import { MsDepartment } from '@models/maintain/msDepartment';
import { DepartmentDataMaintainService } from '@services/maintain/department-data-maintain.service';
import { InjectBase } from '@utilities/inject-base-app';
import { url } from '@constants/url.constants';
import { CaptionConstants, MessageConstants } from '@constants/message.enum';
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
    this.router.navigate([url.maintain.department_data_maintain]);
  }

  add() {
    this.spinnerService.show();
    this.service.add(this.msDepartment).subscribe({
      next: (result) => {
        this.spinnerService.hide();
        if (result.isSuccess) {
          this.snotifyService.success(
            MessageConstants.CREATED_OK_MSG,
            CaptionConstants.SUCCESS
          );
          this.back();
        } else {
          this.snotifyService.error(
            MessageConstants.CREATED_ERROR_MSG,
            CaptionConstants.ERROR
          );
        }
      },
      error: () => {
        this.spinnerService.hide();
        this.snotifyService.error(
          MessageConstants.SYSTEM_ERROR_MSG,
          CaptionConstants.ERROR
        );
      },
    });
  }
}
