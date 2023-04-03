import { NgSnotifyService } from '@services/common/ng-snotify.service';
import { OperationResult } from './../../../../_core/utilities/operation-result';
import { IconButton } from './../../../../_core/constants/common.constants';
import { UsersService } from '@services/users.service';
import { Users } from '@models/roleUsers';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.scss']
})
export class FormComponent implements OnInit {
  params: Users = <Users>
  {
    account: '',
    password: '',
    name: '',
    email: '',
    is_active: false,
  }
  iconButton = IconButton

  constructor(
    // goi service vua tao
    private usersService :UsersService,
    private route: Router,
    private snotify: NgSnotifyService
    ) { }

  ngOnInit(): void {
  }

  saveChanged(){
     this.snotify.confirm('Bạn có muốn thêm mới không?', 'Thêm mới', () => {
        this.usersService.add(this.params).subscribe({
          next: (res: OperationResult) => {
            if (res.isSuccess) {
              this.back();
              this.snotify.success('Add Success', 'Success');
            } else
              this.snotify.error('Add Fail', 'Error');
          },
          error: () => {
            this.snotify.error('Add Fail', 'Error')
          },
          complete: () => {}
        })
     })
  }

  back(){
    this.route.navigate(['maintain/authorization-setting']);
  }
}
