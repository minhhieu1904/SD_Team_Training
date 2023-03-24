import { Component, OnInit, TemplateRef } from '@angular/core';
import { UserRole, Users, UsersParam } from '@models/users';
import { Pagination } from '@utilities/pagination-utility';
import { UsersService } from '@services/users.service';
import { Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss']
})
export class MainComponent implements OnInit {
  modalRef?: BsModalRef;
  data: Users[] = [];

  userRole: UserRole = <UserRole>{
    account: '',
    listRoles: []
  };

  userAuthorize: Users = <Users>{};

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
    private router: Router,
    private modelService: BsModalService
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

  save() {
    console.log(this.userRole);
    this.services.updateAuthorizeByUser(this.userRole).subscribe({
      next: res => {
        console.log(res)
        this.closeModal();
      },
      error: (err) => {
        console.log('save not successfully', err)
      }
    })
  }
  getAuthorizeByAccount(account: string) {
    this.services.getAuthorizeByUser(account).subscribe({
      next: res => {
        console.log('get', res);
        this.userRole = res;
        console.log(`quyen user ${this.userRole.account}`, this.userRole);
      },
      error: (err) => {
        console.log(err);
      },
      complete: () => {
        console.log('complete');
      }
    });
  }
  closeModal(modalId?: number) {
    this.modelService.hide(modalId);
  }
  openModal(users: Users, template: TemplateRef<any>) {
    this.getAuthorizeByAccount(users.account);
    console.log(users);
    //gọi lên API lấy danh sách quyền của User đó bao gồm những tin:
    this.userAuthorize.account = users.account;
    this.userAuthorize.name = users.name;
    this.modalRef = this.modelService.show(template);
  }
}
