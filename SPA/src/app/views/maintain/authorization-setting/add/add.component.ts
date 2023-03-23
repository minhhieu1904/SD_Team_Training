import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Users } from '@models/users';
import { UsersService } from '@services/users.service';

@Component({
  selector: 'app-add',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.scss']
})
export class AddComponent implements OnInit {
  users: Users = <Users>{
    account: '',
    password: '',
    name: '',
    email: '',
    isActive: false,
    updateBy: 'admin'
  };


  constructor(
    private service: UsersService,
    private router: Router
  ) { }

  ngOnInit(): void {
  }


  backList() {
    this.router.navigate(['maintain/authorization-setting']);
  }

  save() {
    this.service.addNew(this.users).subscribe({
      next: res => {
        if(res.isSuccess)
        alert('Them thanh cong')
      },
      error: () => {
        alert('Them that bai')
      }      
    })
  }
}
