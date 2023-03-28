import { Component, OnInit } from '@angular/core';
import { MS_Location } from '@models/warehouse-basic-data';
import { S_warehouse_basic_dataService } from '@services/S_warehouse_basic_data.service';
import { InjectBase } from '@utilities/inject-base-app';

@Component({
  selector: 'app-add',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.scss']
})
export class AddComponent extends InjectBase implements OnInit {
  paramAdd: MS_Location = <MS_Location>{
    manuf: 'N',
    storeH: '',
    locationName: ''
  }
  constructor(private service: S_warehouse_basic_dataService) {super() }

  ngOnInit(): void {

  }
  backlist() {
    this.router.navigate(["maintain/warehouse-basic-data"])
  }
  save(){
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
}
