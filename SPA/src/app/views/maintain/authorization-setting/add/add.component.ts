import { Component, OnInit } from '@angular/core';
import { User } from '@models/maintain/user';
import { AuthorizationSettingService } from '@services/maintain/authorization-setting.service';
import { InjectBase } from '@utilities/inject-base-app';
import { KeyValuePair } from '@utilities/key-value-pair';
@Component({
  selector: 'app-add',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.scss'],
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

  constructor(private service: AuthorizationSettingService) {
    super();
  }

  ngOnInit(): void {}

  add() {
    this.service.add(this.user).subscribe({
      next: (result) => {
        if (result.isSuccess) this.back();
        else alert('Vui lòng thử lại');
      },
      error: (e) => {
        alert('Lỗi hệ thống');
      },
    });
  }

  back() {
    this.router.navigate(['maintain/authorization-setting']);
  }
}
