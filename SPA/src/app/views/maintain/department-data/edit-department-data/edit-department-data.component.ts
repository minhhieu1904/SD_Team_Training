
import { Component, OnInit } from '@angular/core';
import { InjectBase } from '@utilities/inject-base-app';
import { IconButton } from '@constants/common.constants';
import { MS_DepartmentParam } from '@models/maintain/department';
import { DepartmentdataService } from '@services/maintain/department_data.service';

@Component({
  selector: 'app-edit-department-data',
  templateUrl: './edit-department-data.component.html',
  styleUrls: ['./edit-department-data.component.scss']
})
export class EditDepartmentDataComponent extends InjectBase  implements OnInit {
  params: MS_DepartmentParam = <MS_DepartmentParam>
  {

      parNo: '',
      parName: ''
  }

  constructor(private service: DepartmentdataService) {super() }

  ngOnInit(): void {
    // lấy dữ liệu từ data truyền vào load dữ liệu cho trang edit
    this.service.currentDataSource.subscribe(res => {
      if(res) {
        this.params.parNo = res.parNo,
        this.params.parName = res.parName
      }
    })
  }
  iconButton: typeof IconButton = IconButton
  save()
  {
    this.service.upDate(this.params).subscribe({
      next: () => {
        this.spinnerService.hide();
        this.snotifyService.success(this.translateService.instant('System.Message.UpdateOKMsg'), this.translateService.instant('System.Caption.Success'));
        this.router.navigate(['maintain/department-data-maintenance']);
      },
      error: () =>{
        this.spinnerService.hide();
        this.snotifyService.error(this.translateService.instant('System.Message.UnknowError'), this.translateService.instant('System.Caption.Error'))
      }
    })
  }
  back(){
    this.router.navigate(['maintain/department-data-maintenance']);
  }
}
