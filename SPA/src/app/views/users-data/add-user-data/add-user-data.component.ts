
import { Component, OnInit } from '@angular/core';
import { IconButton } from '@constants/common.constants';
import { Users } from '@models/roleUsers';
import { UsersService } from '@services/users.service';
import { InjectBase } from '@utilities/inject-base-app';


@Component({
  selector: 'app-add-user-data',
  templateUrl: './add-user-data.component.html',
  styleUrls: ['./add-user-data.component.scss']
})
export class AddUserDataComponent extends InjectBase implements OnInit {

  params: Users = <Users>
  {
    account: '',
    password: '',
    name: '',
    email: '',
    is_active: false,
  }

  constructor(private userService:UsersService ) {super()}

  ngOnInit(): void {
  }
  iconButton: typeof IconButton = IconButton
  save()
  {
    this.userService.add(this.params).subscribe({
      next: res => {
        console.log(res)
        this.router.navigate(['/users-data']);
      },
      error: () =>{
        alert('save not successfully')
      }
    })
  }
  back(){
    this.router.navigate(['/users-data']);
  }
}
