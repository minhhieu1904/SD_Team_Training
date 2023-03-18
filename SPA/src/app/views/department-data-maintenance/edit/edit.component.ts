import { Component, OnInit } from '@angular/core';
import { MS_Department } from '@models/department-data-maintenance';
import { DepartmentDataMaintenanceService } from '@services/department-data-maintenance.service';
import { InjectBase } from '@utilities/inject-base-app';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.scss']
})
export class EditComponent extends InjectBase implements OnInit {
  msDepartment: MS_Department = <MS_Department>{
    manuf : '',
    parNo :  '',
    parName : ''
  };
  constructor(private service: DepartmentDataMaintenanceService) { super()}

  backlist(){
    this.router.navigate(["department-data-maintenance"])
  }
  ngOnInit() {
    let manuf = '';
    let parNo = '';
    this.route.params.subscribe(params => {
      manuf = params['manuf'];
      parNo = params['parNo'];
      this.getMsDepartment(manuf,parNo)
    });
    
  }
  //lấy dữ liệu
  getMsDepartment(manuf : string, parNo : string){
    this.service.getItem(manuf,parNo).subscribe({
      next: res =>{
        this.msDepartment = res;
        console.log('data cần cập nhật',res);
        
      },
      error: (err) => console.log(err),
      complete:() => console.log('complete')
      
    });

  }
  saveUpdate(){
    this.service.updateShift(this.msDepartment).subscribe({
      next: res => { 
        if(res.isSuccess)
        {
          this.backlist();
        }
        
      },
      error: () => { 
        alert(' Cập nhật không thành công ');
      },
      complete: () => { 
        alert(' Cập nhật thành công ');
      }

      
    })
  }

}
