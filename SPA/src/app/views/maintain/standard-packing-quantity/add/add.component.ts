import { Component, OnInit } from '@angular/core';
import { MS_Package } from '@models/MS_Package';
import { StandardPackingQuantityService } from '@services/standard-packing-quantity.service';
import { InjectBase } from '@utilities/inject-base-app';

@Component({
  selector: 'app-add',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.scss']
})
export class AddComponent extends InjectBase implements OnInit {
  paramAdd: MS_Package = <MS_Package>{
    manuf: '',
    packageNo: '',
    packageQty: 0
  }
  constructor(private service: StandardPackingQuantityService) {
    super();
  }

  ngOnInit(): void {
  }
  backlist() {
    this.router.navigate(["maintain/standard-packing-quantity"])
  }

  save(){
    console.log(this.paramAdd)
      this.service.addNew(this.paramAdd).subscribe({
        next : result => {
          if(result.isSuccess)
          {
            // hiển thị thông báo thêm mới thành công 

            // Chuyển về trang list
            this.backlist();
          }
        },
        error: (err) => { 
          // Thông báo lỗi
          alert(' Thêm không thành công ');
        },
        complete: () => {
          alert(' Thêm thành công ');
        }

      })
  }
  saveNext() {
    this.service.update(this.paramAdd).subscribe({
      next: res => {
        if (res.isSuccess) {
          this.paramAdd.packageNo = "";
          this.paramAdd.packageQty = 0
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
