import { Component, OnInit } from '@angular/core';
import { IconButton } from '@constants/common.constants';
import { CaptionConstants, MessageConstants } from '@constants/message.enum';
import { MS_DepartmentParam, MS_Department } from '@models/maintain/msdepartment';
import { DepartmentDataMaintenanceService } from '@services/department-data-maintenance.service';
import { InjectBase } from '@utilities/inject-base-app';
import { initOffset } from 'ngx-bootstrap/chronos/units/offset';

@Component({
  selector: 'app-department-form',
  templateUrl: './department-form.component.html',
  styleUrls: ['./department-form.component.scss'],
})
export class DepartmentFormComponent extends InjectBase implements OnInit {
  param = <MS_Department>{
    manuf: 'W',
    parNo: '',
    parName: '',
  };

  data: MS_Department[] = [];
  icon = IconButton;
  title: string;

  constructor(private service: DepartmentDataMaintenanceService) {
    super();
  }

  ngOnInit() {
    if(this.router.url === '/maintain/department-data-maintenance/edit'){
      this.spinnerService.show();
      this.title = 'Edit';
      this.service.dataSource.subscribe({
        next: result => {
          this.spinnerService.hide();
          if(result){
            this.param.parNo = result.parNo;
            this.param.parName = result.parName;
          }
        }
      })
    }
    else{
      this.title = 'Add';
    }
  }

  back() {
    this.router.navigate(['/maintain/department-data-maintenance']);
  }

  save() {
    if(this.router.url === '/maintain/department-data-maintenance/edit'){
      this.spinnerService.show();
      this.service.update(this.param).subscribe({
        next: result => {
          if(result.isSuccess) {
            this.spinnerService.hide();
              this.snotifyService.success(MessageConstants.UPDATED_OK_MSG, CaptionConstants.SUCCESS);
              this.back()
          }else{
            this.snotifyService.error(MessageConstants.UPDATED_ERROR_MSG, CaptionConstants.ERROR);
          }
        },
        error: err => {
          this.snotifyService.error(MessageConstants.UN_KNOWN_ERROR, CaptionConstants.ERROR);
        }
      })
    }
    else{
      this.spinnerService.show();
      this.service.create(this.param).subscribe({
        next: result => {
          if(result.isSuccess){
            this.spinnerService.hide();
            this.snotifyService.success(MessageConstants.CREATED_OK_MSG, CaptionConstants.SUCCESS);
            this.back();
          }
          else{
            this.snotifyService.error(MessageConstants.CREATED_ERROR_MSG, CaptionConstants.ERROR);
          }
        },
        error: err => {
          this.snotifyService.error(MessageConstants.UN_KNOWN_ERROR, CaptionConstants.ERROR);
        }
      })
    }
  }
}
