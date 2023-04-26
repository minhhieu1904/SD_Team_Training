import { UsersService } from '@services/maintain/users.service';
import { Component, OnInit } from '@angular/core';
import { InjectBase } from '@utilities/inject-base-app';
import { IconButton } from '@constants/common.constants';
import { Users } from '@models/maintain/roleUsers';


@Component({
  selector: 'app-edit-user-data',
  templateUrl: './edit-user-data.component.html',
  styleUrls: ['./edit-user-data.component.scss']
})


export class EditUserDataComponent extends InjectBase  implements OnInit {



  params: Users = <Users> {
    account: '',
    password: '',
    name: '',
    email: '',
    is_active: false,
  }
  constructor(private service: UsersService) {super() }

  ngOnInit(): void {
    // lấy dữ liệu từ data truyền vào load dữ liệu cho trang edit
    this.service.currentDataSource.subscribe(res => {
      if(res) {
        this.params.account = res.account,
        this.params.password = res.password,
        this.params.name = res.name,
        this.params.email = res.email,
        this.params.is_active = res.is_active
      }
    })
  }

  iconButton: typeof IconButton = IconButton
  save()
  {
    this.service.upDate(this.params).subscribe({
      next: () => {
        this.spinnerService.hide();
        this.snotifyService.success(this.translateService.instant('System.Message.UpdateOKMsg'), this.translateService.instant('System.Caption.Success'));
        this.router.navigate(['maintain/authorization-setting']);
      },
      error: () =>{
        this.spinnerService.hide();
        this.snotifyService.error(this.translateService.instant('System.Message.UnknowError'), this.translateService.instant('System.Caption.Error'))
      }
    })
  }
  back(){
    this.router.navigate(['maintain/authorization-setting']);
  }


}
