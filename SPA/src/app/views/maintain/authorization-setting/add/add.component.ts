import { Component, OnInit } from '@angular/core';
import { User } from '@models/maintain/role';
import { AuthorizationSettingService } from '@services/maintain/authorization-setting.service';
import { InjectBase } from '@utilities/inject-base-app';
import { KeyValuePair } from '@utilities/key-value-pair';

@Component({
  selector: 'app-add',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.scss']
})
export class AddComponent extends InjectBase implements OnInit {
  actives: KeyValuePair[] = [
    { key: false, value: 'N' },
    { key: true, value: 'Y' },
  ];
  user: User = <User>{
    account: '',
    name: '',
    password: '',
    email: '',
    isActive: true,
    updateBy: 'Admin',
  };
  constructor(private service: AuthorizationSettingService) { super(); }

  ngOnInit(): void {
  }
  back(){
    this.router.navigate(['authorization-setting'])
  }
  add(){
    this.service.add(this.user).subscribe({
      next: (res) => {
        if(res.isSuccess) this.back();
        else alert('Vui lòng thử lại')
      },error: (err) => console.log('Lỗi hệ thống')
    })
  }
}
