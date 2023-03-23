import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Users } from '@models/users';
import { UsersService } from '@services/users.service';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.scss']
})
export class EditComponent implements OnInit {
  users: Users = <Users>{
    account: '',
    password: '',
    name: '',
    email: '',
    isActive: false
  }


  constructor(
    private service: UsersService,
    private router: Router,
    private route: ActivatedRoute
  ) { }

  ngOnInit(): void {
    let account: '';
    let name: '';
    this.route.params.subscribe(params => {
      account = params['account'];
      name = params['name'];
      this.getItem(account, name);
    });
    this.updateItem();
  }

  backList() {
    this.router.navigate(['maintain/authorization-setting']);
  }

  getItem(account: string, name: string) {
    this.service.getItem(account, name).subscribe({
      next: res => {
        this.users = res;
      }, error: (err) => {
        console.log(err);
      }, complete: () => {
        console.log('complete');
      }
    })
  }

  updateItem() {
    this.service.editItem(this.users).subscribe({
      next: res => {
        if (res.isSuccess)
          alert('Update thanh cong')
      }, error: (err) => {
        console.log(err);
      }, complete: () => {
        console.log('complete');
      }
    })
  }
}
