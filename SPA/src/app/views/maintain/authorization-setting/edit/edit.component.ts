import { Component, OnInit } from '@angular/core';
import { User, UserParam } from '@models/maintain/role';
import { AuthorizationSettingService } from '@services/maintain/authorization-setting.service';
import { InjectBase } from '@utilities/inject-base-app';
import { KeyValuePair } from '@utilities/key-value-pair';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.scss']
})
export class EditComponent extends InjectBase implements OnInit {
  actives: KeyValuePair[] = [
    {key: false, value: 'N'},
    {key: true, value: 'Y'},
  ]
  user: User = <User>{
    account: '',
    email: '',
    name: '',
    password: '',
    isActive: true,
  };

  constructor(private service: AuthorizationSettingService) { super() }
  param: UserParam = <UserParam>{
    account: '',
    name: '',
    email: '',
  };
  ngOnInit(): void {
    let account = '';
    this.route.params.subscribe(param =>{
      account = param['account'];
      this.getUser(account);
    })
  }
  edit(){
    this.service.update(this.user).subscribe({
      next: (result) => {
        if(result.isSuccess) this.back();
        else alert('Cập nhật thất bại');
      }, error: (err)=> alert('Lỗi hệ thống')
    })
  }
  back(){
    this.router.navigate(['authorization-setting'])
  }
  getUser(account: string){
    this.service.getDataOnly(account).subscribe({
      next: (res) => {
        this.user = res;
      }, error: (err) => alert('Lỗi hệ thống')
    })
  }
}
