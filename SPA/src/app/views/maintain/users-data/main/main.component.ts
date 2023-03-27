

import { InjectBase } from '@utilities/inject-base-app';
import { IconButton } from '@constants/common.constants';
import { PageChangedEvent } from 'ngx-bootstrap/pagination';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { UserRole, Users } from '@models/roleUsers';
import { Pagination } from '@utilities/pagination-utility';
import { UsersService } from '@services/users.service';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss'],
})
export class MainComponent extends InjectBase implements OnInit {
  modalRef?: BsModalRef | null;
  data: Users[] = [];

  iconButton: typeof IconButton = IconButton;

  // Danh sách quyền theo User
  userRole: UserRole = <UserRole>{
    account: '',
    listRoles: [],
  };

  userAuthorize: Users = <Users>{ };

  pagination: Pagination = <Pagination>{
    pageNumber: 1,
    pageSize: 10,
  };

  params: Users = <Users>{
    account: '',
    password: '',
    name: '',
    email: '',
    is_active: false,
  };

  constructor(
    private userService: UsersService,
    private modalService: BsModalService
  ) {
    super();
  }

  ngOnInit(): void {
    this.search();
  }

  getData() {
    this.userService.getData(this.params, this.pagination).subscribe({
      next: (res) => {
        console.log(this.params);
        this.data = res.result;
        this.pagination = res.pagination;
      },
      error: () => {
        alert(' Error ');
      },
    });
  }

  search() {
    this.pagination.pageNumber === 1
      ? this.getData()
      : (this.pagination.pageNumber = 1);
  }

  pageChanged(event: PageChangedEvent) {
    this.pagination.pageNumber = event.page;
    this.getData();
  }


  clear() {
    (this.params.account = ''), (this.params.name = ''), this.getData();
  }

  add() {
    this.router.navigate(['maintain/authorization-setting/add']);
  }

  edit(model: Users) {
    this.params = {
      account: model.account,
      password: model.password,
      name: model.name,
      email: model.email,
      is_active: model.is_active,
    };
    this.userService.dataSources.next(this.params);
    this.router.navigate(['maintain/authorization-setting/edit']);
  }
  getAuthorizeByAccount(account: string) {
    this.spinnerService.show();
    this.userService.getAuthorizeByUser(account).subscribe({
      next: (result) => {
        console.log(result)
        this.userRole = result;
        console.log(`quyen user ${this.userRole.account}`, this.userRole);

        this.spinnerService.hide();
      },
      error: (err) => {
        this.spinnerService.hide();
        console.log(err);
      },
      complete: () => {
        this.spinnerService.hide();
      },
    });
  }

  save()
  {
    this.userService.updateAuthorizeByUser(this.userRole).subscribe({
      next: res => {
        console.log(res)
        this.closeModal();
      },
      error: (err) =>{
        console.log('save not successfully', err)
      }
    })
  }
  openModal(user: Users, template: TemplateRef<any>) {
    this.getAuthorizeByAccount(user.account);
    console.log(user);
    // gọi lên API lấy danh sách quyền của User đó bao gồm những tin:
    this.userAuthorize.account = user.account;
    this.userAuthorize.name = user.name;

    this.modalRef = this.modalService.show(template);
  }
  closeModal(modalId?: number){
    this.modalService.hide(modalId);
  }
}
