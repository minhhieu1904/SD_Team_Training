import { MS_Department, MS_DepartmentParam } from '@models/department-data/department_data';
import { Component, OnInit } from '@angular/core';
import { InjectBase } from '@utilities/inject-base-app';
import { DepartmentdataService } from '@services/department_data.service';
import { IconButton } from '@constants/common.constants';

@Component({
  selector: 'app-edit-department-data',
  templateUrl: './edit-department-data.component.html',
  styleUrls: ['./edit-department-data.component.scss']
})
export class EditDepartmentDataComponent extends InjectBase  implements OnInit {
  params: MS_Department = <MS_Department>
  {
      manuf: '',
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
      next: res => {
        console.log(res)
        alert('save  successfully')
        this.router.navigate(['/department-data']);
      },
      error: () =>{
        alert('save not successfully')
      }
    })
  }
  back(){
    this.router.navigate(['/department-data']);
  }
}
