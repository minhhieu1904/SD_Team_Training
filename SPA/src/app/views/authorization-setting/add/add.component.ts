import { Component, OnInit } from '@angular/core';
import { Users } from '@models/users';
import { AuthorizationSettingService } from '@services/authorization-setting.service';
import { InjectBase } from '@utilities/inject-base-app';
import { KeyValuePair } from '@utilities/key-value-pair';

@Component({
  selector: 'app-add',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.scss']
})
export class AddComponent extends InjectBase implements OnInit {
  actives: KeyValuePair[] = [
    {key: false, value: 'N'},
    {key: true, value: 'Y'},
  ]
  users: Users = <Users>{
    account: '',
    name: '',
    password: '',
    email: '',
    is_active: true,
    update_by: 'Admin'
  }
  
  constructor(private service: AuthorizationSettingService) {super() }

  ngOnInit(): void {
  }
  backlist() {
    this.router.navigate(["maintain/authorization-setting"])
  }

  save(){
      console.log('loi ',this.users);
      this.service.addNew(this.users).subscribe({
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
          console.log(err)
          alert(' Thêm không thành công ');
        },
        complete: () => {
          alert(' Thêm thành công ');
        }

      })
  }

}
