import { UsersService } from '@services/Maintain/users.service';
import { Component, OnInit } from '@angular/core';
import { InjectBase } from '@utilities/inject-base-app';
import { IconButton } from '@constants/common.constants';
import { Users } from '@models/roleUsers';


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
      next: res => {
        console.log(res)
        alert('save  successfully')
        this.router.navigate(['maintain/authorization-setting']);
      },
      error: () =>{
        alert('update not successfully')
      }
    })
  }
  back(){
    this.router.navigate(['maintain/authorization-setting']);
  }


}
