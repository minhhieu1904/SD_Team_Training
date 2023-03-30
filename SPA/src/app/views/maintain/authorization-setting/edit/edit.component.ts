import { Component, OnInit } from '@angular/core';
import { authorizationSetting, authorizationSettingEdit, Users } from '@models/users';
import { AuthorizationSettingService } from '@services/authorization-setting.service';
import { InjectBase } from '@utilities/inject-base-app';
import { Pagination } from '@utilities/pagination-utility';
import { KeyValuePair } from '@utilities/key-value-pair'

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.scss']
})
export class EditComponent extends InjectBase implements OnInit {
  data: Users[] = [];
  actives: KeyValuePair[] = [
    {key: false, value: 'N'},
    {key: true, value: 'Y'},
  ]
  pagination: Pagination = <Pagination>{
    pageNumber: 1,
    pageSize: 10
  };
  param: authorizationSettingEdit = <authorizationSettingEdit>{
    account: '',
    name: '',
    password: '',
    email: '',
    is_active: true
  }
  constructor(private service: AuthorizationSettingService) { super() }
  ngOnInit(): void {
    let account = '';
    this.route.params.subscribe(params => {
      account = params['account'];
      this.getUsers(account)
    });
  }

  backlist() {
    this.router.navigate(["maintain/authorization-setting"])
  }

  getUsers(account: string) {
    this.spinnerService.show();
    this.service.getItem(account).subscribe({
      next: res => {
        this.param = res;
        // console.log('data cần cập nhật', res);
        this.spinnerService.hide();
      },
      error: (err) => console.log(err),
      complete: () => console.log('complete')

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
