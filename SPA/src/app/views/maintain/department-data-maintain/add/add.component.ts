import { Component, OnInit } from '@angular/core';
import { MsDepartment } from '@models/maintain/msDepartment';
import { DepartmentDataMaintainService } from '@services/maintain/department-data-maintain-services.service';
import { InjectBase } from '@utilities/inject-base-app';

@Component({
  selector: 'app-add',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.scss']
})
export class AddComponent extends InjectBase implements OnInit {
  param: MsDepartment = <MsDepartment>{
    manuf: 'N',
    parNo: '',
    parName: ''
  }

  constructor(private service: DepartmentDataMaintainService) { super() }

  ngOnInit(): void {
  }

  backlist(){
    this.router.navigate(["maintain/department-data-maintenance"])
  }
  
  save(){
    this.service.add(this.param).subscribe({
      next: res => {
        if(res.isSuccess){
          this.backlist();
        }else alert('Vui lòng thử lại');
      },error:()=>{alert('Lỗi hệ thống')}
    })
  }

}
