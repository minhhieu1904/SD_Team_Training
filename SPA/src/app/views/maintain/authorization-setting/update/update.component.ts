import { OperationResult } from './../../../../_core/utilities/operation-result';
import { NgSnotifyService } from '@services/common/ng-snotify.service';
import { Router } from '@angular/router';
import { UsersService } from '@services/users.service';
import { IconButton } from './../../../../_core/constants/common.constants';
import { Component, OnInit } from '@angular/core';
import { Users } from '@models/roleUsers';

@Component({
  selector: 'app-update',
  templateUrl: './update.component.html',
  styleUrls: ['./update.component.scss']
})
export class UpdateComponent implements OnInit {
  params: Users = <Users>
  {
    password: '',
    email: '',
    is_active: false,
  }
  iconButton = IconButton

  constructor(
    private usersService : UsersService,
    private route : Router,
    private snotify : NgSnotifyService
  ) { }

  ngOnInit(): void {
    // goi ham
    this.getDataFromMain();
  }

  // lay dữ liệu từ trang main bắn sang
  getDataFromMain() {
    this.usersService.currentDataSource.subscribe({
      next:(res) => {
        if(res ==null)
        return this.back()
        this.params = res

      },
      error:() => {
        this.snotify.error('GetData Fail', 'Error')

      }
    })
  }

  saveChanged() {
    this.snotify.confirm('Bạn có muốn cập nhật không?', 'Cập nhật', () => {
      this.usersService.update(this.params).subscribe({
        next: (res: OperationResult) => {
          if (res.isSuccess) {
            this.back();
            this.snotify.success('Update Success', 'Success');
          } else
            this.snotify.error('Update Fail', 'Error');
        },
        error: () => {
          this.snotify.error('Update Fail', 'Error')
        },
        complete: () => {}
      })
    })
  }

  back() {
    this.route.navigate(['maintain/authorization-setting']);
  }
}
