import { Component, OnInit } from '@angular/core';
import { User } from '@models/maintain/user';
import { AuthorizationSettingService } from '@services/maintain/authorization-setting.service';
import { InjectBase } from '@utilities/inject-base-app';
import { KeyValuePair } from '@utilities/key-value-pair';
@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.scss'],
})
export class EditComponent extends InjectBase implements OnInit {
  actives: KeyValuePair[] = [
    { key: false, value: 'N' },
    { key: true, value: 'Y' },
  ];

  user: User = <User>{
    account: '',
    email: '',
    name: '',
    password: '',
    isActive: true,
  };
  constructor(private service: AuthorizationSettingService) {
    super();
  }

  ngOnInit(): void {
    let account = '';
    this.route.params.subscribe((param) => {
      account = param['account'];
      this.getUser(account);
    });
  }

  back() {
    this.router.navigate(['maintain/authorization-setting']);
  }

  getUser(account: string) {
    this.service.getDataOnly(account).subscribe({
      next: (result) => {
        this.user = result;
      },
      error: (err) => {
        alert('Lỗi hệ thống');
      },
    });
  }

  update() {
    this.service.update(this.user).subscribe({
      next: (result) => {
        if (result.isSuccess) this.back();
        else alert('Vui lòng thử lại');
      },
      error: (err) => {
        alert('Lỗi hệ thống');
      },
    });
  }
}
