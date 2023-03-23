import { Component, OnInit } from '@angular/core';
import { Users, UsersParam } from '@models/users';
import { Pagination } from '@utilities/pagination-utility';
import { UsersService } from '@services/users.service';
import { Router } from '@angular/router';
@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss']
})
export class MainComponent implements OnInit {

  data: Users[] = [];

  pagination: Pagination = <Pagination>{
    pageNumber: 1,
    pageSize: 10
  }

  params: UsersParam = <UsersParam>{
    account: '',
    name: ''
  }
  constructor(
    private services: UsersService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.getData();
  }

  getData() {
    this.services.getData(this.pagination, this.params).subscribe({
      next: res => {
        this.data = res.result;
        this.pagination = res.pagination;
      }, error: (err) => {
        console.log(err);
      },
      complete: () => {
        console.log('complete');
      }
    })
  }

  add() {
    this.router.navigate(['maintain/authorization-setting/add']);
  }

  updateItem(users: Users) {
    debugger
    this.router.navigate([`maintain/authorization-setting/edit/${users.account}/${users.name}`]);
  }

  clear() {
    this.params.account = '';
    this.params.name = '';
    this.getData() == null;
  }

  search() {
    this.pagination.pageNumber = 1;
    this.getData();
  }

  pageChanged(e: any) {
    this.pagination.pageNumber = e.page;
    this.getData();
  }
}
