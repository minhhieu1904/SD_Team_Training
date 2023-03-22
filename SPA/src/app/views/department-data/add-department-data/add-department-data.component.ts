
import { Component, OnInit } from '@angular/core';
import { InjectBase } from '@utilities/inject-base-app';
import { IconButton } from '@constants/common.constants';
import { DepartmentdataService } from '@services/department_data.service';
import { MS_DepartmentParam } from '@models/department';

@Component({
  selector: 'app-add-department-data',
  templateUrl: './add-department-data.component.html',
  styleUrls: ['./add-department-data.component.scss']
})

export class AddDepartmentDataComponent  extends InjectBase implements OnInit {

  constructor(private service: DepartmentdataService) { super()}

  params: MS_DepartmentParam = <MS_DepartmentParam>
  {
    parNo: '',
    parName: ''
  }
  ngOnInit(): void {
  }
  iconButton: typeof IconButton = IconButton
  save()
  {
    this.service.add(this.params).subscribe({
      next: res => {
        console.log(res)
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




