import { Component, OnInit } from '@angular/core';
import { MS_Package } from '@models/MS_Package';
import { StandardPackingQuantityService } from '@services/standard-packing-quantity.service';
import { InjectBase } from '@utilities/inject-base-app';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.scss']
})
export class EditComponent extends InjectBase implements OnInit {
  param: MS_Package = <MS_Package>{
    manuf: '',
    packageNo: '',
    packageQty: 0
  };
  constructor(private service: StandardPackingQuantityService) {
    super();
  }
  backlist(){
    this.router.navigate(["maintain/standard-packing-quantity"])
  }
  ngOnInit() {
    let manuf = '';
    let packageNo = '';
    this.route.params.subscribe(params => {
      manuf = params['manuf'];
      packageNo = params['packageNo'];
      this.getMsDepartment(manuf,packageNo)
    });
    
  }
  //lấy dữ liệu
  getMsDepartment(manuf : string, packageNo : string){
    this.service.getItem(manuf,packageNo).subscribe({
      next: res =>{
        this.param = res;
        console.log('data cần cập nhật',res);
        
      },
      error: (err) => console.log(err),
      complete:() => console.log('complete')
      
    });

  }
  saveUpdate(){
    this.service.update(this.param).subscribe({
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
